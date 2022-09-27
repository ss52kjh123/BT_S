
#include "TcpServer.h"
#include "main.h"
#include "Basic.h"
//#include "FlashMemory.h"
#if LWIP_TCP
static char Tcp_tx_buf[100];
static  char Tcp_rx_buf[100];
short sTxCount;
volatile int iReceiveCmmandType;
volatile int iTcpOpenCount;
volatile int iTcpConnectedCount;
volatile int iTcpCloseCount;
static struct tcp_pcb *tcp_echoserver_pcb;

//전역변수
static uint8_t uiPareingState;
static uint8_t rx_Bluetooth;
extern uint16_t iTcpReceiveCnt;
uint8_t *Rs232_str ="\0";
int iReceiveCounter;
extern volatile int iRs232_count_CallBack;
extern volatile int iRs232_count;
extern volatile char Rs232_receive_buffer[50];
extern UART_HandleTypeDef huart3;
uint8_t Send_BT_STR[100];
int iTcpRxCounter;
int iTcpRs232Debug;

//Tcp Ip,Port
uint16_t _set_ip1,_set_ip2,_set_ip3,_set_ip4,_set_port;
/* ECHO protocol states */
enum tcp_echoserver_states
{
  ES_NONE = 0,
  ES_ACCEPTED,
  ES_RECEIVED,
  ES_CLOSING
};

/* structure for maintaining connection infos to be passed as argument
   to LwIP callbacks*/
struct tcp_echoserver_struct
{
  u8_t state;             /* current connection state */
  u8_t retries;
  struct tcp_pcb *pcb;    /* pointer on the current tcp_pcb */
  struct pbuf *p;         /* pointer on the received/to be transmitted pbuf */
};


static err_t tcp_echoserver_accept(void *arg, struct tcp_pcb *newpcb, err_t err);
static err_t tcp_echoserver_recv(void *arg, struct tcp_pcb *tpcb, struct pbuf *p, err_t err);
static void tcp_echoserver_error(void *arg, err_t err);
static err_t tcp_echoserver_poll(void *arg, struct tcp_pcb *tpcb);
static err_t tcp_echoserver_sent(void *arg, struct tcp_pcb *tpcb, u16_t len);
static void tcp_echoserver_send(struct tcp_pcb *tpcb, struct tcp_echoserver_struct *es);
static void tcp_echoserver_connection_close(struct tcp_pcb *tpcb, struct tcp_echoserver_struct *es);


/**
  * @brief  Initializes the tcp echo server
  * @param  None
  * @retval None
  */
void tcp_echoserver_init(uint16_t setPort)
{
  /* create new tcp pcb */
  tcp_echoserver_pcb = tcp_new();

  if (tcp_echoserver_pcb != NULL)
  {
    err_t err;

    /* bind echo_pcb to port 7 (ECHO protocol) */
    err = tcp_bind(tcp_echoserver_pcb, IP_ADDR_ANY, setPort);

    if (err == ERR_OK)
    {
      /* start tcp listening for echo_pcb */
      tcp_echoserver_pcb = tcp_listen(tcp_echoserver_pcb);

      /* initialize LwIP tcp_accept callback function */
      tcp_accept(tcp_echoserver_pcb, tcp_echoserver_accept);
    }
    else
    {
      /* deallocate the pcb */
      memp_free(MEMP_TCP_PCB, tcp_echoserver_pcb);
    }
  }
}

/**
  * @brief  This function is the implementation of tcp_accept LwIP callback
  * @param  arg: not used
  * @param  newpcb: pointer on tcp_pcb struct for the newly created tcp connection
  * @param  err: not used
  * @retval err_t: error status
  */
