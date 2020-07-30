using System.Collections.Generic;
using Ywxt.Cens.Core.Utils;

namespace Ywxt.Cens.Core.Cpu.Instruction
{
    public sealed class RorInstruction : IInstruction
    {
        public IReadOnlyDictionary<byte, AddressingMode> OpCodes { get; }
            = new Dictionary<byte, AddressingMode>
            {
                {0x6A, AddressingMode.AccumulatorAddressingMode},
                {0x66, AddressingMode.ZeroPageAddressingMode},
                {0x76, AddressingMode.ZeroPageXAddressingMode},
                {0x6E, AddressingMode.AbsoluteAddressingMode},
                {0x7E, AddressingMode.AbsoluteXAddressingMode}
            };

        public int Invoke(ICpu cpu, byte instruction, ushort address, bool pageCrossed)
        {
            var data = this.ReadData(address, cpu, instruction);
            var @new = (byte) ((data >> 1) | ((byte) (cpu.Registers.P & PFlags.C) << 7));
            this.WriteData(address, @new, cpu, instruction);


            cpu.Registers.SetCFlag((data & 1) == 1);
            cpu.Registers.SetZAndNFlags(@new);

            return instruction switch
            {
                0x6A => 2,
                0x66 => 5,
                0x76 => 6,
                0x6E => 6,
                0x7E => 7,
                _ => 0
            };
        }
    }
}