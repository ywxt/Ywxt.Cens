using System.Collections.Generic;
using Ywxt.Cens.Core.Utils;

namespace Ywxt.Cens.Core.Cpu.Instruction
{
    public sealed class LaxInstruction : IInstruction
    {
        public IReadOnlyDictionary<byte, (AddressingMode mode, InstructionType insType, int cycles)> OpCodes { get; }
            = new Dictionary<byte, (AddressingMode, InstructionType, int)>
            {
                {0xA7, (AddressingMode.ZeroPageAddressingMode, InstructionType.Common, )},
                {0xB7, (AddressingMode.ZeroPageYAddressingMode, InstructionType.Common, )},
                {0xAF, (AddressingMode.AbsoluteAddressingMode, InstructionType.Common, )},
                {0xBF, (AddressingMode.AbsoluteYAddressingMode, InstructionType.Common, )},
                {0xA3, (AddressingMode.IndirectXAddressingMode, InstructionType.Common, )},
                {0xB3, (AddressingMode.IndirectYAddressingMode, InstructionType.Common, )}
            };

        public int Invoke(ICpu cpu, byte instruction, ushort address)
        {
            var data = this.ReadData(address, cpu, instruction);
            cpu.Registers.A = data;
            cpu.Registers.X = data;
            cpu.Registers.SetZAndNFlags(data);
            return instruction switch
            {
                0xA7 => 3,
                0xB7 => 4,
                0xAF => 4,
                0xBF => 4 + InstructionUtil.GetCrossingPageClockCycles(pageCrossed),
                0xA3 => 6,
                0xB3 => 5 + InstructionUtil.GetCrossingPageClockCycles(pageCrossed),
                _ => 0
            };
        }
    }
}