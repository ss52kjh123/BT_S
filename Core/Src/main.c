

#include "main.h"
#include "lwip.h"
#include "lwip/debug.h"
#include "lwip/stats.h"
#include "lwip/tcp.h"
#include "netif/etharp.h"
#include "Basic.h"
#include "TcpServer.h"
#include "FlashMemory.h"
#include "AS5600.h"
/* Private includes ----------------------------------------------------------*/
/* USER CODE BEGIN Includes */

/* USER CODE END Includes */

/* Private typedef -----------------------------------------------------------*/
/* USER CODE BEGIN PTD */
struct netif gnetif;

int iTcpIpCallbackCounter;
/* USER CODE END PTD */

/* Private define ------------------------------------------------------------*/
/* USER CODE BEGIN PD */
/* USER CODE END PD */

/* Private macro -------------------------------------------------------------*/
/* USER CODE BEGIN PM */

/* USER CODE END PM */



UART_HandleTypeDef huart4;
UART_HandleTypeDef huart3;

/* USER CODE BEGIN PV */

/* USER CODE END PV */

/* Private function prototypes -----------------------------------------------*/
void SystemClock_Config(void);
static void MX_GPIO_Init(void);
static void MX_I2C1_Init(void);
static void MX_I2C2_Init(void);
static void MX_USART3_UART_Init(void);
static void MX_UART4_Init(void);
/* USER CODE BEGIN PFP */

/* USER CODE END PFP */

/* Private user code ---------------------------------------------------------*/
/* USER CODE BEGIN 0 */

/* USER CODE END 0 */
/*-------------------------  블루투스 모듈 -2022 08.17 -kyd ----------------------*/
uint8_t iBluetoothStep;
uint8_t iInterruptCnt,iInterrupt_TotalCnt;
uint8_t *ManualRs232_str ="AT\r";
uint8_t *ManualRs232_str2 ="AT+\r";
char Rs232_manualSend[10]="AT\r";

uint32_t iSystemTick;
uint8_t Rs232_receive_data;
uint8_t test_BT;
volatile int iRs232ReceiveCounter;
volatile char rx_Bluetooth;
volatile bool gbRs232_Flag;
bool bATMODE_ON;
bool bPairingMODE_ON;
volatile int iRs232_count;
volatile int iRs232_count_CallBack;
volatile char Rs232_receive_buffer[100];
char *test_Str;
int iUART_InterruptCount;
volatile int istepPareingCheck;
volatile int istepPareingCheck_TimeOut;
volatile bool gbPareingState;
volatile int iPareingFail_Flag_Cnt;
/*-------------------------  Flash Memory -2022 06.15 -kyd ----------------------*/
void GetFlashMemory(void);
uint8_t flash_read_data[20];
short GetFlashMemory_SavedCounter,get_ip1,get_ip2,get_ip3,get_ip4,get_port;
//디폴트 IP,Port
uint8_t ipDefaultSet_1 =192;
uint8_t ipDefaultSet_2 =168;
uint8_t ipDefaultSet_3 =0;
uint8_t ipDefaultSet_4 =11;
uint16_t iDefaultPort =9300;

/*-------------------------  Wire Draw AS5600 -2022 09.05 -kyd ----------------------*/
int iI2C_Cnt_1,iI2C_Cnt_2;
float numberofTurns = 0; //number of turns
float correctedAngle = 0; //tared angle - based on the startup value
float startAngle = 0; //starting angle
int quadrantNumber, previousquadrantNumber; //quadrant IDs

float previoustotalAngle = 0; //for the display printing

float totalAngle = 0; //total absolute angular displacement
float degAngle;
long revolutions = 0;   // number of revolutions the encoder has made
double position = 0;    // the calculated value the encoder is at
double  output;          // raw value from AS5600
long lastOutput;        // last output from AS5600
float WireLength;

I2C_HandleTypeDef hi2c1;
I2C_HandleTypeDef hi2c2;
HAL_I2C_StateTypeDef stateI2C_1;	// state of the I2C bus
HAL_I2C_StateTypeDef stateI2C_2;	// state of the I2C bus too
HAL_StatusTypeDef retI2C;			// value returned by I2C transmit
void checkQuadrant();

