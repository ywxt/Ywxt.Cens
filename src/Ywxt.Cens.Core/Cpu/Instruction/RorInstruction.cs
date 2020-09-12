using System.Collections.Generic;
using Ywxt.Cens.Core.Utils;

namespace Ywxt.Cens.Core.Cpu.Instruction
{
    public sealed class RorInstruction : IInstruction
    {
        public IReadOnlyDictionary<byte, (AddressingMode mode, InstructionType insType, int cycles)> OpCodes { get; }
            = new Dictionary<byte, (AddressingMode, InstructionType, int)>
            {
                {0x6A, (AddressingMode.AccumulatorAddressingMode, InstructionType.Common, )},
                {0x66, (AddressingMode.ZeroPageAddressingMode, InstructionType.Common, )},
                {0x76, (AddressingMode.ZeroPageXAddressingMode, InstructionType.Common, )},
                {0x6E, (AddressingMode.AbsoluteAddressingMode, InstructionType.Common, )},
                {0x7E, (AddressingMode.AbsoluteXAddressingMode, InstructionType.Common, )}
            };

        public int Invoke(ICpu cpu, byte instruction, ushort address)
        {
            var data = this.ReadData(address, cpu, instruction);
            var @new = (byte) ((data >> 1) | ((byte) (cpu.Registers.P & PFlags.C) << 7));
            this.WriteData(address, @new, cpu, instruction);


            cpu.Registers.SetCFlag((data & 1) == 1);
            cpu.Registers.SetZAndNFlags(@new);

            return instruction switch
            {
                0x6A => 2,
                0x66 => 5,
                0x76 => 6,
                0x6E => 6,
                0x7E => 7,
                _ => 0
            };
        }
    }
}