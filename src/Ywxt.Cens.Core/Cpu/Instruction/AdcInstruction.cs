using System.Collections.Generic;
using Ywxt.Cens.Core.Utils;

namespace Ywxt.Cens.Core.Cpu.Instruction
{
    public sealed class AdcInstruction : IInstruction
    {
        public IReadOnlyDictionary<byte, (AddressingMode mode, InstructionType insType, int cycles)> OpCodes { get; }
            = new Dictionary<byte, (AddressingMode, InstructionType, int)>
            {
                {0x69, (AddressingMode.ImmediateAddressingMode, InstructionType.Common, 2)},
                {0x65, (AddressingMode.ZeroPageAddressingMode, InstructionType.Common, 3)},
                {0x75, (AddressingMode.ZeroPageXAddressingMode, InstructionType.Common, 4)},
                {0x6D, (AddressingMode.AbsoluteAddressingMode, InstructionType.Common, 4)},
                {0x7D, (AddressingMode.AbsoluteXAddressingMode, InstructionType.CrossingPage, 4)},
                {0x79, (AddressingMode.AbsoluteYAddressingMode, InstructionType.CrossingPage, 4)},
                {0x61, (AddressingMode.IndirectXAddressingMode, InstructionType.Common, 6)},
                {0x71, (AddressingMode.IndirectYAddressingMode, InstructionType.CrossingPage, 5)}
            };

        public int Invoke(ICpu cpu, byte instruction, ushort address)
        {
            var data = this.ReadData(address, cpu, instruction);
            var result = cpu.CpuRegisters.A + data + (byte) (cpu.CpuRegisters.P & PFlags.C);
            var af = cpu.CpuRegisters.A >> 7;
            var bf = data >> 7;
            var cf = (result >> 7) & 1;
            cpu.CpuRegisters.SetVFlag(af == bf && af != cf);
            cpu.CpuRegisters.SetCFlag(((result >> 8) & 1) == 1);
            cpu.CpuRegisters.A = unchecked((byte) result);
            cpu.CpuRegisters.SetZAndNFlags(cpu.CpuRegisters.A);
            return 0;
        }
    }
}