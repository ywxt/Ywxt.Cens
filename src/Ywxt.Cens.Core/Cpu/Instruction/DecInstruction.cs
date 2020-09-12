using System.Collections.Generic;
using Ywxt.Cens.Core.Utils;

namespace Ywxt.Cens.Core.Cpu.Instruction
{
    public sealed class DecInstruction : IInstruction
    {
        public IReadOnlyDictionary<byte, (AddressingMode mode, InstructionType insType, int cycles)> OpCodes { get; }
            = new Dictionary<byte, (AddressingMode, InstructionType, int)>
            {
                {0xC6, (AddressingMode.ZeroPageAddressingMode, InstructionType.Common, )},
                {0xD6, (AddressingMode.ZeroPageXAddressingMode, InstructionType.Common, )},
                {0xCE, (AddressingMode.AbsoluteAddressingMode, InstructionType.Common, )},
                {0xDE, (AddressingMode.AbsoluteXAddressingMode, InstructionType.Common, )}
            };

        public int Invoke(ICpu cpu, byte instruction, ushort address)
        {
            var data = this.ReadData(address, cpu, instruction);
            data--;
            cpu.Bus.WriteByte(address, data);
            cpu.Registers.SetZAndNFlags(data);
            return instruction switch
            {
                0xC6 => 5,
                0xD6 => 6,
                0xCE => 6,
                0xDE => 7,
                _ => 0
            };
        }
    }
}