int main(void)
{



  /* USER CODE BEGIN 1 */

  /* USER CODE END 1 */

  /* MCU Configuration--------------------------------------------------------*/

  /* Reset of all peripherals, Initializes the Flash interface and the Systick. */
  HAL_Init();

  /* USER CODE BEGIN Init */

  /* USER CODE END Init */

  /* Configure the system clock */
  SystemClock_Config();

  /* USER CODE BEGIN SysInit */

  /* USER CODE END SysInit */

  /* Initialize all configured peripherals */
  MX_GPIO_Init();


  MX_UART4_Init();
  /* USER CODE BEGIN 2 */
  ST_LED_ON_1();
  ST_LED_ON_2();
  ST_LED_ON_3();
  /* USER CODE END 2 */

  /* Infinite loop */


  Delay_ms(50);
  MX_USART3_UART_Init();
  HAL_UART_Receive_IT(&huart3, &Rs232_receive_data, 1);  //AT Mode 아닌경우
  //HAL_UART_Transmit(&huart3,test_BT,sizeof(test_BT),10);
  iInterruptCnt =0;
  iInterrupt_TotalCnt =0;


  //Flash Memory -> Ip Set
  GetFlashMemory();
  if(GetFlashMemory_SavedCounter <=0){  //한번도 저장을 안한 초기 상태라면 디폴트 IP,PORT설정
	  MX_LWIP_Init(ipDefaultSet_1,ipDefaultSet_2,ipDefaultSet_3,ipDefaultSet_4);
	  tcp_echoserver_init(iDefaultPort);
  }
  else   //한번이라도 저장한 적이 있다면 플레시 메모리 값으로 IP,PORT설정
  {
	  MX_LWIP_Init(ipDefaultSet_1,ipDefaultSet_2,ipDefaultSet_3,ipDefaultSet_4);
	 	  tcp_echoserver_init(iDefaultPort);
	  //MX_LWIP_Init(get_ip1,get_ip2,get_ip3,get_ip4);
	  //tcp_echoserver_init(get_port);
  }

	/*-------------------------  Wire Draw AS5600 -2022 09.05 -kyd ----------------------*/
  MX_I2C1_Init();
  MX_I2C2_Init();
  Delay_ms(30);

  output =AS5600_GetRawAngle();
  Delay_ms(30);
  degAngle = -1*output* 0.087890625;
  startAngle = degAngle;
  lastOutput = output;
  position = output;
  //Init Complite
  Init_Complite_Led_Offeration();
  /* USER CODE BEGIN WHILE */

  bATMODE_ON = true;

  while (1)
  {
	  /*-------------------------  Wire Draw AS5600 -2022 09.05 -kyd ----------------------*/
	//uint16_t angle = 0;

	output =AS5600_GetRawAngle();

	 correctedAngle = degAngle - startAngle;  //correct angle
	 checkQuadrant();
	  if(correctedAngle < 0) //if the calculated angle is negative, we need to "normalize" it
	  {
	  correctedAngle = correctedAngle + 360; //correction for negative numbers (i.e. -15 becomes +345)
	  }
	  else
	  {
	    //do nothing
	  }
	  degAngle = output* 0.087890625;
	  if ((lastOutput - output) > 2047 )        // check if a full rotation has been made
	    revolutions++;
	  if ((lastOutput - output) < -2047 )
	    revolutions--;
	position = revolutions * 4096 + output;   // calculate the position the the encoder is at based off of the number of revolutions
	WireLength = position*0.038-80;   // -80은 오프셋 -> 향후 파라미터로 조정 가능하게
	 lastOutput = output;


    //MX_LWIP_Process();
	  ethernetif_input(&gnetif);

	  sys_check_timeouts();

	iTcpIpCallbackCounter = TcpCounterCallBack(1,2);
	Set_TcpVariable_BOOL(bPairingMODE_ON,bATMODE_ON, bATMODE_ON);
    switch(iBluetoothStep)
    {
    	case 0:
    		if(iInterruptCnt==1)	   //AT 모드 진입
    		{
    			iInterruptCnt =0;
    			iInterrupt_TotalCnt++;
    			iBluetoothStep = 10;
    		}
    		else if(iInterruptCnt==2)  //페어링 모드 진입
    		{
    			iInterruptCnt =0;
    			iInterrupt_TotalCnt++;
    		  	iBluetoothStep = 20;
    		}
    		else if(iInterruptCnt==3)  //TEST STRING
    		{
    			iInterruptCnt =0;
    			iInterrupt_TotalCnt++;
    		  	iBluetoothStep = 50;
    		}
    		else if(iInterruptCnt==4)  //TEST STRING
    		{
    			iInterruptCnt =0;
    			iInterrupt_TotalCnt++;
    		  	iBluetoothStep = 60;
    		}
    		break;

    	case 10: //AT MODE 시작

    		//Bluetooth_PowerOff();
    		//Delay_ms(500);
    		//Bluetooth_ATMode();
    		//Delay_ms(200);
    		iRs232_count =0;
    		Rs232_manualSend[0] = '\0';
    		Rs232_manualSend[1] = '\0';
    		Rs232_manualSend[2] = '\0';
    		Rs232_manualSend[3] = '\0';
    		Rs232_manualSend[4] = '\0';
    		Rs232_manualSend[5] = '\0';
    		Rs232_manualSend[6] = '\0';
    		Rs232_manualSend[7] = '\0';
    		Rs232_manualSend[8] = '\0';
    		Rs232_manualSend[9] = '\0';

    		Rs232_manualSend[0]='A';
    		Rs232_manualSend[1]='T';
    		Rs232_manualSend[2]='\r';
    		Delay_ms(30);
    		iBluetoothStep = 11;


    		break;
    	case 11:
    		HAL_UART_Transmit(&huart3,&Rs232_manualSend,3,50);
    		iBluetoothStep = 0;
    		break;
    	case 15:


    		iBluetoothStep = 0;
    		break;

    	case 20: //Pairing MODE 시작
    		iRs232_count =0;

    		Rs232_manualSend[0] = '\0';
    		Rs232_manualSend[1] = '\0';
    		Rs232_manualSend[2] = '\0';
    		Rs232_manualSend[3] = '\0';
    		Rs232_manualSend[4] = '\0';
    		Rs232_manualSend[5] = '\0';
    		Rs232_manualSend[6] = '\0';
    		Rs232_manualSend[7] = '\0';
    		Rs232_manualSend[8] = '\0';
    		Rs232_manualSend[9] = '\0';


    		Rs232_manualSend[0]='A';
    		Rs232_manualSend[1]='T';
    		Rs232_manualSend[2]='+';
    		Rs232_manualSend[3]='R';
    		Rs232_manualSend[4]='O';
    		Rs232_manualSend[5]='L';
    		Rs232_manualSend[6]='E';
    		Rs232_manualSend[7]='?';
    		Rs232_manualSend[8]='\r';
    		Delay_ms(30);
    		iBluetoothStep = 21;



    		break;
    	case 21:
    		HAL_UART_Transmit(&huart3,&Rs232_manualSend,9,50);
    		iBluetoothStep =0;
    	case 25:
    		Bluetooth_PowerOn();
    		Delay_ms(200);
    		bATMODE_ON = false;
    		bPairingMODE_ON = true;
    		iBluetoothStep = 0;
    		break;
    	case 30:
    		//HAL_UART_Transmit(&huart3,ManualRs232_str,sizeof(ManualRs232_str),10);   //RS232 송신
    		iBluetoothStep = 35;
    		break;
    	case 35:
    		Delay_ms(500);
    		iBluetoothStep = 0;
    		break;
    	case 50:
    		FlashMemoryWrite(192,168,0,18,9301);
    		iBluetoothStep = 0;
    		break;
    	case 60:
    		GetFlashMemory();
    		iBluetoothStep = 0;
    	default:
    		break;
    }

    switch(istepPareingCheck)
    	 {
    	 	 case 0:
    	 		gbPareingState =false;
    	 		 if(gbRs232_Flag)
    	 			istepPareingCheck =1;
    	 		 	 //if(istepPareingCheck_TimeOut>30000)
    	 	 	break;
    	 	 case 1:

    	 		 if(gbRs232_Flag)
    	 		 {
    	 			gbRs232_Flag=false;
    	 			istepPareingCheck_TimeOut =0;
    	 			gbPareingState =true;
    	 			istepPareingCheck=2;

    	 		 }
    	 		 else
    	 		 {
    	 			istepPareingCheck_TimeOut++;
    	 			 if(istepPareingCheck_TimeOut>100000)
    	 			 {
    	 				gbPareingState =false;
    	 				istepPareingCheck=3;

    	 			 }


    	 		 }
    	 		break;

    	 	 case 2:
    	 		istepPareingCheck=1;
    	 		break;
    	 	 case 3:
    	 		gbPareingState =false;
    	 		iPareingFail_Flag_Cnt++;
    	 		istepPareingCheck=4;
    	 		break;
    	 	 case 4:
    	 		 if(gbRs232_Flag)
    	 		 {

    	 			istepPareingCheck_TimeOut =0;
    	 			gbPareingState =true;
    	 			istepPareingCheck=1;
    	 		 }
    	 		break;
    	 }


  }
  /* USER CODE END 3 */
  if(rx_Bluetooth=='0')
  {
	  ST_LED_OFF_2();
  }
  else
  {
	  ST_LED_ON_2();
  }


}   //Main End
void checkQuadrant(){
	  //Quadrant 1
	  if(correctedAngle >= 0 && correctedAngle <=90)
	  {
	    quadrantNumber = 1;
	  }

	  //Quadrant 2
	  if(correctedAngle > 90 && correctedAngle <=180)
	  {
	    quadrantNumber = 2;
	  }

	  //Quadrant 3
	  if(correctedAngle > 180 && correctedAngle <=270)
	  {
	    quadrantNumber = 3;
	  }

	  //Quadrant 4
	  if(correctedAngle > 270 && correctedAngle <360)
	  {
	    quadrantNumber = 4;
	  }

	  if(quadrantNumber != previousquadrantNumber) //if we changed quadrant
	  {
	    if(quadrantNumber == 1 && previousquadrantNumber == 4)
	    {
	      numberofTurns++; // 4 --> 1 transition: CW rotation
	    }

	    if(quadrantNumber == 4 && previousquadrantNumber == 1)
	    {
	      numberofTurns--; // 1 --> 4 transition: CCW rotation
	    }
	    //this could be done between every quadrants so one can count every 1/4th of transition

	    previousquadrantNumber = quadrantNumber;  //update to the current quadrant

	  }
	  totalAngle = (numberofTurns*360) + correctedAngle;
}

