using System.Collections.Generic;
using Ywxt.Cens.Core.Utils;

namespace Ywxt.Cens.Core.Cpu.Instruction
{
    public sealed class LsrInstruction : IInstruction
    {
        public IReadOnlyDictionary<byte, AddressingMode> OpCodes { get; }
            = new Dictionary<byte, AddressingMode>
            {
                {0x4A, AddressingMode.AccumulatorAddressingMode},
                {0x46, AddressingMode.ZeroPageAddressingMode},
                {0x56, AddressingMode.ZeroPageXAddressingMode},
                {0x4E, AddressingMode.AbsoluteAddressingMode},
                {0x5E, AddressingMode.AbsoluteXAddressingMode}
            };

        public int Invoke(ICpu cpu, byte instruction, ushort address, bool pageCrossed)
        {
            var data = this.ReadData(address, cpu, instruction);
            var old = data;
            var @new = (byte) (data >> 1);
            this.WriteData(address, @new, cpu, instruction);

            cpu.Registers.SetCFlag((old & 1) == 1);
            cpu.Registers.SetZAndNFlags(@new);

            return instruction switch
            {
                0x4A => 2,
                0x46 => 5,
                0x56 => 6,
                0x4E => 6,
                0x5E => 7,
                _ => 0
            };
        }
    }
}