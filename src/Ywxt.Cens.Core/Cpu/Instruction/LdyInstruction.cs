using System.Collections.Generic;
using Ywxt.Cens.Core.Utils;

namespace Ywxt.Cens.Core.Cpu.Instruction
{
    public sealed class LdyInstruction : IInstruction
    {
        public IReadOnlyDictionary<byte, AddressingMode> OpCodes { get; }
            = new Dictionary<byte, AddressingMode>
            {
                {0xA0, AddressingMode.ImmediateAddressingMode},
                {0xA4, AddressingMode.ZeroPageAddressingMode},
                {0xB4, AddressingMode.ZeroPageXAddressingMode},
                {0xAC, AddressingMode.AbsoluteAddressingMode},
                {0xBC, AddressingMode.AbsoluteXAddressingMode}
            };

        public int Invoke(ICpu cpu, byte instruction, ushort address, bool pageCrossed)
        {
            var data = this.ReadData(address, cpu, instruction);
            cpu.Registers.Y = data;
            cpu.Registers.SetZAndNFlags(cpu.Registers.Y);

            return instruction switch
            {
                0xA0 => 2,
                0xA4 => 3,
                0xB4 => 4,
                0xAC => 4,
                0xBC => 4 + InstructionUtil.GetClockCyclesByCrossingPage(pageCrossed),
                _ => 0
            };
        }
    }
}