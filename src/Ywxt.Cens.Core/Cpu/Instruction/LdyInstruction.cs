using System.Collections.Generic;
using Ywxt.Cens.Core.Utils;

namespace Ywxt.Cens.Core.Cpu.Instruction
{
    public sealed class LdyInstruction : IInstruction
    {
        public IReadOnlyDictionary<byte, (AddressingMode mode, InstructionType insType, int cycles)> OpCodes { get; }
            = new Dictionary<byte, (AddressingMode, InstructionType, int)>
            {
                {0xA0, (AddressingMode.ImmediateAddressingMode, InstructionType.Common, 2)},
                {0xA4, (AddressingMode.ZeroPageAddressingMode, InstructionType.Common, 3)},
                {0xB4, (AddressingMode.ZeroPageXAddressingMode, InstructionType.Common, 4)},
                {0xAC, (AddressingMode.AbsoluteAddressingMode, InstructionType.Common, 4)},
                {0xBC, (AddressingMode.AbsoluteXAddressingMode, InstructionType.CrossingPage, 4)}
            };

        public int Invoke(ICpu cpu, byte instruction, ushort address)
        {
            var data = this.ReadData(address, cpu, instruction);
            cpu.CpuRegisters.Y = data;
            cpu.CpuRegisters.SetZAndNFlags(cpu.CpuRegisters.Y);

            return 0;
        }
    }
}