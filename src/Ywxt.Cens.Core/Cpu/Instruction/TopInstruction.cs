using System.Collections.Generic;
using Ywxt.Cens.Core.Utils;

namespace Ywxt.Cens.Core.Cpu.Instruction
{
    public sealed class TopInstruction : IInstruction
    {
        public IReadOnlyDictionary<byte, (AddressingMode mode, InstructionType insType, int cycles)> OpCodes { get; }
            = new Dictionary<byte, (AddressingMode, InstructionType, int)>
            {
                {0x0C, (AddressingMode.AbsoluteAddressingMode, InstructionType.Common, )},
                {0x1C, (AddressingMode.AbsoluteXAddressingMode, InstructionType.Common, )},
                {0x3C, (AddressingMode.AbsoluteXAddressingMode, InstructionType.Common, )},
                {0x5C, (AddressingMode.AbsoluteXAddressingMode, InstructionType.Common, )},
                {0x7C, (AddressingMode.AbsoluteXAddressingMode, InstructionType.Common, )},
                {0xDC, (AddressingMode.AbsoluteXAddressingMode, InstructionType.Common, )},
                {0xFC, (AddressingMode.AbsoluteXAddressingMode, InstructionType.Common, )},
            };

        public int Invoke(ICpu cpu, byte instruction, ushort address)
        {
            switch (instruction)
            {
                case 0x0C:
                    return 4;
                case 0x1C:
                case 0x3C:
                case 0x5C:
                case 0x7C:
                case 0xDC:
                case 0xFC:
                    return 4 + InstructionUtil.GetCrossingPageClockCycles(pageCrossed);
                default: return 0;
            }
        }
    }
}