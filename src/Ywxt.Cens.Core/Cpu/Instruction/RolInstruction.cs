using System.Collections.Generic;
using Ywxt.Cens.Core.Utils;

namespace Ywxt.Cens.Core.Cpu.Instruction
{
    public sealed class RolInstruction : IInstruction
    {
        public IReadOnlyDictionary<byte, AddressingMode> OpCodes { get; }
            = new Dictionary<byte, AddressingMode>
            {
                {0x2A, AddressingMode.AccumulatorAddressingMode},
                {0x26, AddressingMode.ZeroPageAddressingMode},
                {0x36, AddressingMode.ZeroPageXAddressingMode},
                {0x2E, AddressingMode.AbsoluteAddressingMode},
                {0x3E, AddressingMode.AbsoluteXAddressingMode}
            };

        public int Invoke(ICpu cpu, byte instruction, ushort address, bool pageCrossed)
        {
            var data = this.ReadData(address, cpu, instruction);
            var @new = (byte) ((data << 1) | (byte) (cpu.Registers.P & PFlags.C));
            this.WriteData(address, @new, cpu, instruction);

            cpu.Registers.SetCFlag(data >> 7 == 1);
            cpu.Registers.SetZAndNFlags(@new);

            return instruction switch
            {
                0x2A => 2,
                0x26 => 5,
                0x36 => 6,
                0x2E => 6,
                0x3E => 7,
                _ => 0
            };
        }
    }
}