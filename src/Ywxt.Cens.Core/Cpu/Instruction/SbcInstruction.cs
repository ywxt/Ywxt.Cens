using System.Collections.Generic;
using Ywxt.Cens.Core.Utils;

namespace Ywxt.Cens.Core.Cpu.Instruction
{
    public sealed class SbcInstruction : IInstruction
    {
        public IReadOnlyDictionary<byte, (AddressingMode mode, InstructionType insType, int cycles)> OpCodes { get; }
            = new Dictionary<byte, (AddressingMode, InstructionType, int)>
            {
                {0xE9, (AddressingMode.ImmediateAddressingMode, InstructionType.Common, )},
                {0xEB, (AddressingMode.ImmediateAddressingMode, InstructionType.Common, )},
                {0xE5, (AddressingMode.ZeroPageAddressingMode, InstructionType.Common, )},
                {0xF5, (AddressingMode.ZeroPageXAddressingMode, InstructionType.Common, )},
                {0xED, (AddressingMode.AbsoluteAddressingMode, InstructionType.Common, )},
                {0xFD, (AddressingMode.AbsoluteXAddressingMode, InstructionType.Common, )},
                {0xF9, (AddressingMode.AbsoluteYAddressingMode, InstructionType.Common, )},
                {0xE1, (AddressingMode.IndirectXAddressingMode, InstructionType.Common, )},
                {0xF1, (AddressingMode.IndirectYAddressingMode, InstructionType.Common, )}
            };

        public int Invoke(ICpu cpu, byte instruction, ushort address)
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
                0xFD => 4 + InstructionUtil.GetCrossingPageClockCycles(pageCrossed),
                0xF9 => 4 + InstructionUtil.GetCrossingPageClockCycles(pageCrossed),
                0xE1 => 6,
                0xF1 => 5 + InstructionUtil.GetCrossingPageClockCycles(pageCrossed),
                _ => 0
            };
        }
    }
}