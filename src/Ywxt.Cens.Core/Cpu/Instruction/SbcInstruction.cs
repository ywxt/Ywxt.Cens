using System.Collections.Generic;
using Ywxt.Cens.Core.Utils;

namespace Ywxt.Cens.Core.Cpu.Instruction
{
    public sealed class SbcInstruction : IInstruction
    {
        public IReadOnlyDictionary<byte, AddressingMode> OpCodes { get; }
            = new Dictionary<byte, AddressingMode>
            {
                {0xE9, AddressingMode.ImmediateAddressingMode},
                {0xEB, AddressingMode.ImmediateAddressingMode},
                {0xE5, AddressingMode.ZeroPageAddressingMode},
                {0xF5, AddressingMode.ZeroPageXAddressingMode},
                {0xED, AddressingMode.AbsoluteAddressingMode},
                {0xFD, AddressingMode.AbsoluteXAddressingMode},
                {0xF9, AddressingMode.AbsoluteYAddressingMode},
                {0xE1, AddressingMode.IndirectXAddressingMode},
                {0xF1, AddressingMode.IndirectYAddressingMode}
            };

        public int Invoke(ICpu cpu, byte instruction, ushort address, bool pageCrossed)
        {
            var data = this.ReadData(address, cpu, instruction);
            var result = unchecked(cpu.Registers.A - data - 1 + (byte) (cpu.Registers.P & PFlags.C));
            var af = cpu.Registers.A >> 7;
            var bf = data >> 7;
            var cf = (result >> 7) & 1;
            //判断溢出
            cpu.Registers.SetVFlag((af == 1 && cf == 0) | (af == 0 && bf == 1 && cf == 1));

            cpu.Registers.SetCFlag(((result >> 8) & 1) != 1);

            cpu.Registers.A = unchecked((byte) result);
            cpu.Registers.SetZAndNFlags(cpu.Registers.A);
            return instruction switch
            {
                0xE9 => 2,
                0xEB => 2,
                0xE5 => 3,
                0xF5 => 4,
                0xED => 4,
                0xFD => 4 + InstructionUtil.GetClockCyclesByCrossingPage(pageCrossed),
                0xF9 => 4 + InstructionUtil.GetClockCyclesByCrossingPage(pageCrossed),
                0xE1 => 6,
                0xF1 => 5 + InstructionUtil.GetClockCyclesByCrossingPage(pageCrossed),
                _ => 0
            };
        }
    }
}