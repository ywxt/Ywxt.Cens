using System.Collections.Generic;
using Ywxt.Cens.Core.Utils;

namespace Ywxt.Cens.Core.Cpu.Instruction
{
    public sealed class AslInstruction : IInstruction
    {
        public IReadOnlyDictionary<byte, (AddressingMode mode, InstructionType insType, int cycles)> OpCodes { get; }
            = new Dictionary<byte, (AddressingMode, InstructionType, int)>
            {
                {0x0A, (AddressingMode.AccumulatorAddressingMode, InstructionType.Common, 2)},
                {0x06, (AddressingMode.ZeroPageAddressingMode, InstructionType.Common, 5)},
                {0x16, (AddressingMode.ZeroPageXAddressingMode, InstructionType.Common, 6)},
                {0x0E, (AddressingMode.AbsoluteAddressingMode, InstructionType.Common, 6)},
                {0x1E, (AddressingMode.AbsoluteXAddressingMode, InstructionType.Common, 7)}
            };

        public int Invoke(ICpu cpu, byte instruction, ushort address)
        {
            var data = this.ReadData(address, cpu, instruction);
            var @new = (byte) (data << 1);
            this.WriteData(address, @new, cpu, instruction);

            cpu.CpuRegisters.SetCFlag(data >> 7 == 1);
            cpu.CpuRegisters.SetZAndNFlags(@new);

            return 0;
        }
    }
}