using System;
using Ywxt.Cens.Core.Rom;

namespace Ywxt.Cens.Core.Cpu
{
    public class CpuBus : IBus
    {
        public const ushort AddressCpuRamEnd = 0x1FFF;

        public const ushort AddressIoRegistersStart = 0x2000;
        public const ushort AddressIoRegistersEnd = 0x401F;


        public Cartridge Cartridge { get; }

        public Memory<byte> Ram { get; }


        public byte ReadByte(ushort address)
        {
            if (address <= AddressCpuRamEnd)
            {
                return Ram.Span[address];
            }

            // IO 寄存器，暂不处理
            if (address >= AddressIoRegistersStart && address <= AddressIoRegistersEnd)
            {
                return 0;
            }

            // 卡带数据
            return Cartridge.Mapper.Read(address);
        }

        public void WriteByte(ushort address, byte data)
        {
            if (address <= AddressCpuRamEnd)
            {
                Ram.Span[address] = data;
            }

            // IO 寄存器，暂不处理
            if (address >= AddressIoRegistersStart && address <= AddressIoRegistersEnd)
            {
                return;
            }

            // 卡带数据
            Cartridge.Mapper.Write(address, data);
        }
    }
}