using System.Collections.Generic;
using Ywxt.Cens.Core.Utils;

namespace Ywxt.Cens.Core.Cpu.Instruction
{
    public sealed class IncInstruction : IInstruction
    {
        public IReadOnlyDictionary<byte, (AddressingMode mode, InstructionType insType, int cycles)> OpCodes { get; }
            = new Dictionary<byte, (AddressingMode, InstructionType, int)>
            {
                {0xE6, (AddressingMode.ZeroPageAddressingMode, InstructionType.Common, 5)},
                {0xF6, (AddressingMode.ZeroPageXAddressingMode, InstructionType.Common, 6)},
                {0xEE, (AddressingMode.AbsoluteAddressingMode, InstructionType.Common, 6)},
                {0xFE, (AddressingMode.AbsoluteXAddressingMode, InstructionType.Common, 7)}
            };

        public int Invoke(ICpu cpu, byte instruction, ushort address)
        {
            var data = cpu.Bus.ReadByte(address);
            data++;
            cpu.Bus.WriteByte(address, data);
            cpu.Registers.SetZAndNFlags(data);
            return 0;
        }
    }
}