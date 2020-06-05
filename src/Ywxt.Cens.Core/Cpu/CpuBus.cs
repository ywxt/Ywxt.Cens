using System;
using Ywxt.Cens.Core.Rom;

namespace Ywxt.Cens.Core.Cpu
{
    public sealed class CpuBus : IBus
    {
        public const ushort AddressStackStart = 0x0100;
        public const ushort AddressStackEnd = 0x0200 - 1;

        public const ushort AddressCpuRamEnd = 0x1FFF;

        public const ushort AddressIoRegistersStart = 0x2000;
        public const ushort AddressIoRegistersEnd = 0x401F;


        public Cartridge Cartridge { get; }

        public Memory<byte> Ram { get; }


        public CpuBus(Cartridge cartridge)
        {
            Cartridge = cartridge;
            Ram = new Memory<byte>(new byte[0x0800]);
        }


        public byte ReadByte(ushort address)
        {
            if (address <= AddressCpuRamEnd)
            {
                // 镜像
                return Ram.Span[address & 0x07FF];
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
                Ram.Span[address & 0x07FF] = data;
                return;
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