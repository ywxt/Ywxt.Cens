using System.Collections.Generic;
using Ywxt.Cens.Core.Utils;

namespace Ywxt.Cens.Core.Cpu.Instruction
{
    public sealed class LdyInstruction : IInstruction
    {
        public IReadOnlyDictionary<byte, (AddressingMode mode, InstructionType insType, int cycles)> OpCodes { get; }
            = new Dictionary<byte, (AddressingMode, InstructionType, int)>
            {
                {0xA0, (AddressingMode.ImmediateAddressingMode, InstructionType.Common, )},
                {0xA4, (AddressingMode.ZeroPageAddressingMode, InstructionType.Common, )},
                {0xB4, (AddressingMode.ZeroPageXAddressingMode, InstructionType.Common, )},
                {0xAC, (AddressingMode.AbsoluteAddressingMode, InstructionType.Common, )},
                {0xBC, (AddressingMode.AbsoluteXAddressingMode, InstructionType.Common, )}
            };

        public int Invoke(ICpu cpu, byte instruction, ushort address)
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
                0xBC => 4 + InstructionUtil.GetCrossingPageClockCycles(pageCrossed),
                _ => 0
            };
        }
    }
}