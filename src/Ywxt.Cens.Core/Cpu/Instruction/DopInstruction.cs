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
                {0x04, (AddressingMode.ZeroPageAddressingMode, InstructionType.Common, )},
                {0x14, (AddressingMode.ZeroPageXAddressingMode, InstructionType.Common, )},
                {0x34, (AddressingMode.ZeroPageXAddressingMode, InstructionType.Common, )},
                {0x44, (AddressingMode.ZeroPageAddressingMode, InstructionType.Common, )},
                {0x54, (AddressingMode.ZeroPageXAddressingMode, InstructionType.Common, )},
                {0x64, (AddressingMode.ZeroPageAddressingMode, InstructionType.Common, )},
                {0x74, (AddressingMode.ZeroPageXAddressingMode, InstructionType.Common, )},
                {0x80, (AddressingMode.ImmediateAddressingMode, InstructionType.Common, )},
                {0x82, (AddressingMode.ImmediateAddressingMode, InstructionType.Common, )},
                {0x89, (AddressingMode.ImmediateAddressingMode, InstructionType.Common, )},
                {0xC2, (AddressingMode.ImmediateAddressingMode, InstructionType.Common, )},
                {0xD4, (AddressingMode.ZeroPageXAddressingMode, InstructionType.Common, )},
                {0xE2, (AddressingMode.ImmediateAddressingMode, InstructionType.Common, )},
                {0xF4, (AddressingMode.ZeroPageXAddressingMode, InstructionType.Common, )}
            };

        public int Invoke(ICpu cpu, byte instruction, ushort address)
        {
            switch (instruction)
            {
                case 0x80:
                case 0x82:
                case 0x89:
                case 0xC2:
                case 0xE2:
                    return 2;
                case 0x04:
                case 0x44:
                case 0x64:
                    return 3;
                case 0x14:
                case 0x34:
                case 0x54:
                case 0x74:
                case 0xD4:
                case 0xF4:
                    return 4;
                default: return 0;
            }
        }
    }
}