void HAL_UART_RxCpltCallback(UART_HandleTypeDef *huart)
{


	 if(huart->Instance == huart3.Instance)
	  {
		 iUART_InterruptCount++;
		 if(iUART_InterruptCount>1000)
		 {
			 iUART_InterruptCount=0;
		 }

		// if(Rs232_receive_data == 'null') //마지막 문자
		// {
			 //Rs232_receive_buffer[iRs232_count] = '\n';
		//	 iRs232_count =0;

		// }
		// else //마지막 문자가 오기 전까지 버퍼에 저장
		// {
		 if(bATMODE_ON)
		 {
			 Rs232_receive_buffer[iRs232_count] = Rs232_receive_data;
			 iRs232_count++;
			 iRs232_count_CallBack++;   //TCP함수로 넘기기 위한 카운트
		 }
		 else //페어링 모드
		 {
			 gbRs232_Flag =true;
			 rx_Bluetooth = Rs232_receive_data;
		 }


		 HAL_UART_Receive_IT(&huart3, &Rs232_receive_data, 1);  //인터럽트 정의


	  }
}
void GetFlashMemory(void){
	FlashMemoryRead();
	GetFlashMemory_SavedCounter = (flash_read_data[1] << 8) + flash_read_data[0] ;
	get_ip1 = (flash_read_data[3] << 8) + flash_read_data[2] ;
	get_ip2 = (flash_read_data[5] << 8) + flash_read_data[4] ;
	get_ip3 = (flash_read_data[7] << 8) + flash_read_data[6] ;
	get_ip4 = (flash_read_data[9] << 8) + flash_read_data[8] ;
	get_port = (flash_read_data[11] << 8) + flash_read_data[10] ;
}
 void HAL_IncTick(void)
{
  iSystemTick++;
  if(iSystemTick>300)
	  iSystemTick=0;
  uwTick += uwTickFreq;
  if(iSystemTick < 100)
  {
	  ST_LED_ON_1();
	  if(gbPareingState)
	  {
		  ST_LED_OFF_3();
	  }
  }
  else if(iSystemTick > 100 && iSystemTick<200)
  {
	  if(gbPareingState)
	  {
		  ST_LED_ON_3();
	  }
	  else
	  {
		  ST_LED_OFF_3();
	  }
	  ST_LED_OFF_1();
	  //ST_LED_OFF_3();
  }
  else if(iSystemTick > 200 && iSystemTick<300)
  {
	  //ST_LED_ON_3();
	  ST_LED_OFF_1();
	  //ST_LED_OFF_2();
  }

}

 void HAL_GPIO_EXTI_Callback(uint16_t GPIO_Pin)
{
  if(GPIO_Pin == GPIO_PIN_2)
  {
	  iInterruptCnt=1;
  }
  else if(GPIO_Pin == GPIO_PIN_3)
  {
	  iInterruptCnt = 2;
  }
  else if(GPIO_Pin == GPIO_PIN_4)
  {
	  iInterruptCnt = 3;
  }
  else if(GPIO_Pin == GPIO_PIN_5)
  {
	  iInterruptCnt = 4;
  }
}
void SystemClock_Config(void)
{
  RCC_OscInitTypeDef RCC_OscInitStruct = {0};
  RCC_ClkInitTypeDef RCC_ClkInitStruct = {0};

  /** Configure the main internal regulator output voltage
  */
  __HAL_RCC_PWR_CLK_ENABLE();
  __HAL_PWR_VOLTAGESCALING_CONFIG(PWR_REGULATOR_VOLTAGE_SCALE3);

  /** Initializes the RCC Oscillators according to the specified parameters
  * in the RCC_OscInitTypeDef structure.
  */
  RCC_OscInitStruct.OscillatorType = RCC_OSCILLATORTYPE_HSI;
  RCC_OscInitStruct.HSIState = RCC_HSI_ON;
  RCC_OscInitStruct.HSICalibrationValue = RCC_HSICALIBRATION_DEFAULT;
  RCC_OscInitStruct.PLL.PLLState = RCC_PLL_NONE;
  if (HAL_RCC_OscConfig(&RCC_OscInitStruct) != HAL_OK)
  {
    Error_Handler();
  }

  /** Initializes the CPU, AHB and APB buses clocks
  */
  RCC_ClkInitStruct.ClockType = RCC_CLOCKTYPE_HCLK|RCC_CLOCKTYPE_SYSCLK
                              |RCC_CLOCKTYPE_PCLK1|RCC_CLOCKTYPE_PCLK2;
  RCC_ClkInitStruct.SYSCLKSource = RCC_SYSCLKSOURCE_HSI;
  RCC_ClkInitStruct.AHBCLKDivider = RCC_SYSCLK_DIV1;
  RCC_ClkInitStruct.APB1CLKDivider = RCC_HCLK_DIV2;
  RCC_ClkInitStruct.APB2CLKDivider = RCC_HCLK_DIV1;

  if (HAL_RCC_ClockConfig(&RCC_ClkInitStruct, FLASH_LATENCY_0) != HAL_OK)
  {
    Error_Handler();
  }
}

