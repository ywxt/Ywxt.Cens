using System.Collections.Generic;
using Ywxt.Cens.Core.Utils;

namespace Ywxt.Cens.Core.Cpu.Instruction
{
    public sealed class LaxInstruction : IInstruction
    {
        public IReadOnlyDictionary<byte, AddressingMode> OpCodes { get; }
            = new Dictionary<byte, AddressingMode>
            {
                {0xA7, AddressingMode.ZeroPageAddressingMode},
                {0xB7, AddressingMode.ZeroPageYAddressingMode},
                {0xAF, AddressingMode.AbsoluteAddressingMode},
                {0xBF, AddressingMode.AbsoluteYAddressingMode},
                {0xA3, AddressingMode.IndirectXAddressingMode},
                {0xB3, AddressingMode.IndirectYAddressingMode}
            };

        public AddressingType AddressingType { get; } = AddressingType.Data;

        public int Invoke(ICpu cpu, byte instruction, ushort address, byte data, bool pageCrossed)
        {
            cpu.Registers.A = data;
            cpu.Registers.X = data;
            cpu.Registers.SetZAndN(data);
            return instruction switch
            {
                0xA7 => 3,
                0xB7 => 4,
                0xAF => 4,
                0xBF => 4 + InstructionUtil.GetClockCycleByCrossingPage(pageCrossed),
                0xA3 => 6,
                0xB3 => 5 + InstructionUtil.GetClockCycleByCrossingPage(pageCrossed),
                _ => 0
            };
        }
    }
}