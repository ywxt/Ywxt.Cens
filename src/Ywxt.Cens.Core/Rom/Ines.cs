using System;
using Ywxt.Cens.Core.Exceptions;

namespace Ywxt.Cens.Core.Rom
{
    /// <summary>
    /// INES 文件
    /// </summary>
    public class Ines
    {
        public Header Header { get; }

        public Memory<byte> Trainer { get; }

        public Memory<byte> Prg { get; }

        public Memory<byte> Chr { get; }

        public Ines(Memory<byte> rom)
        {
            if (rom.Length < Header.HeaderSize)
            {
                throw new ArgumentException("INES文件长度必须不小于16");
            }

            Header = new Header(rom.Span[..Header.HeaderSize]);
            var position = Rom.Header.HeaderSize;
            if (Header.Trainer())
            {
                if (rom.Length < position + Header.TrainerSize)
                {
                    throw new RomException("NES文件损坏");
                }

                Trainer = rom[position..(position + Header.TrainerSize)];
                position += Header.TrainerSize;
            }
            else
            {
                Trainer = new Memory<byte>();
            }

            if (rom.Length < position + Header.PrgUnitSize * Header.PrgSize)
            {
                throw new RomException("NES文件损坏");
            }

            Prg = rom[position..(position + Header.PrgUnitSize * Header.PrgSize)];
            position += Header.PrgUnitSize * Header.PrgSize;
            if (rom.Length < position + Header.ChrUnitSize * Header.ChrSize)
            {
                throw new RomException("NES文件损坏");
            }
            Chr = rom[position..(position + Header.ChrUnitSize * Header.ChrSize)];
            // position += Header.ChrUnitSize * Header.ChrSize;
        }
    }
}