static err_t tcp_echoserver_accept(void *arg, struct tcp_pcb *newpcb, err_t err)
{
  err_t ret_err;
  struct tcp_echoserver_struct *es;

  LWIP_UNUSED_ARG(arg);
  LWIP_UNUSED_ARG(err);

  /* set priority for the newly accepted tcp connection newpcb */
  tcp_setprio(newpcb, TCP_PRIO_MIN);

  /* allocate structure es to maintain tcp connection informations */
  es = (struct tcp_echoserver_struct *)mem_malloc(sizeof(struct tcp_echoserver_struct));
  if (es != NULL)
  {
    es->state = ES_ACCEPTED;
    es->pcb = newpcb;
    es->retries = 0;
    es->p = NULL;

    /* pass newly allocated es structure as argument to newpcb */
    tcp_arg(newpcb, es);

    /* initialize lwip tcp_recv callback function for newpcb  */
    tcp_recv(newpcb, tcp_echoserver_recv);

    /* initialize lwip tcp_err callback function for newpcb  */
    tcp_err(newpcb, tcp_echoserver_error);

    /* initialize lwip tcp_poll callback function for newpcb */
    tcp_poll(newpcb, tcp_echoserver_poll, 0);

    ret_err = ERR_OK;
  }
  else
  {
    /*  close tcp connection */
    tcp_echoserver_connection_close(newpcb, es);
    /* return memory error */
    ret_err = ERR_MEM;
  }
  return ret_err;
}


/**
  * @brief  This function is the implementation for tcp_recv LwIP callback
  * @param  arg: pointer on a argument for the tcp_pcb connection
  * @param  tpcb: pointer on the tcp_pcb connection
  * @param  pbuf: pointer on the received pbuf
  * @param  err: error information regarding the reveived pbuf
  * @retval err_t: error code
  */
