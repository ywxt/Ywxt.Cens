using Ywxt.Cens.Core.Cpu;

namespace Ywxt.Cens.Core.Ppu
{
    /// <summary>
    /// The PPU exposes eight memory-mapped registers to the CPU. These nominally sit at $2000 through $2007 in the CPU's address space, but because they're incompletely decoded, they're mirrored in every 8 bytes from $2008 through $3FFF, so a write to $3456 is the same as a write to $2006.
    /// <list type="table">
    /// <listheader>
    /// <term> Common Name </term>
    /// <term> Address
    /// </term>
    /// <term> Bits
    /// </term>
    /// <term> Notes
    /// </term></listheader>
    /// <item>
    /// <term> PPUCTRL
    /// </term>
    /// <term> $2000
    /// </term>
    /// <term> VPHB SINN </term>
    /// <term> NMI enable (V), PPU master/slave (P), sprite height (H), background tile select (B), sprite tile select (S), increment mode (I), nametable select (NN)
    /// </term></item>
    /// <item>
    /// <term> PPUMASK
    /// </term>
    /// <term> $2001
    /// </term>
    /// <term> BGRs bMmG </term>
    /// <term> color emphasis (BGR), sprite enable (s), background enable (b), sprite left column enable (M), background left column enable (m), greyscale (G)
    /// </term></item>
    /// <item>
    /// <term> PPUSTATUS
    /// </term>
    /// <term> $2002
    /// </term>
    /// <term> VSO- ---- </term>
    /// <term> vblank (V), sprite 0 hit (S), sprite overflow (O); read resets write pair for $2005/$2006
    /// </term></item>
    /// <item>
    /// <term> OAMADDR
    /// </term>
    /// <term> $2003
    /// </term>
    /// <term> aaaa aaaa </term>
    /// <term> OAM read/write address
    /// </term></item>
    /// <item>
    /// <term> OAMDATA
    /// </term>
    /// <term> $2004
    /// </term>
    /// <term> dddd dddd </term>
    /// <term> OAM data read/write
    /// </term></item>
    /// <item>
    /// <term> PPUSCROLL
    /// </term>
    /// <term> $2005
    /// </term>
    /// <term> xxxx xxxx </term>
    /// <term> fine scroll position (two writes: X scroll, Y scroll)
    /// </term></item>
    /// <item>
    /// <term> PPUADDR
    /// </term>
    /// <term> $2006
    /// </term>
    /// <term> aaaa aaaa </term>
    /// <term> PPU read/write address (two writes: most significant byte, least significant byte)
    /// </term></item>
    /// <item>
    /// <term> PPUDATA
    /// </term>
    /// <term> $2007
    /// </term>
    /// <term> dddd dddd </term>
    /// <term> PPU data read/write
    /// </term></item>
    /// <item>
    /// <term> OAMDMA
    /// </term>
    /// <term> $4014
    /// </term>
    /// <term> aaaa aaaa</term>
    /// <term> OAM DMA high address
    /// </term></item></list>
    /// </summary>
    public sealed class PpuRegisters
    {
        private readonly IBus _cpuBus;

        public const ushort PPUCTRL = 0x2000;

        public PpuRegisters(IBus cpuBus)
        {
            _cpuBus = cpuBus;
        }

        public ushort BaseNametableAddress
        {
            get => (_cpuBus.ReadByte(PPUCTRL) & 0b00000011) switch
            {
                0 => 0x2000,
                1 => 0x2400,
                2 => 0x2800,
                3 => 0x2C00,
                _ => 0x2000
            };
            set
            {
                var address = value switch
                {
                    0x2000 => 0,
                    0x2400 => 1,
                    0x2800 => 2,
                    0x2C00 => 3,
                    _ => 0
                };
                _cpuBus.WriteByte(PPUCTRL, (byte) ((_cpuBus.ReadByte(PPUCTRL) & 0b11111100) | address));
            }
        }
    }
}