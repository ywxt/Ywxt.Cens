using System;

namespace Ywxt.Cens.Core.Rom
{
    public sealed class Mapper0 : IMapper
    {
        public const ushort AddressChrBankEnd = 0x2000 - 1;
        public const ushort AddressExpansionRomStart = 0x4020;
        public const ushort AddressExpansionRomEnd = 0x5FFF;
        public const ushort AddressPrgRamBankStart = 0x6000;
        public const ushort AddressPrgRamBankEnd = 0x8000 - 1;
        public const ushort AddressPrgBankFirstStart = 0x8000;
        public const ushort AddressPrgBankFirstEnd = 0xC000 - 1;
        public const ushort AddressPrgBankSecondStart = 0xC000;


        /// <summary>
        /// 卡带上的 8k SRAM，游戏存档
        /// </summary>
        private Memory<byte> Sram { get; }

        /// <summary>
        /// 程序数据
        /// </summary>
        private Memory<byte> Prg { get; }

        /// <summary>
        /// Chr 镜像
        /// </summary>
        private Memory<byte> Chr { get; }

        /// <summary>
        /// 是否镜像
        /// </summary>
        private bool Mirroring { get; }


        public Mapper0(Memory<byte> prg, Memory<byte> sram, Memory<byte> chr)
        {
            Prg = prg;
            Sram = sram;
            Chr = chr.IsEmpty ? new byte[0x2000] : Chr;
            Mirroring = prg.Length == Header.PrgUnitSize;
        }


        public byte Read(ushort address)
        {
            // CHR 位于 PPU
            if (address <= AddressChrBankEnd)
            {
                return Chr.Span[address];
            }

            if (address >= AddressExpansionRomStart && address <= AddressExpansionRomEnd)
            {
                // Mapper扩展，一般用不到
                return 0;
            }

            if (address >= AddressPrgRamBankStart && address <= AddressPrgRamBankEnd)
            {
                return Sram.Span[address - AddressPrgRamBankStart];
            }

            if (address >= AddressPrgBankFirstStart && address <= AddressPrgBankFirstEnd)
            {
                return Prg.Span[address - AddressPrgBankFirstStart];
            }

            if (address >= AddressPrgBankSecondStart)
            {
                return Mirroring
                    ? Prg.Span[address - AddressPrgBankSecondStart]
                    : Prg.Span[address - AddressPrgBankFirstStart];
            }

            // 其他情况由CPU处理
            return 0;
        }

        public void Write(ushort address, byte data)
        {
            // CHR位于PPU
            if (address <= AddressChrBankEnd)
            {
                Chr.Span[address] = data;
            }
            if (address >= AddressExpansionRomStart && address <= AddressExpansionRomEnd)
            {
                // Mapper扩展，一般用不到
                return;
            }
            if (address >= AddressPrgRamBankStart && address <= AddressPrgRamBankEnd)
            {
                Sram.Span[address - AddressPrgRamBankStart] = data;
            }

            if (address >= AddressPrgBankFirstStart && address <= AddressPrgBankFirstEnd)
            {
                Prg.Span[address - AddressPrgBankFirstStart] = data;
            }

            if (address >= AddressPrgBankSecondStart)
            {
                if (Mirroring)
                {
                    Prg.Span[address - AddressPrgBankSecondStart] = data;
                }
                else
                {
                    Prg.Span[address - AddressPrgBankFirstStart] = data;
                }
            }

            // 其他情况由CPU处理
        }
    }
}