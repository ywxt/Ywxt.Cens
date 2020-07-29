using System.Collections.Generic;
using Ywxt.Cens.Core.Utils;

namespace Ywxt.Cens.Core.Cpu.Instruction
{
    public sealed class TopInstruction : IInstruction
    {
        public IReadOnlyDictionary<byte, AddressingMode> OpCodes { get; }
            = new Dictionary<byte, AddressingMode>
            {
                {0x0C, AddressingMode.AbsoluteAddressingMode},
                {0x1C, AddressingMode.AbsoluteXAddressingMode},
                {0x3C, AddressingMode.AbsoluteXAddressingMode},
                {0x5C, AddressingMode.AbsoluteXAddressingMode},
                {0x7C, AddressingMode.AbsoluteXAddressingMode},
                {0xDC, AddressingMode.AbsoluteXAddressingMode},
                {0xFC, AddressingMode.AbsoluteXAddressingMode},
            };

        public int Invoke(ICpu cpu, byte instruction, ushort address, bool pageCrossed)
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
                    return 4 + InstructionUtil.GetClockCyclesByCrossingPage(pageCrossed);
                default: return 0;
            }
        }
    }
}