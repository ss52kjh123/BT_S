/*
 * FlashMemory.h
 *
 *  Created on: 2022. 8. 16.
 *      Author: ydkim
 */

#ifndef INC_FLASHMEMORY_H_
#define INC_FLASHMEMORY_H_
#include "main.h"
#include "stm32f4xx_hal_flash.h"
#include "FlashMemory.h"

void FlashMemoryWrite(short _ip1,short _ip2,short _ip3,short _ip4,short _port);
void FlashMemoryRead(void);
void For_FlashMemory_Int_To_Byte(short _ip1,short _ip2,short _ip3,short _ip4,short _port);
static void MY_FLASH_EraseSector(void);

#endif /* INC_FLASHMEMORY_H_ */