static err_t tcp_echoserver_recv(void *arg, struct tcp_pcb *tpcb, struct pbuf *p, err_t err)
{
  struct tcp_echoserver_struct *es;
  err_t ret_err;

  LWIP_ASSERT("arg != NULL",arg != NULL);

  es = (struct tcp_echoserver_struct *)arg;

  /* if we receive an empty tcp frame from client => close connection */
  if (p == NULL)
  {
    /* remote host closed connection */
    es->state = ES_CLOSING;
    if(es->p == NULL)
    {
       /* we're done sending, close connection */
       tcp_echoserver_connection_close(tpcb, es);
    }
    else
    {
      /* we're not done yet */
      /* acknowledge received packet */
      tcp_sent(tpcb, tcp_echoserver_sent);

      /* send remaining data*/
      tcp_echoserver_send(tpcb, es);
    }
    ret_err = ERR_OK;
  }
  /* else : a non empty frame was received from client but for some reason err != ERR_OK */
  else if(err != ERR_OK)
  {
    /* free received pbuf*/
    if (p != NULL)
    {
      es->p = NULL;
      pbuf_free(p);
    }
    ret_err = err;
  }
  else if(es->state == ES_ACCEPTED)
  {
    /* first data chunk in p->payload */
    es->state = ES_RECEIVED;

    /* store reference to incoming pbuf (chain) */
    es->p = p;
    pbuf_copy_partial(p, &Tcp_rx_buf, p->tot_len, 0) ;
    iReceiveCmmandType = Tcp_rx_buf[3];


    /* 클라이언트 BnR에서 처음 데이터를 받았을 떄(echo) */
    switch(iReceiveCmmandType)
          {
            case 1: //You Live?
            	sTxCount++;
            	if(sTxCount>100)
            		sTxCount=0;
            	Tcp_tx_buf[0] = 2;				  //start
              	Tcp_tx_buf[1] = Tcp_rx_buf[1];	  //length
               	Tcp_tx_buf[2] = Tcp_tx_buf[2] +1; //count
               	Tcp_tx_buf[3] = 1;           	  //command ACK
               	Tcp_tx_buf[4] = 3;	  			  //End
            	es->p->payload =&Tcp_tx_buf;
            	es->p->len= 5;
            	es->p->tot_len = 5;
            	tcp_echoserver_send(tpcb, es);
            	break;

            case 2: //ip1234 ,port 설정
            	Tcp_tx_buf[0] = 2;				  //start
            	Tcp_tx_buf[1] = Tcp_rx_buf[1];	  //length
            	Tcp_tx_buf[2] = Tcp_tx_buf[2] +1; //count
            	Tcp_tx_buf[3] = 2;           	  //command ACK
            	Tcp_tx_buf[4] = Tcp_rx_buf[4];	  //ip1
            	Tcp_tx_buf[5] = Tcp_rx_buf[5];	  //ip1
            	Tcp_tx_buf[6] = Tcp_rx_buf[6];	  //ip2
            	Tcp_tx_buf[7] = Tcp_rx_buf[7];    //ip2
            	Tcp_tx_buf[8] = Tcp_rx_buf[8];	  //ip3
            	Tcp_tx_buf[9] = Tcp_rx_buf[9];    //ip3
            	Tcp_tx_buf[10] = Tcp_rx_buf[10];  //ip4
            	Tcp_tx_buf[11] = Tcp_rx_buf[11];  //ip4
            	Tcp_tx_buf[12] = Tcp_rx_buf[12];  //port
            	Tcp_tx_buf[13] = Tcp_rx_buf[13];  //port
            	Tcp_tx_buf[14] = 3;  			  //end


            	_set_ip1 = (Tcp_rx_buf[5] << 8) + Tcp_rx_buf[4] ;
            	_set_ip2 = (Tcp_rx_buf[7] << 8) + Tcp_rx_buf[6] ;
            	_set_ip3 = (Tcp_rx_buf[9] << 8) + Tcp_rx_buf[8] ;
            	_set_ip4 = (Tcp_rx_buf[11] << 8) + Tcp_rx_buf[10] ;
            	//_set_port = (Tcp_rx_buf[13] << 8) + Tcp_rx_buf[12] ;
            	_set_port =9300;
            	FlashMemoryWrite(_set_ip1,_set_ip2,_set_ip3,_set_ip4,_set_port);
            	es->p->payload =&Tcp_tx_buf;
            	es->p->len= 15;
            	es->p->tot_len = 15;
            	tcp_echoserver_send(tpcb, es);
              	iTcpRxCounter++;
              	iReceiveCmmandType =0;
            	break;

            case 10:  //case10이 실제 리모컨 값 주는 경우
           		Tcp_tx_buf[0] = 2;				  //start
             	Tcp_tx_buf[1] = Tcp_rx_buf[1];	  //Length
             	Tcp_tx_buf[2] = Tcp_tx_buf[2] +1; //count
             	Tcp_tx_buf[3] = 10;				  //command ACK
             	if(uiPareingState)
             	  Tcp_tx_buf[4] =1; //블루투스 페어링 상태
             	else
             	  Tcp_tx_buf[4] =0;

             	if(rx_Bluetooth=='4') //up
             	{
             	Tcp_tx_buf[5]=1;
             	Tcp_tx_buf[6]=0;
             	Tcp_tx_buf[7]=0;
             	Tcp_tx_buf[8]=0;
             	Tcp_tx_buf[9]=0;
             	}
                else if(rx_Bluetooth=='1') //down
      			{
             	Tcp_tx_buf[5]=0;
             	Tcp_tx_buf[6]=1;
             	Tcp_tx_buf[7]=0;
             	Tcp_tx_buf[8]=0;
             	Tcp_tx_buf[9]=0;
      			}
             	else if(rx_Bluetooth=='3') //left
      			{
             	Tcp_tx_buf[5]=0;
             	Tcp_tx_buf[6]=0;
             	Tcp_tx_buf[7]=1;
             	Tcp_tx_buf[8]=0;
             	Tcp_tx_buf[9]=0;
      			}
             	else if(rx_Bluetooth=='2') //right
      			{
             	Tcp_tx_buf[5]=0;
             	Tcp_tx_buf[6]=0;
             	Tcp_tx_buf[7]=0;
             	Tcp_tx_buf[8]=1;
             	Tcp_tx_buf[9]=0;
      			}
             	else
             	{
             	Tcp_tx_buf[5]=0;
             	Tcp_tx_buf[6]=0;
             	Tcp_tx_buf[7]=0;
             	Tcp_tx_buf[8]=0;
             	Tcp_tx_buf[9]=0;
             	}
             	if(!uiPareingState)
             	{
             	Tcp_tx_buf[5]=0;
             	Tcp_tx_buf[6]=0;
             	Tcp_tx_buf[7]=0;
             	Tcp_tx_buf[8]=0;
             	Tcp_tx_buf[9]=0;
             	}
             	Tcp_tx_buf[10]=3; //end
             	es->p->payload =&Tcp_tx_buf;
             	es->p->len= 11;
             	es->p->tot_len = 11;
             	tcp_echoserver_send(tpcb, es);
             	break;

            case 100: //AT 살아있니?

            	//Rs232_str = "AT\r\n";
            	iTcpRs232Debug =0;
            	for(int i=0; i<99; i++)
            	{
            		Tcp_tx_buf[i] = '\0';

            		Rs232_receive_buffer[i] = '\0';
            		Send_BT_STR[i] = '\0';
            	}

            	for(int i=0; i<Tcp_rx_buf[1]-5; i++)
            	{
            		Send_BT_STR[i] = Tcp_rx_buf[i+4];
            	}
            	iTcpRs232Debug =2;
            	if(Tcp_rx_buf[1]-5<1)
            	{
            		HAL_UART_Transmit(&huart3,Send_BT_STR,sizeof(Send_BT_STR),10);   //RS232 송신
            	}
               	else
               	{
               		HAL_UART_Transmit(&huart3,Send_BT_STR,Tcp_rx_buf[1]-5,10);   //RS232 송신
               	}
            	iTcpRs232Debug =3;
            	Delay_ms(300);
            	iTcpRs232Debug =4;
               	Tcp_tx_buf[0] = 2;				  //telegram start
              	Tcp_tx_buf[1] = iRs232_count_CallBack+5;  //telegram length
               	Tcp_tx_buf[2] = Tcp_tx_buf[2] +1; //count
               	Tcp_tx_buf[3] = 100;           	  //command ACK
               	iTcpRs232Debug =5;
            	for(int i=0; i<iRs232_count_CallBack; i++)  //Rs232 인터럽트 갯수 리턴 받아 i값 적용
            	{
            		Tcp_tx_buf[i+4] = Rs232_receive_buffer[i];
            	}

               	Tcp_tx_buf[iRs232_count_CallBack+4] = 3;	//telegram  end

               	es->p->payload =&Tcp_tx_buf;
              	es->p->len= iRs232_count_CallBack+5;
              	es->p->tot_len = iRs232_count_CallBack+5;
              	iTcpRs232Debug =10;
              	tcp_echoserver_send(tpcb, es);    //TCP 송신
              	iRs232_count_CallBack =0;
              	iRs232_count =0;
              	iTcpRs232Debug =20;
              	iTcpRxCounter++;
              	iReceiveCmmandType =0;
              	break;

          }

    ret_err = ERR_OK;
  }
  else if (es->state == ES_RECEIVED)
  {
	/* 클라이언트에서 두번째부터 데이터를 받았을 떄 */
    if(es->p == NULL)
    {
      iReceiveCounter++;
      es->p = p;
      pbuf_copy_partial(p, &Tcp_rx_buf, p->tot_len, 0) ;
      iReceiveCmmandType = Tcp_rx_buf[3];
      /* send back received data */


      switch(iReceiveCmmandType)
      {
        case 1: //You Live?
        	sTxCount++;
        	if(sTxCount>100)
        		sTxCount=0;
        	Tcp_tx_buf[0] = 2;				  //start
          	Tcp_tx_buf[1] = Tcp_rx_buf[1];	  //length
           	Tcp_tx_buf[2] = Tcp_tx_buf[2] +1; //count
           	Tcp_tx_buf[3] = 1;           	  //command ACK
           	Tcp_tx_buf[4] = 3;	  			  //End
        	es->p->payload =&Tcp_tx_buf;
        	es->p->len= 5;
        	es->p->tot_len = 5;
        	tcp_echoserver_send(tpcb, es);
        	break;

        case 2: //ip1234 ,port 설정
        	Tcp_tx_buf[0] = 2;				  //start
        	Tcp_tx_buf[1] = Tcp_rx_buf[1];	  //length
        	Tcp_tx_buf[2] = Tcp_tx_buf[2] +1; //count
        	Tcp_tx_buf[3] = 2;           	  //command ACK
        	Tcp_tx_buf[4] = Tcp_rx_buf[4];	  //ip1
        	Tcp_tx_buf[5] = Tcp_rx_buf[5];	  //ip1
        	Tcp_tx_buf[6] = Tcp_rx_buf[6];	  //ip2
        	Tcp_tx_buf[7] = Tcp_rx_buf[7];    //ip2
        	Tcp_tx_buf[8] = Tcp_rx_buf[8];	  //ip3
        	Tcp_tx_buf[9] = Tcp_rx_buf[9];    //ip3
        	Tcp_tx_buf[10] = Tcp_rx_buf[10];  //ip4
        	Tcp_tx_buf[11] = Tcp_rx_buf[11];  //ip4
        	Tcp_tx_buf[12] = Tcp_rx_buf[12];  //port
        	Tcp_tx_buf[13] = Tcp_rx_buf[13];  //port
        	Tcp_tx_buf[14] = 3;  			  //end


        	_set_ip1 = (Tcp_rx_buf[5] << 8) + Tcp_rx_buf[4] ;
        	_set_ip2 = (Tcp_rx_buf[7] << 8) + Tcp_rx_buf[6] ;
        	_set_ip3 = (Tcp_rx_buf[9] << 8) + Tcp_rx_buf[8] ;
        	_set_ip4 = (Tcp_rx_buf[11] << 8) + Tcp_rx_buf[10] ;
        	_set_port = (Tcp_rx_buf[13] << 8) + Tcp_rx_buf[12] ;
        	FlashMemoryWrite(_set_ip1,_set_ip2,_set_ip3,_set_ip4,_set_port);
        	es->p->payload =&Tcp_tx_buf;
        	es->p->len= 15;
        	es->p->tot_len = 15;
        	tcp_echoserver_send(tpcb, es);
          	iTcpRxCounter++;
          	iReceiveCmmandType =0;
        	break;

        case 10:  //case10이 실제 리모컨 값 주는 경우
       		Tcp_tx_buf[0] = 2;				  //start
         	Tcp_tx_buf[1] = Tcp_rx_buf[1];	  //Length
         	Tcp_tx_buf[2] = Tcp_tx_buf[2] +1; //count
         	Tcp_tx_buf[3] = 10;				  //command ACK
         	if(uiPareingState)
         	  Tcp_tx_buf[4] =1; //블루투스 페어링 상태
         	else
         	  Tcp_tx_buf[4] =0;

         	if(rx_Bluetooth=='4') //up
         	{
         	Tcp_tx_buf[5]=1;
         	Tcp_tx_buf[6]=0;
         	Tcp_tx_buf[7]=0;
         	Tcp_tx_buf[8]=0;
         	Tcp_tx_buf[9]=0;
         	}
            else if(rx_Bluetooth=='1') //down
  			{
         	Tcp_tx_buf[5]=0;
         	Tcp_tx_buf[6]=1;
         	Tcp_tx_buf[7]=0;
         	Tcp_tx_buf[8]=0;
         	Tcp_tx_buf[9]=0;
  			}
         	else if(rx_Bluetooth=='3') //left
  			{
         	Tcp_tx_buf[5]=0;
         	Tcp_tx_buf[6]=0;
         	Tcp_tx_buf[7]=1;
         	Tcp_tx_buf[8]=0;
         	Tcp_tx_buf[9]=0;
  			}
         	else if(rx_Bluetooth=='2') //right
  			{
         	Tcp_tx_buf[5]=0;
         	Tcp_tx_buf[6]=0;
         	Tcp_tx_buf[7]=0;
         	Tcp_tx_buf[8]=1;
         	Tcp_tx_buf[9]=0;
  			}
         	else
         	{
         	Tcp_tx_buf[5]=0;
         	Tcp_tx_buf[6]=0;
         	Tcp_tx_buf[7]=0;
         	Tcp_tx_buf[8]=0;
         	Tcp_tx_buf[9]=0;
         	}
         	if(!uiPareingState)
         	{
         	Tcp_tx_buf[5]=0;
         	Tcp_tx_buf[6]=0;
         	Tcp_tx_buf[7]=0;
         	Tcp_tx_buf[8]=0;
         	Tcp_tx_buf[9]=0;
         	}
         	Tcp_tx_buf[10]=3; //end
         	es->p->payload =&Tcp_tx_buf;
         	es->p->len= 11;
         	es->p->tot_len = 11;
         	tcp_echoserver_send(tpcb, es);
         	iTcpRxCounter++;
         	break;

        case 100: //AT 살아있니?

        	//Rs232_str = "AT\r\n";
        	iTcpRs232Debug =0;
        	for(int i=0; i<99; i++)
        	{
        		Tcp_tx_buf[i] = '\0';

        		Rs232_receive_buffer[i] = '\0';
        		Send_BT_STR[i] = '\0';
        	}

        	for(int i=0; i<Tcp_rx_buf[1]-5; i++)
        	{
        		Send_BT_STR[i] = Tcp_rx_buf[i+4];
        	}
        	iTcpRs232Debug =2;
        	if(Tcp_rx_buf[1]-5<1)
        	{
        		HAL_UART_Transmit(&huart3,Send_BT_STR,sizeof(Send_BT_STR),10);   //RS232 송신
        	}
        	else
        	{
        		HAL_UART_Transmit(&huart3,Send_BT_STR,Tcp_rx_buf[1]-5,10);   //RS232 송신
        	}


        	iTcpRs232Debug =3;
        	Delay_ms(300);
        	iTcpRs232Debug =4;
           	Tcp_tx_buf[0] = 2;				  //telegram start
          	Tcp_tx_buf[1] = iRs232_count_CallBack+5;  //telegram length
           	Tcp_tx_buf[2] = Tcp_tx_buf[2] +1; //count
           	Tcp_tx_buf[3] = 100;           	  //command ACK
           	iTcpRs232Debug =5;
        	for(int i=0; i<iRs232_count_CallBack; i++)  //Rs232 인터럽트 갯수 리턴 받아 i값 적용
        	{
        		Tcp_tx_buf[i+4] = Rs232_receive_buffer[i];
        	}

           	Tcp_tx_buf[iRs232_count_CallBack+4] = 3;	//telegram  end

           	es->p->payload =&Tcp_tx_buf;
          	es->p->len= iRs232_count_CallBack+5;
          	es->p->tot_len = iRs232_count_CallBack+5;
          	iTcpRs232Debug =10;
          	tcp_echoserver_send(tpcb, es);    //TCP 송신
          	iRs232_count_CallBack =0;
          	iRs232_count =0;
          	iTcpRs232Debug =20;
          	iTcpRxCounter++;
          	iReceiveCmmandType =0;
          	break;

      }

    }
    else
    {
      struct pbuf *ptr;

      /* chain pbufs to the end of what we recv'ed previously  */
      ptr = es->p;
      pbuf_chain(ptr,p);
    }
    ret_err = ERR_OK;
  }
  else if(es->state == ES_CLOSING)
  {
    /* odd case, remote side closing twice, trash data */
    tcp_recved(tpcb, p->tot_len);
    es->p = NULL;
    pbuf_free(p);
    ret_err = ERR_OK;
  }
  else
  {
    /* unknown es->state, trash data  */
    tcp_recved(tpcb, p->tot_len);
    es->p = NULL;
    pbuf_free(p);
    ret_err = ERR_OK;
  }
  return ret_err;
}