/**
  * @brief I2C1 Initialization Function
  * @param None
  * @retval None
  */
static void MX_I2C1_Init(void)
{

  /* USER CODE BEGIN I2C1_Init 0 */

  /* USER CODE END I2C1_Init 0 */

  /* USER CODE BEGIN I2C1_Init 1 */

  /* USER CODE END I2C1_Init 1 */
  hi2c1.Instance = I2C1;
  hi2c1.Init.ClockSpeed = 100000;
  hi2c1.Init.DutyCycle = I2C_DUTYCYCLE_2;
  hi2c1.Init.OwnAddress1 = 0;
  hi2c1.Init.AddressingMode = I2C_ADDRESSINGMODE_7BIT;
  hi2c1.Init.DualAddressMode = I2C_DUALADDRESS_DISABLE;
  hi2c1.Init.OwnAddress2 = 0;
  hi2c1.Init.GeneralCallMode = I2C_GENERALCALL_DISABLE;
  hi2c1.Init.NoStretchMode = I2C_NOSTRETCH_DISABLE;
  if (HAL_I2C_Init(&hi2c1) != HAL_OK)
  {
    Error_Handler();
  }

  /** Configure Analogue filter
  */
  if (HAL_I2CEx_ConfigAnalogFilter(&hi2c1, I2C_ANALOGFILTER_ENABLE) != HAL_OK)
  {
    Error_Handler();
  }

  /** Configure Digital filter
  */
  if (HAL_I2CEx_ConfigDigitalFilter(&hi2c1, 0) != HAL_OK)
  {
    Error_Handler();
  }
  /* USER CODE BEGIN I2C1_Init 2 */

  /* USER CODE END I2C1_Init 2 */

}

