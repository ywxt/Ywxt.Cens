using System.Collections.Generic;

namespace Ywxt.Cens.Core.Cpu.Instruction
{
    public sealed class StaInstruction : IInstruction
    {
        public IReadOnlyDictionary<byte, (AddressingMode mode, InstructionType insType, int cycles)> OpCodes { get; }
            = new Dictionary<byte, (AddressingMode, InstructionType, int)>
            {
                {0x85, (AddressingMode.ZeroPageAddressingMode, InstructionType.Common, 3)},
                {0x95, (AddressingMode.ZeroPageXAddressingMode, InstructionType.Common, 4)},
                {0x8D, (AddressingMode.AbsoluteAddressingMode, InstructionType.Common, 4)},
                {0x9D, (AddressingMode.AbsoluteXAddressingMode, InstructionType.Common, 5)},
                {0x99, (AddressingMode.AbsoluteYAddressingMode, InstructionType.Common, 5)},
                {0x81, (AddressingMode.IndirectXAddressingMode, InstructionType.Common, 6)},
                {0x91, (AddressingMode.IndirectYAddressingMode, InstructionType.Common, 6)}
            };

        public int Invoke(ICpu cpu, byte instruction, ushort address)
        {
            cpu.Bus.WriteByte(address, cpu.CpuRegisters.A);
            return 0;
        }
    }
}