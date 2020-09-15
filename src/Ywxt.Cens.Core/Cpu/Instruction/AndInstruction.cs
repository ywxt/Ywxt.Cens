using System.Collections.Generic;
using Ywxt.Cens.Core.Utils;

namespace Ywxt.Cens.Core.Cpu.Instruction
{
    public sealed class AndInstruction : IInstruction
    {
        public IReadOnlyDictionary<byte, (AddressingMode mode, InstructionType insType, int cycles)> OpCodes { get; }
            = new Dictionary<byte, (AddressingMode, InstructionType, int)>
            {
                {0x29, (AddressingMode.ImmediateAddressingMode, InstructionType.Common, 2)},
                {0x25, (AddressingMode.ZeroPageAddressingMode, InstructionType.Common, 3)},
                {0x35, (AddressingMode.ZeroPageXAddressingMode, InstructionType.Common, 4)},
                {0x2D, (AddressingMode.AbsoluteAddressingMode, InstructionType.Common, 4)},
                {0x3D, (AddressingMode.AbsoluteXAddressingMode, InstructionType.CrossingPage, 4)},
                {0x39, (AddressingMode.AbsoluteYAddressingMode, InstructionType.CrossingPage, 4)},
                {0x21, (AddressingMode.IndirectXAddressingMode, InstructionType.Common, 6)},
                {0x31, (AddressingMode.IndirectYAddressingMode, InstructionType.CrossingPage, 5)}
            };

        public int Invoke(ICpu cpu, byte instruction, ushort address)
        {
            var data = this.ReadData(address, cpu, instruction);
            cpu.CpuRegisters.A = (byte) (cpu.CpuRegisters.A & data);
            cpu.CpuRegisters.SetZAndNFlags(cpu.CpuRegisters.A);
            return 0;
        }
    }
}