/**
  * @brief I2C2 Initialization Function
  * @param None
  * @retval None
  */
static void MX_I2C2_Init(void)
{

  /* USER CODE BEGIN I2C2_Init 0 */

  /* USER CODE END I2C2_Init 0 */

  /* USER CODE BEGIN I2C2_Init 1 */

  /* USER CODE END I2C2_Init 1 */
  hi2c2.Instance = I2C2;
  hi2c2.Init.ClockSpeed = 100000;
  hi2c2.Init.DutyCycle = I2C_DUTYCYCLE_2;
  hi2c2.Init.OwnAddress1 = 0;
  hi2c2.Init.AddressingMode = I2C_ADDRESSINGMODE_7BIT;
  hi2c2.Init.DualAddressMode = I2C_DUALADDRESS_DISABLE;
  hi2c2.Init.OwnAddress2 = 0;
  hi2c2.Init.GeneralCallMode = I2C_GENERALCALL_DISABLE;
  hi2c2.Init.NoStretchMode = I2C_NOSTRETCH_DISABLE;
  if (HAL_I2C_Init(&hi2c2) != HAL_OK)
  {
    Error_Handler();
  }

  /** Configure Analogue filter
  */
  if (HAL_I2CEx_ConfigAnalogFilter(&hi2c2, I2C_ANALOGFILTER_ENABLE) != HAL_OK)
  {
    Error_Handler();
  }

  /** Configure Digital filter
  */
  if (HAL_I2CEx_ConfigDigitalFilter(&hi2c2, 0) != HAL_OK)
  {
    Error_Handler();
  }
  /* USER CODE BEGIN I2C2_Init 2 */

  /* USER CODE END I2C2_Init 2 */

}

