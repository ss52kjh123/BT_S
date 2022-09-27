/*
 * Basic.c
 *
 *  Created on: Aug 3, 2022
 *      Author: ydkim
 */
#include "main.h"
#include "Basic.h"
void Delay_us(int time_us){
              if(time_us>1){
                       uint32_t count=time_us*8-6;
                       while(count--);
               }else{
                       uint32_t count=3;
                       while(count--);
                       }
        }

void Delay_ms(int time_ms)			/* time delay for ms in 72MHz */
{
  while(time_ms--)
    Delay_us(1000);
}

void Bluetooth_ATMode(void)
{
	HAL_GPIO_WritePin(GPIOG, GPIO_PIN_1, GPIO_PIN_SET);
}

void Bluetooth_PairingMode(void)
{
	HAL_GPIO_WritePin(GPIOG, GPIO_PIN_1, GPIO_PIN_RESET);
}
void Bluetooth_PowerOn(void)
{
	HAL_GPIO_WritePin(GPIOG, GPIO_PIN_0, GPIO_PIN_SET);
}
void Bluetooth_PowerOff(void)
{
	HAL_GPIO_WritePin(GPIOG, GPIO_PIN_0, GPIO_PIN_RESET);
}




/////////////////////////////////////////////////////////////////////////
void ST_LED_ON_1()
{
	HAL_GPIO_WritePin(GPIOB, GPIO_PIN_0, GPIO_PIN_SET);
}

void ST_LED_ON_2()
{
	HAL_GPIO_WritePin(GPIOB, GPIO_PIN_7, GPIO_PIN_SET);
}
void ST_LED_ON_3()
{
	HAL_GPIO_WritePin(GPIOB, GPIO_PIN_14, GPIO_PIN_SET);
}

void ST_LED_OFF_1()
{
	HAL_GPIO_WritePin(GPIOB, GPIO_PIN_0, GPIO_PIN_RESET);
}

void ST_LED_OFF_2()
{
	HAL_GPIO_WritePin(GPIOB, GPIO_PIN_7, GPIO_PIN_RESET);
}
void ST_LED_OFF_3()
{
	HAL_GPIO_WritePin(GPIOB, GPIO_PIN_14, GPIO_PIN_RESET);
}


void MOSTEC_LED_ON_1()
{
	HAL_GPIO_WritePin(GPIOG, GPIO_PIN_2, GPIO_PIN_SET);
}

void MOSTEC_LED_ON_2()
{
	HAL_GPIO_WritePin(GPIOG, GPIO_PIN_3, GPIO_PIN_SET);
}

void MOSTEC_LED_ON_3()
{
	HAL_GPIO_WritePin(GPIOG, GPIO_PIN_4, GPIO_PIN_SET);
}

void MOSTEC_LED_ON_4()
{
	HAL_GPIO_WritePin(GPIOG, GPIO_PIN_5, GPIO_PIN_SET);
}


void MOSTEC_LED_OFF_1()
{
	HAL_GPIO_WritePin(GPIOG, GPIO_PIN_2, GPIO_PIN_RESET);
}

void MOSTEC_LED_OFF_2()
{
	HAL_GPIO_WritePin(GPIOG, GPIO_PIN_3, GPIO_PIN_RESET);
}

void MOSTEC_LED_OFF_3()
{
	HAL_GPIO_WritePin(GPIOG, GPIO_PIN_4, GPIO_PIN_RESET);
}

void MOSTEC_LED_OFF_4()
{
	HAL_GPIO_WritePin(GPIOG, GPIO_PIN_5, GPIO_PIN_RESET);
}

void Init_Complite_Led_Offeration(){
	MOSTEC_LED_ON_1();
	Delay_ms(300);
	MOSTEC_LED_OFF_1();

	MOSTEC_LED_ON_2();
	Delay_ms(300);
	MOSTEC_LED_OFF_2();

	MOSTEC_LED_ON_3();
	Delay_ms(300);
	MOSTEC_LED_OFF_3();
}
