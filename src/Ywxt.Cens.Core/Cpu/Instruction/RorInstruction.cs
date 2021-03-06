using System.Collections.Generic;
using Ywxt.Cens.Core.Utils;

namespace Ywxt.Cens.Core.Cpu.Instruction
{
    public sealed class RorInstruction : IInstruction
    {
        public IReadOnlyDictionary<byte, (AddressingMode mode, InstructionType insType, int cycles)> OpCodes { get; }
            = new Dictionary<byte, (AddressingMode, InstructionType, int)>
            {
                {0x6A, (AddressingMode.AccumulatorAddressingMode, InstructionType.Common, 2)},
                {0x66, (AddressingMode.ZeroPageAddressingMode, InstructionType.Common, 5)},
                {0x76, (AddressingMode.ZeroPageXAddressingMode, InstructionType.Common, 6)},
                {0x6E, (AddressingMode.AbsoluteAddressingMode, InstructionType.Common, 6)},
                {0x7E, (AddressingMode.AbsoluteXAddressingMode, InstructionType.Common, 7)}
            };

        public int Invoke(ICpu cpu, byte instruction, ushort address)
        {
            var data = this.ReadData(address, cpu, instruction);
            var @new = (byte) ((data >> 1) | ((byte) (cpu.CpuRegisters.P & PFlags.C) << 7));
            this.WriteData(address, @new, cpu, instruction);


            cpu.CpuRegisters.SetCFlag((data & 1) == 1);
            cpu.CpuRegisters.SetZAndNFlags(@new);

            return 0;
        }
    }
}