/**
  * @brief  This function implements the tcp_err callback function (called
  *         when a fatal tcp_connection error occurs.
  * @param  arg: pointer on argument parameter
  * @param  err: not used
  * @retval None
  */
static void tcp_echoserver_error(void *arg, err_t err)
{
  struct tcp_echoserver_struct *es;

  LWIP_UNUSED_ARG(err);

  es = (struct tcp_echoserver_struct *)arg;
  if (es != NULL)
  {
    /*  free es structure */
    mem_free(es);
  }
}

/**
  * @brief  This function implements the tcp_poll LwIP callback function
  * @param  arg: pointer on argument passed to callback
  * @param  tpcb: pointer on the tcp_pcb for the current tcp connection
  * @retval err_t: error code
  */
static err_t tcp_echoserver_poll(void *arg, struct tcp_pcb *tpcb)
{
  err_t ret_err;
  struct tcp_echoserver_struct *es;

  es = (struct tcp_echoserver_struct *)arg;
  if (es != NULL)
  {
    if (es->p != NULL)
    {
      tcp_sent(tpcb, tcp_echoserver_sent);
      /* there is a remaining pbuf (chain) , try to send data */
      tcp_echoserver_send(tpcb, es);
    }
    else
    {
      /* no remaining pbuf (chain)  */
      if(es->state == ES_CLOSING)
      {
        /*  close tcp connection */
        tcp_echoserver_connection_close(tpcb, es);
      }
    }
    ret_err = ERR_OK;
  }
  else
  {
    /* nothing to be done */
    tcp_abort(tpcb);
    ret_err = ERR_ABRT;
  }
  return ret_err;
}