/**
  * @brief UART4 Initialization Function
  * @param None
  * @retval None
  */
static void MX_UART4_Init(void)
{

  /* USER CODE BEGIN UART4_Init 0 */

  /* USER CODE END UART4_Init 0 */

  /* USER CODE BEGIN UART4_Init 1 */

  /* USER CODE END UART4_Init 1 */
  huart4.Instance = UART4;
  huart4.Init.BaudRate = 115200;
  huart4.Init.WordLength = UART_WORDLENGTH_8B;
  huart4.Init.StopBits = UART_STOPBITS_1;
  huart4.Init.Parity = UART_PARITY_NONE;
  huart4.Init.Mode = UART_MODE_TX_RX;
  huart4.Init.HwFlowCtl = UART_HWCONTROL_NONE;
  huart4.Init.OverSampling = UART_OVERSAMPLING_16;
  if (HAL_UART_Init(&huart4) != HAL_OK)
  {
    Error_Handler();
  }
  /* USER CODE BEGIN UART4_Init 2 */

  /* USER CODE END UART4_Init 2 */

}

/**
  * @brief USART3 Initialization Function
  * @param None
  * @retval None
  */
static void MX_USART3_UART_Init(void)
{

  /* USER CODE BEGIN USART3_Init 0 */

  /* USER CODE END USART3_Init 0 */

  /* USER CODE BEGIN USART3_Init 1 */

  /* USER CODE END USART3_Init 1 */
  huart3.Instance = USART3;
  huart3.Init.BaudRate = 9600;
  huart3.Init.WordLength = UART_WORDLENGTH_8B;
  huart3.Init.StopBits = UART_STOPBITS_1;
  huart3.Init.Parity = UART_PARITY_NONE;
  huart3.Init.Mode = UART_MODE_TX_RX;
  huart3.Init.HwFlowCtl = UART_HWCONTROL_NONE;
  huart3.Init.OverSampling = UART_OVERSAMPLING_16;
  if (HAL_UART_Init(&huart3) != HAL_OK)
  {
    Error_Handler();
  }
  /* USER CODE BEGIN USART3_Init 2 */

  /* USER CODE END USART3_Init 2 */

}

