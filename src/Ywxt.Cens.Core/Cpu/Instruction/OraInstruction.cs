using System.Collections.Generic;
using Ywxt.Cens.Core.Utils;

namespace Ywxt.Cens.Core.Cpu.Instruction
{
    public sealed class OraInstruction : IInstruction
    {
        public IReadOnlyDictionary<byte, AddressingMode> OpCodes { get; }
            = new Dictionary<byte, AddressingMode>
            {
                {0x09, AddressingMode.ImmediateAddressingMode},
                {0x05, AddressingMode.ZeroPageAddressingMode},
                {0x15, AddressingMode.ZeroPageXAddressingMode},
                {0x0D, AddressingMode.AbsoluteAddressingMode},
                {0x1D, AddressingMode.AbsoluteXAddressingMode},
                {0x19, AddressingMode.AbsoluteYAddressingMode},
                {0x01, AddressingMode.IndirectXAddressingMode},
                {0x11, AddressingMode.IndirectYAddressingMode}
            };

        public int Invoke(ICpu cpu, byte instruction, ushort address, bool pageCrossed)
        {
            var data = this.ReadData(address, cpu, instruction);
            cpu.Registers.A = (byte) (cpu.Registers.A | data);
            cpu.Registers.SetZAndNFlags(cpu.Registers.A);
            return instruction switch
            {
                0x09 => 2,
                0x05 => 3,
                0x15 => 4,
                0x0D => 4,
                0x1D => 4 + InstructionUtil.GetClockCyclesByCrossingPage(pageCrossed),
                0x19 => 4 + InstructionUtil.GetClockCyclesByCrossingPage(pageCrossed),
                0x01 => 6,
                0x11 => 5 + InstructionUtil.GetClockCyclesByCrossingPage(pageCrossed),
                _ => 0
            };
        }
    }
}