using System.Collections.Generic;
using Ywxt.Cens.Core.Utils;

namespace Ywxt.Cens.Core.Cpu.Instruction
{
    public sealed class OraInstruction : IInstruction
    {
        public IReadOnlyDictionary<byte, (AddressingMode mode, InstructionType insType, int cycles)> OpCodes { get; }
            = new Dictionary<byte, (AddressingMode, InstructionType, int)>
            {
                {0x09, (AddressingMode.ImmediateAddressingMode, InstructionType.Common, 2)},
                {0x05, (AddressingMode.ZeroPageAddressingMode, InstructionType.Common, 3)},
                {0x15, (AddressingMode.ZeroPageXAddressingMode, InstructionType.Common, 4)},
                {0x0D, (AddressingMode.AbsoluteAddressingMode, InstructionType.Common, 4)},
                {0x1D, (AddressingMode.AbsoluteXAddressingMode, InstructionType.CrossingPage, 4)},
                {0x19, (AddressingMode.AbsoluteYAddressingMode, InstructionType.CrossingPage, 4)},
                {0x01, (AddressingMode.IndirectXAddressingMode, InstructionType.Common, 6)},
                {0x11, (AddressingMode.IndirectYAddressingMode, InstructionType.CrossingPage, 5)}
            };

        public int Invoke(ICpu cpu, byte instruction, ushort address)
        {
            var data = this.ReadData(address, cpu, instruction);
            cpu.Registers.A = (byte) (cpu.Registers.A | data);
            cpu.Registers.SetZAndNFlags(cpu.Registers.A);
            return 0;
        }
    }
}