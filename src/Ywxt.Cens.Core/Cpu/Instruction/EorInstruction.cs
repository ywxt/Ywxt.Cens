using System.Collections.Generic;
using Ywxt.Cens.Core.Utils;

namespace Ywxt.Cens.Core.Cpu.Instruction
{
    public sealed class EorInstruction : IInstruction
    {
        public IReadOnlyDictionary<byte, (AddressingMode mode, InstructionType insType, int cycles)> OpCodes { get; }
            = new Dictionary<byte, (AddressingMode, InstructionType, int)>
            {
                {0x49, (AddressingMode.ImmediateAddressingMode, InstructionType.Common, )},
                {0x45, (AddressingMode.ZeroPageAddressingMode, InstructionType.Common, )},
                {0x55, (AddressingMode.ZeroPageXAddressingMode, InstructionType.Common, )},
                {0x4D, (AddressingMode.AbsoluteAddressingMode, InstructionType.Common, )},
                {0x5D, (AddressingMode.AbsoluteXAddressingMode, InstructionType.Common, )},
                {0x59, (AddressingMode.AbsoluteYAddressingMode, InstructionType.Common, )},
                {0x41, (AddressingMode.IndirectXAddressingMode, InstructionType.Common, )},
                {0x51, (AddressingMode.IndirectYAddressingMode, InstructionType.Common, )}
            };

        public int Invoke(ICpu cpu, byte instruction, ushort address)
        {
            var data = this.ReadData(address, cpu, instruction);
            cpu.Registers.A = (byte) (cpu.Registers.A ^ data);
            cpu.Registers.SetZAndNFlags(cpu.Registers.A);
            return instruction switch
            {
                0x49 => 2,
                0x45 => 3,
                0x55 => 4,
                0x4D => 4,
                0x5D => 4 + InstructionUtil.GetCrossingPageClockCycles(pageCrossed),
                0x59 => 4 + InstructionUtil.GetCrossingPageClockCycles(pageCrossed),
                0x41 => 6,
                0x51 => 5 + InstructionUtil.GetCrossingPageClockCycles(pageCrossed),
                _ => 0
            };
        }
    }
}