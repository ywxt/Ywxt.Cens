using System.Collections.Generic;
using Ywxt.Cens.Core.Utils;

namespace Ywxt.Cens.Core.Cpu.Instruction
{
    public sealed class AslInstruction : IInstruction
    {
        public IReadOnlyDictionary<byte, AddressingMode> OpCodes { get; }
            = new Dictionary<byte, AddressingMode>
            {
                {0x0A, AddressingMode.AccumulatorAddressingMode},
                {0x06, AddressingMode.ZeroPageAddressingMode},
                {0x16, AddressingMode.ZeroPageXAddressingMode},
                {0x0E, AddressingMode.AbsoluteAddressingMode},
                {0x1E, AddressingMode.AbsoluteXAddressingMode}
            };

        public int Invoke(ICpu cpu, byte instruction, ushort address, bool pageCrossed)
        {
            var data = this.ReadData(address, cpu, instruction);
            var @new = (byte) (data << 1);
            this.WriteData(address, @new, cpu, instruction);

            cpu.Registers.SetCFlag(data >> 7 == 1);
            cpu.Registers.SetZAndNFlags(@new);

            return instruction switch
            {
                0x0A => 2,
                0x06 => 5,
                0x16 => 6,
                0x0E => 6,
                0x1E => 7,
                _ => 0
            };
        }
    }
}