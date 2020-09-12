using System.Collections.Generic;
using Ywxt.Cens.Core.Utils;

namespace Ywxt.Cens.Core.Cpu.Instruction
{
    public sealed class LsrInstruction : IInstruction
    {
        public IReadOnlyDictionary<byte, (AddressingMode mode, InstructionType insType, int cycles)> OpCodes { get; }
            = new Dictionary<byte, (AddressingMode, InstructionType, int)>
            {
                {0x4A, (AddressingMode.AccumulatorAddressingMode, InstructionType.Common, 2)},
                {0x46, (AddressingMode.ZeroPageAddressingMode, InstructionType.Common, 5)},
                {0x56, (AddressingMode.ZeroPageXAddressingMode, InstructionType.Common, 6)},
                {0x4E, (AddressingMode.AbsoluteAddressingMode, InstructionType.Common, 6)},
                {0x5E, (AddressingMode.AbsoluteXAddressingMode, InstructionType.Common, 7)}
            };

        public int Invoke(ICpu cpu, byte instruction, ushort address)
        {
            var data = this.ReadData(address, cpu, instruction);
            var old = data;
            var @new = (byte) (data >> 1);
            this.WriteData(address, @new, cpu, instruction);

            cpu.Registers.SetCFlag((old & 1) == 1);
            cpu.Registers.SetZAndNFlags(@new);

            return 0;
        }
    }
}