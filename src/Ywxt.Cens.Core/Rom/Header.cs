using System;
using System.Collections.ObjectModel;
using Ywxt.Cens.Core.Exceptions;
using Ywxt.Cens.Core.Ppu;

namespace Ywxt.Cens.Core.Rom
{
    /// <summary>
    /// NES 文件头
    /// </summary>
    public class Header
    {
        public static readonly ReadOnlyMemory<byte> NesSign = new byte[] {0x4E, 0x45, 0x53, 0x1A};

        public const int SignSize = 4;
        public const int PrgUnitSize = 16 * 1024;
        public const int ChrUnitSize = 8 * 1024;
        public const int UnusedSize = 5;
        public const int HeaderSize = 16;
        public const int TrainerSize = 512;

        /// <summary>
        /// NES 文件标志，值为 $4E $45 $53 $1A
        /// </summary>
        public ReadOnlyMemory<byte> Sign { get; }

        /// <summary>
        /// PRG 大小，每个单元16k
        /// </summary>
        public byte PrgSize { get; }

        /// <summary>
        /// CHR 大小，每个单元8k
        /// </summary>
        public byte ChrSize { get; }

        /// <summary>
        /// - 0: Mirroring
        /// <br/>
        ///   0: 水平镜像<br/>
        ///   1: 垂直镜像<br/>
        /// <br/>
        /// - 1: 卡带上没有带电池的SRAM
        /// <br/>
        /// - 2: Trainer标志
        /// <br/>
        /// - 3: 忽略Mirroring，启用4-Screen模式
        /// <br/>
        /// - 4-7: Mapper 号的低4位
        /// </summary>
        public byte Flags6 { get; }

        /// <summary>
        /// - 0: VS Unisystem  <br/>
        /// - 1: PlayChoice-10  <br/>
        /// - 2-3: 如果等于2，则为NES 2.0格式，Flags8-10按照2.0格式读取  <br/>
        /// - 4-7: Mapper 号的高4位  <br/>
        /// </summary>
        public byte Flags7 { get; }

        /// <summary>
        /// RPG RAM 大小
        /// <br/>
        /// 以下 8-10 NES 2.0 格式均不同，但此程序不支持2.0，故不再列出
        /// </summary>
        public byte Flags8 { get; }

        /// <summary>
        ///  TV 系统<br/>
        ///  - 0: NTSC<br/>
        ///  - 1: PAL<br/>
        /// </summary>
        public byte Flags9 { get; }

        /// <summary>
        /// 默认为0
        /// </summary>
        public byte Flags10 { get; }

        /// <summary>
        /// 默认为0，大小5
        /// </summary>
        public byte[] Unused { get; }

        public static bool IsNesRom(ReadOnlySpan<byte> header)
        {
            return header[..SignSize].SequenceEqual(NesSign.Span);
        }

        public Header(ReadOnlySpan<byte> header)
        {
            if (!IsNesRom(header))
            {
                throw new RomException("无效NES文件");
            }

            Sign = NesSign;
            PrgSize = header[4];
            ChrSize = header[5];
            Flags6 = header[6];
            Flags7 = header[7];
            Flags8 = header[8];
            Flags9 = header[9];
            Flags10 = header[10];
            Unused = header[11..16].ToArray();
        }

        public Mirroring Mirroring()
        {
            if (((Flags6 >> 3) & 1) == 1)
            {
                return Ppu.Mirroring.FourScreen;
            }

            return ((Flags6 & 10) == 1) ? Ppu.Mirroring.Vertical : Ppu.Mirroring.Horizontal;
        }

        public bool BatteryBacked()
        {
            return ((Flags6 >> 1) & 1) == 1;
        }

        public bool Trainer()
        {
            return ((Flags6 >> 2) & 1) == 1;
        }

        public byte MapperNumber()
        {
            var low = Flags6 >> 4;
            var high = Flags7 >> 4;
            return (byte) ((high << 4) | low);
        }

        public bool VsUnisystem()
        {
            return (Flags7 & 1) == 1;
        }

        public bool PlayChoice10()
        {
            return ((Flags7 >> 1) & 1) == 1;
        }

        public bool Nes2Format()
        {
            return ((Flags7 >> 2) & 0b11) == 0b10;
        }

        public byte PrgRamSize()
        {
            if (Nes2Format())
            {
                throw new InvalidOperationException("此操作不支持NES 2.0");
            }

            return Flags8;
        }
    }
}