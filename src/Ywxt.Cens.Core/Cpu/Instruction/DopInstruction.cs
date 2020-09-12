using System.Collections.Generic;

namespace Ywxt.Cens.Core.Cpu.Instruction
{
    /// <summary>
    /// 扩展指令 DOP(double NOP)
    /// </summary>
    public sealed class DopInstruction : IInstruction
    {
        public IReadOnlyDictionary<byte, (AddressingMode mode, InstructionType insType, int cycles)> OpCodes { get; }
            = new Dictionary<byte, (AddressingMode, InstructionType, int)>
            {
                {0x04, (AddressingMode.ZeroPageAddressingMode, InstructionType.Common, 3)},
                {0x14, (AddressingMode.ZeroPageXAddressingMode, InstructionType.Common, 4)},
                {0x34, (AddressingMode.ZeroPageXAddressingMode, InstructionType.Common, 4)},
                {0x44, (AddressingMode.ZeroPageAddressingMode, InstructionType.Common, 3)},
                {0x54, (AddressingMode.ZeroPageXAddressingMode, InstructionType.Common, 4)},
                {0x64, (AddressingMode.ZeroPageAddressingMode, InstructionType.Common, 3)},
                {0x74, (AddressingMode.ZeroPageXAddressingMode, InstructionType.Common, 4)},
                {0x80, (AddressingMode.ImmediateAddressingMode, InstructionType.Common, 2)},
                {0x82, (AddressingMode.ImmediateAddressingMode, InstructionType.Common, 2)},
                {0x89, (AddressingMode.ImmediateAddressingMode, InstructionType.Common, 2)},
                {0xC2, (AddressingMode.ImmediateAddressingMode, InstructionType.Common, 2)},
                {0xD4, (AddressingMode.ZeroPageXAddressingMode, InstructionType.Common, 4)},
                {0xE2, (AddressingMode.ImmediateAddressingMode, InstructionType.Common, 2)},
                {0xF4, (AddressingMode.ZeroPageXAddressingMode, InstructionType.Common, 4)}
            };

        public int Invoke(ICpu cpu, byte instruction, ushort address)
        {
            return 0;
        }
    }
}