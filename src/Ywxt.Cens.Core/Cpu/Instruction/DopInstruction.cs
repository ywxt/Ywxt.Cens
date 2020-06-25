using System.Collections.Generic;

namespace Ywxt.Cens.Core.Cpu.Instruction
{
    /// <summary>
    /// 扩展指令 DOP(double NOP)
    /// </summary>
    public sealed class DopInstruction : IInstruction
    {
        public IReadOnlyDictionary<byte, AddressingMode> OpCodes { get; }
            = new Dictionary<byte, AddressingMode>
            {
                {0x04, AddressingMode.ZeroPageAddressingMode},
                {0x14, AddressingMode.ZeroPageXAddressingMode},
                {0x34, AddressingMode.ZeroPageXAddressingMode},
                {0x44, AddressingMode.ZeroPageAddressingMode},
                {0x54, AddressingMode.ZeroPageXAddressingMode},
                {0x64, AddressingMode.ZeroPageAddressingMode},
                {0x74, AddressingMode.ZeroPageXAddressingMode},
                {0x80, AddressingMode.ImmediateAddressingMode},
                {0x82, AddressingMode.ImmediateAddressingMode},
                {0x89, AddressingMode.ImmediateAddressingMode},
                {0xC2, AddressingMode.ImmediateAddressingMode},
                {0xD4, AddressingMode.ZeroPageXAddressingMode},
                {0xE2, AddressingMode.ImmediateAddressingMode},
                {0xF4, AddressingMode.ZeroPageXAddressingMode}
            };

        public AddressingType AddressingType { get; } = AddressingType.Data;

        public int Invoke(ICpu cpu, byte instruction, ushort address, byte data, bool pageCrossed)
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