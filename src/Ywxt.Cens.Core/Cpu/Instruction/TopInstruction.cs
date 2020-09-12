using System.Collections.Generic;
using Ywxt.Cens.Core.Utils;

namespace Ywxt.Cens.Core.Cpu.Instruction
{
    public sealed class TopInstruction : IInstruction
    {
        public IReadOnlyDictionary<byte, (AddressingMode mode, InstructionType insType, int cycles)> OpCodes { get; }
            = new Dictionary<byte, (AddressingMode, InstructionType, int)>
            {
                {0x0C, (AddressingMode.AbsoluteAddressingMode, InstructionType.Common, 4)},
                {0x1C, (AddressingMode.AbsoluteXAddressingMode, InstructionType.CrossingPage, 4)},
                {0x3C, (AddressingMode.AbsoluteXAddressingMode, InstructionType.CrossingPage, 4)},
                {0x5C, (AddressingMode.AbsoluteXAddressingMode, InstructionType.CrossingPage, 4)},
                {0x7C, (AddressingMode.AbsoluteXAddressingMode, InstructionType.CrossingPage, 4)},
                {0xDC, (AddressingMode.AbsoluteXAddressingMode, InstructionType.CrossingPage, 4)},
                {0xFC, (AddressingMode.AbsoluteXAddressingMode, InstructionType.CrossingPage, 4)},
            };

        public int Invoke(ICpu cpu, byte instruction, ushort address)
        {
            return 0;
        }
    }
}