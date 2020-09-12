using System.Collections.Generic;
using Ywxt.Cens.Core.Utils;

namespace Ywxt.Cens.Core.Cpu.Instruction
{
    public sealed class IncInstruction : IInstruction
    {
        public IReadOnlyDictionary<byte, (AddressingMode mode, InstructionType insType, int cycles)> OpCodes { get; }
            = new Dictionary<byte, (AddressingMode, InstructionType, int)>
            {
                {0xE6, (AddressingMode.ZeroPageAddressingMode, InstructionType.Common, )},
                {0xF6, (AddressingMode.ZeroPageXAddressingMode, InstructionType.Common, )},
                {0xEE, (AddressingMode.AbsoluteAddressingMode, InstructionType.Common, )},
                {0xFE, (AddressingMode.AbsoluteXAddressingMode, InstructionType.Common, )}
            };

        public int Invoke(ICpu cpu, byte instruction, ushort address)
        {
            var data = cpu.Bus.ReadByte(address);
            data++;
            cpu.Bus.WriteByte(address, data);
            cpu.Registers.SetZAndNFlags(data);
            return instruction switch
            {
                0xE6 => 5,
                0xF6 => 6,
                0xEE => 6,
                0xFE => 7,
                _ => 0
            };
        }
    }
}