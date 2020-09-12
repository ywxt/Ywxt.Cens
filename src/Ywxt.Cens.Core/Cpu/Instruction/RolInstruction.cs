using System.Collections.Generic;
using Ywxt.Cens.Core.Utils;

namespace Ywxt.Cens.Core.Cpu.Instruction
{
    public sealed class RolInstruction : IInstruction
    {
        public IReadOnlyDictionary<byte, (AddressingMode mode, InstructionType insType, int cycles)> OpCodes { get; }
            = new Dictionary<byte, (AddressingMode, InstructionType, int)>
            {
                {0x2A, (AddressingMode.AccumulatorAddressingMode, InstructionType.Common, 2)},
                {0x26, (AddressingMode.ZeroPageAddressingMode, InstructionType.Common, 5)},
                {0x36, (AddressingMode.ZeroPageXAddressingMode, InstructionType.Common, 6)},
                {0x2E, (AddressingMode.AbsoluteAddressingMode, InstructionType.Common, 6)},
                {0x3E, (AddressingMode.AbsoluteXAddressingMode, InstructionType.Common, 7)}
            };

        public int Invoke(ICpu cpu, byte instruction, ushort address)
        {
            var data = this.ReadData(address, cpu, instruction);
            var @new = (byte) ((data << 1) | (byte) (cpu.Registers.P & PFlags.C));
            this.WriteData(address, @new, cpu, instruction);

            cpu.Registers.SetCFlag(data >> 7 == 1);
            cpu.Registers.SetZAndNFlags(@new);

            return 0;
        }
    }
}