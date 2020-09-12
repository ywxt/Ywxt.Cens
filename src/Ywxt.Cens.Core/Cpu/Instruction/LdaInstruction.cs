using System.Collections.Generic;
using Ywxt.Cens.Core.Utils;

namespace Ywxt.Cens.Core.Cpu.Instruction
{
    public sealed class LdaInstruction : IInstruction
    {
        public IReadOnlyDictionary<byte, (AddressingMode mode, InstructionType insType, int cycles)> OpCodes { get; }
            = new Dictionary<byte, (AddressingMode, InstructionType, int)>
            {
                {0xA9, (AddressingMode.ImmediateAddressingMode, InstructionType.Common, )},
                {0xA5, (AddressingMode.ZeroPageAddressingMode, InstructionType.Common, )},
                {0xB5, (AddressingMode.ZeroPageXAddressingMode, InstructionType.Common, )},
                {0xAD, (AddressingMode.AbsoluteAddressingMode, InstructionType.Common, )},
                {0xBD, (AddressingMode.AbsoluteXAddressingMode, InstructionType.Common, )},
                {0xB9, (AddressingMode.AbsoluteYAddressingMode, InstructionType.Common, )},
                {0xA1, (AddressingMode.IndirectXAddressingMode, InstructionType.Common, )},
                {0xB1, (AddressingMode.IndirectYAddressingMode, InstructionType.Common, )}
            };

        public int Invoke(ICpu cpu, byte instruction, ushort address)
        {
            var data = this.ReadData(address, cpu, instruction);
            cpu.Registers.A = data;
            cpu.Registers.SetZAndNFlags(cpu.Registers.A);

            return instruction switch
            {
                0xA9 => 2,
                0xA5 => 3,
                0xB5 => 4,
                0xAD => 4,
                0xBD => 4 + InstructionUtil.GetCrossingPageClockCycles(pageCrossed),
                0xB9 => 4 + InstructionUtil.GetCrossingPageClockCycles(pageCrossed),
                0xA1 => 6,
                0xB1 => 5 + InstructionUtil.GetCrossingPageClockCycles(pageCrossed),
                _ => 0
            };
        }
    }
}