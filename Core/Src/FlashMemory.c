/*
 * FlashMemory.c
 *
 *  Created on: 2022. 8. 16.
 *      Author: ydkim
 */

#include "main.h"
#include "Basic.h"
#include "FlashMemory.h"
/*-------------------------  Flash Memory -2022 06.15 -kyd ----------------------*/
uint32_t Flash_Address = 0x081E0000;  //sector 23  -128KB
uint8_t flash_write_data[20];
extern uint8_t flash_read_data[20];


short iFlashMemory_SavedCounter;

void FlashMemoryWrite(short _ip1,short _ip2,short _ip3,short _ip4,short _port)
{
	 short ip1,ip2,ip3,ip4,port;
	 ip1 =_ip1; ip2=_ip2; ip3=_ip3; ip4=_ip4; port=_port;
	 Flash_Address = 0x081E0000;  //sector 23  -128KB  쓰기 전 초기화
	 MY_FLASH_EraseSector();
	 HAL_FLASH_Unlock();

	  For_FlashMemory_Int_To_Byte(ip1,ip2,ip3,ip4,port);

	  for(int i=0; i<=19; i++)
	  {
		  HAL_FLASH_Program(FLASH_TYPEPROGRAM_BYTE, Flash_Address, ((uint8_t*)flash_write_data)[i]);
		  Flash_Address++;
	  }

	  HAL_FLASH_Lock();


}
void FlashMemoryRead(void){
	  //read
	//uint8_t value = 0x081E0000;  //sector 23  -128KB  기 전 초기화
	 // HAL_FLASH_Unlock();
	Flash_Address = 0x081E0000;
	  for(int i=0; i<=19; i++)
	  {

		  *((uint8_t *)flash_read_data + i) = *(uint8_t *)Flash_Address;

		  Flash_Address++;
	  }
	 // HAL_FLASH_Lock();
}
void For_FlashMemory_Int_To_Byte(short _ip1,short _ip2,short _ip3,short _ip4,short _port)
{

	short ipAdd_1,ipAdd_2,ipAdd_3,ipAdd_4,port;
	ipAdd_1 =_ip1; ipAdd_2=_ip2; ipAdd_3=_ip3; ipAdd_4=_ip4; port=_port;
	iFlashMemory_SavedCounter++;
	flash_write_data[0] = iFlashMemory_SavedCounter & 0xFF;
	flash_write_data[1] = (iFlashMemory_SavedCounter>> 8) & 0xff;

	flash_write_data[2] = (ipAdd_1 & 0xff);       //Low Byte
	flash_write_data[3] = (ipAdd_1 >> 8) & 0xFF;  //High Byte

	flash_write_data[4] = (ipAdd_2 & 0xff);       //Low Byte
	flash_write_data[5] = (ipAdd_2 >> 8) & 0xFF;  //High Byte

	flash_write_data[6] = (ipAdd_3 & 0xff);       //Low Byte
	flash_write_data[7] = (ipAdd_3 >> 8) & 0xFF;  //High Byte

	flash_write_data[8] = (ipAdd_4 & 0xff);       //Low Byte
	flash_write_data[9] = (ipAdd_4 >> 8) & 0xFF;  //High Byte

	flash_write_data[10] = (port & 0xff);       //Low Byte
	flash_write_data[11] = (port >> 8) & 0xFF;  //High Byte

}
static void MY_FLASH_EraseSector(void)
{
	HAL_FLASH_Unlock();
	//Erase the required Flash sector
	FLASH_Erase_Sector(23, FLASH_VOLTAGE_RANGE_3);
	HAL_FLASH_Lock();
}
