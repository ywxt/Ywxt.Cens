using System.Collections.Generic;

namespace Ywxt.Cens.Core.Cpu.Instruction
{
    public sealed class StyInstruction : IInstruction
    {
        public IReadOnlyDictionary<byte, (AddressingMode mode, InstructionType insType, int cycles)> OpCodes { get; }
            = new Dictionary<byte, (AddressingMode, InstructionType, int)>
            {
                {0x84, (AddressingMode.ZeroPageAddressingMode, InstructionType.Common, 3)},
                {0x94, (AddressingMode.ZeroPageXAddressingMode, InstructionType.Common, 4)},
                {0x8C, (AddressingMode.AbsoluteAddressingMode, InstructionType.Common, 4)}
            };

        public int Invoke(ICpu cpu, byte instruction, ushort address)
        {
            cpu.Bus.WriteByte(address, cpu.Registers.Y);
            return 0;
        }
    }
}