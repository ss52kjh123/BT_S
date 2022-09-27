/*
 * TcpServer.h
 *
 *  Created on: Aug 4, 2022
 *      Author: ydkim
 */

#ifndef INC_TCPSERVER_H_
#define INC_TCPSERVER_H_
#include "stdio.h"
#include "stdbool.h"
#include "stdlib.h"
#include "lwip/debug.h"
#include "lwip/stats.h"
#include "lwip/tcp.h"
#include "TcpServer.h"
#include "FlashMemory.h"
typedef struct TcpGetBool
{
	bool a1;
	bool b1;
	bool c1;
}TcpGetBool;
void tcp_echoserver_init(uint16_t setPort);
int TcpCounterCallBack(uint16_t a,uint16_t b);
TcpGetBool Set_TcpVariable_BOOL(bool a1,bool b1, bool c1);
#endif /* INC_TCPSERVER_H_ */
