using System.Collections.Generic;
using Ywxt.Cens.Core.Utils;

namespace Ywxt.Cens.Core.Cpu.Instruction
{
    public sealed class LdxInstruction : IInstruction
    {
        public IReadOnlyDictionary<byte, (AddressingMode mode, InstructionType insType, int cycles)> OpCodes { get; }
            = new Dictionary<byte, (AddressingMode, InstructionType, int)>
            {
                {0xA2, (AddressingMode.ImmediateAddressingMode, InstructionType.Common, )},
                {0xAE, (AddressingMode.AbsoluteAddressingMode, InstructionType.Common, )},
                {0xA6, (AddressingMode.ZeroPageAddressingMode, InstructionType.Common, )},
                {0xB6, (AddressingMode.ZeroPageYAddressingMode, InstructionType.Common, )},
                {0xBE, (AddressingMode.AbsoluteYAddressingMode, InstructionType.Common, )}
            };

        public int Invoke(ICpu cpu, byte instruction, ushort address)
        {
            var data = this.ReadData(address, cpu, instruction);
            cpu.Registers.X = data;
            cpu.Registers.SetZAndNFlags(cpu.Registers.X);

            return instruction switch
            {
                0xA2 => 2,
                0xA6 => 3,
                0xB6 => 4,
                0xAE => 4,
                0xBE => 4 + InstructionUtil.GetCrossingPageClockCycles(pageCrossed),
                _ => 0
            };
        }
    }
}