/**
  * @brief  This function implements the tcp_sent LwIP callback (called when ACK
  *         is received from remote host for sent data)
  * @param  None
  * @retval None
  */
static err_t tcp_echoserver_sent(void *arg, struct tcp_pcb *tpcb, u16_t len)
{
  struct tcp_echoserver_struct *es;

  LWIP_UNUSED_ARG(len);

  es = (struct tcp_echoserver_struct *)arg;
  es->retries = 0;

  if(es->p != NULL)
  {
    /* still got pbufs to send */
    tcp_sent(tpcb, tcp_echoserver_sent);
    tcp_echoserver_send(tpcb, es);
  }
  else
  {
    /* if no more data to send and client closed connection*/
    if(es->state == ES_CLOSING)
      tcp_echoserver_connection_close(tpcb, es);
  }
  return ERR_OK;
}


/**
  * @brief  This function is used to send data for tcp connection
  * @param  tpcb: pointer on the tcp_pcb connection
  * @param  es: pointer on echo_state structure
  * @retval None
  */
static void tcp_echoserver_send(struct tcp_pcb *tpcb, struct tcp_echoserver_struct *es)
{
  struct pbuf *ptr;
  err_t wr_err = ERR_OK;

  while ((wr_err == ERR_OK) &&
         (es->p != NULL) &&
         (es->p->len <= tcp_sndbuf(tpcb)))
  {

    /* get pointer on pbuf from es structure */
    ptr = es->p;

    /* enqueue data for transmission */
    wr_err = tcp_write(tpcb, ptr->payload, ptr->len, 1);

    if (wr_err == ERR_OK)
    {
      u16_t plen;
      u8_t freed;

      plen = ptr->len;

      /* continue with next pbuf in chain (if any) */
      es->p = ptr->next;

      if(es->p != NULL)
      {
        /* increment reference count for es->p */
        pbuf_ref(es->p);
      }

     /* chop first pbuf from chain */
      do
      {
        /* try hard to free pbuf */
        freed = pbuf_free(ptr);
      }
      while(freed == 0);
     /* we can read more data now */
     tcp_recved(tpcb, plen);
   }
   else if(wr_err == ERR_MEM)
   {
      /* we are low on memory, try later / harder, defer to poll */
     es->p = ptr;
   }
   else
   {
     /* other problem ?? */
   }
  }
}

/**
  * @brief  This functions closes the tcp connection
  * @param  tcp_pcb: pointer on the tcp connection
  * @param  es: pointer on echo_state structure
  * @retval None
  */
static void tcp_echoserver_connection_close(struct tcp_pcb *tpcb, struct tcp_echoserver_struct *es)
{

  /* remove all callbacks */
  tcp_arg(tpcb, NULL);
  tcp_sent(tpcb, NULL);
  tcp_recv(tpcb, NULL);
  tcp_err(tpcb, NULL);
  tcp_poll(tpcb, NULL, 0);

  /* delete es structure */
  if (es != NULL)
  {
    mem_free(es);
  }

  /* close tcp connection */
  tcp_close(tpcb);
}
//kyd 정의
int TcpCounterCallBack(uint16_t a,uint16_t b){
	return iReceiveCounter;
}
TcpGetBool Set_TcpVariable_BOOL(bool a1,bool b1, bool c1)
{
	TcpGetBool TcpGetBool_0;

	TcpGetBool_0.a1 = a1;
	TcpGetBool_0.b1 = b1;
	TcpGetBool_0.c1 = c1;

	return TcpGetBool_0;
}
#endif /* LWIP_TCP */