/**
  * @brief GPIO Initialization Function
  * @param None
  * @retval None
  */
static void MX_GPIO_Init(void)
{
  GPIO_InitTypeDef GPIO_InitStruct = {0};

  /* GPIO Ports Clock Enable */
  __HAL_RCC_GPIOC_CLK_ENABLE();
  __HAL_RCC_GPIOF_CLK_ENABLE();
  __HAL_RCC_GPIOH_CLK_ENABLE();
  __HAL_RCC_GPIOA_CLK_ENABLE();
  __HAL_RCC_GPIOB_CLK_ENABLE();
  __HAL_RCC_GPIOG_CLK_ENABLE();

  /*Configure GPIO pin Output Level */
  HAL_GPIO_WritePin(GPIOB, LD1_Pin|LD3_Pin|LD2_Pin, GPIO_PIN_RESET);

  /*Configure GPIO pin Output Level */
  HAL_GPIO_WritePin(GPIOG, GPIO_PIN_0|GPIO_PIN_1|GPIO_PIN_2|GPIO_PIN_3
                          |GPIO_PIN_4|GPIO_PIN_5|USB_PowerSwitchOn_Pin, GPIO_PIN_RESET);

  /*Configure GPIO pin : USER_Btn_Pin */
  GPIO_InitStruct.Pin = USER_Btn_Pin;
  GPIO_InitStruct.Mode = GPIO_MODE_IT_RISING;
  GPIO_InitStruct.Pull = GPIO_NOPULL;
  HAL_GPIO_Init(USER_Btn_GPIO_Port, &GPIO_InitStruct);

  /*Configure GPIO pins : PF2 PF3 PF4 PF5 */
  GPIO_InitStruct.Pin = GPIO_PIN_2|GPIO_PIN_3|GPIO_PIN_4|GPIO_PIN_5;
  GPIO_InitStruct.Mode = GPIO_MODE_IT_RISING;
  GPIO_InitStruct.Pull = GPIO_NOPULL;
  HAL_GPIO_Init(GPIOF, &GPIO_InitStruct);

  /*Configure GPIO pins : LD1_Pin LD3_Pin LD2_Pin */
  GPIO_InitStruct.Pin = LD1_Pin|LD3_Pin|LD2_Pin;
  GPIO_InitStruct.Mode = GPIO_MODE_OUTPUT_PP;
  GPIO_InitStruct.Pull = GPIO_NOPULL;
  GPIO_InitStruct.Speed = GPIO_SPEED_FREQ_LOW;
  HAL_GPIO_Init(GPIOB, &GPIO_InitStruct);

  /*Configure GPIO pins : PG0 PG1 PG2 PG3
                           PG4 PG5 USB_PowerSwitchOn_Pin */
  GPIO_InitStruct.Pin = GPIO_PIN_0|GPIO_PIN_1|GPIO_PIN_2|GPIO_PIN_3
                          |GPIO_PIN_4|GPIO_PIN_5|USB_PowerSwitchOn_Pin;
  GPIO_InitStruct.Mode = GPIO_MODE_OUTPUT_PP;
  GPIO_InitStruct.Pull = GPIO_NOPULL;
  GPIO_InitStruct.Speed = GPIO_SPEED_FREQ_LOW;
  HAL_GPIO_Init(GPIOG, &GPIO_InitStruct);

  /*Configure GPIO pin : USB_OverCurrent_Pin */
  GPIO_InitStruct.Pin = USB_OverCurrent_Pin;
  GPIO_InitStruct.Mode = GPIO_MODE_INPUT;
  GPIO_InitStruct.Pull = GPIO_NOPULL;
  HAL_GPIO_Init(USB_OverCurrent_GPIO_Port, &GPIO_InitStruct);

  /*Configure GPIO pins : USB_SOF_Pin USB_ID_Pin USB_DM_Pin USB_DP_Pin */
  GPIO_InitStruct.Pin = USB_SOF_Pin|USB_ID_Pin|USB_DM_Pin|USB_DP_Pin;
  GPIO_InitStruct.Mode = GPIO_MODE_AF_PP;
  GPIO_InitStruct.Pull = GPIO_NOPULL;
  GPIO_InitStruct.Speed = GPIO_SPEED_FREQ_VERY_HIGH;
  GPIO_InitStruct.Alternate = GPIO_AF10_OTG_FS;
  HAL_GPIO_Init(GPIOA, &GPIO_InitStruct);

  /*Configure GPIO pin : USB_VBUS_Pin */
  GPIO_InitStruct.Pin = USB_VBUS_Pin;
  GPIO_InitStruct.Mode = GPIO_MODE_INPUT;
  GPIO_InitStruct.Pull = GPIO_NOPULL;
  HAL_GPIO_Init(USB_VBUS_GPIO_Port, &GPIO_InitStruct);

  /* EXTI interrupt init*/
  HAL_NVIC_SetPriority(EXTI2_IRQn, 0, 0);
  HAL_NVIC_EnableIRQ(EXTI2_IRQn);

  HAL_NVIC_SetPriority(EXTI3_IRQn, 0, 0);
  HAL_NVIC_EnableIRQ(EXTI3_IRQn);

  HAL_NVIC_SetPriority(EXTI4_IRQn, 0, 0);
  HAL_NVIC_EnableIRQ(EXTI4_IRQn);

  HAL_NVIC_SetPriority(EXTI9_5_IRQn, 0, 0);
  HAL_NVIC_EnableIRQ(EXTI9_5_IRQn);

}

/* USER CODE BEGIN 4 */

/* USER CODE END 4 */

/**
  * @brief  This function is executed in case of error occurrence.
  * @retval None
  */
void Error_Handler(void)
{
  /* USER CODE BEGIN Error_Handler_Debug */
  /* User can add his own implementation to report the HAL error return state */
  __disable_irq();
  while (1)
  {
  }
  /* USER CODE END Error_Handler_Debug */
}

#ifdef  USE_FULL_ASSERT
/**
  * @brief  Reports the name of the source file and the source line number
  *         where the assert_param error has occurred.
  * @param  file: pointer to the source file name
  * @param  line: assert_param error line source number
  * @retval None
  */
void assert_failed(uint8_t *file, uint32_t line)
{
  /* USER CODE BEGIN 6 */
  /* User can add his own implementation to report the file name and line number,
     ex: printf("Wrong parameters value: file %s on line %d\r\n", file, line) */
  /* USER CODE END 6 */
}
#endif /* USE_FULL_ASSERT */
