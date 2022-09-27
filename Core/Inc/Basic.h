/*
 * Basic.h
 *
 *  Created on: Jun 20, 2021
 *      Author: ydkim
 */

#ifndef INC_BASIC_H_
#define INC_BASIC_H_
#include "main.h"
#include "Basic.h"

void Delay_us(int time_us);
void Delay_ms(int time_ms);

void ST_LED_ON_1(void);
void ST_LED_ON_2(void);
void ST_LED_ON_3(void);
void MOSTEC_LED_ON_1(void);
void MOSTEC_LED_ON_2(void);
void MOSTEC_LED_ON_3(void);
void MOSTEC_LED_ON_4(void);
void ST_LED_OFF_1(void);
void ST_LED_OFF_2(void);
void ST_LED_OFF_3(void);
void MOSTEC_LED_OFF_1(void);
void MOSTEC_LED_OFF_2(void);
void MOSTEC_LED_OFF_3(void);
void MOSTEC_LED_OFF_4(void);

//블루투수 ON/OFF
void Bluetooth_ATMode(void);
void Bluetooth_PairingMode(void);
void Bluetooth_PowerOn(void);
void Bluetooth_PowerOff(void);

//Init Complite Led On
void Init_Complite_Led_Offeration(void);
#endif /* INC_BASIC_H_ */
