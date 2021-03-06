using System.Collections.Generic;
using Ywxt.Cens.Core.Utils;

namespace Ywxt.Cens.Core.Cpu.Instruction
{
    /// <summary>
    /// Rotate Right then Add with Carry - ROR + ADC
    /// </summary>
    public sealed class RraInstruction : IInstruction
    {
        public IReadOnlyDictionary<byte, (AddressingMode mode, InstructionType insType, int cycles)> OpCodes { get; }
            = new Dictionary<byte, (AddressingMode, InstructionType, int)>
            {
                [0x67] = (AddressingMode.ZeroPageAddressingMode, InstructionType.Common, 5),
                [0x77] = (AddressingMode.ZeroPageXAddressingMode, InstructionType.Common, 6),
                [0x6F] = (AddressingMode.AbsoluteAddressingMode, InstructionType.Common, 6),
                [0x7F] = (AddressingMode.AbsoluteXAddressingMode, InstructionType.Common, 7),
                [0x7B] = (AddressingMode.AbsoluteYAddressingMode, InstructionType.Common, 7),
                [0x63] = (AddressingMode.IndirectXAddressingMode, InstructionType.Common, 8),
                [0x73] = (AddressingMode.IndirectYAddressingMode, InstructionType.Common, 8)
            };

        public int Invoke(ICpu cpu, byte instruction, ushort address)
        {
            // ROR
            ushort data = cpu.Bus.ReadByte(address);
            var @new = (byte) ((data >> 1) | ((byte) (cpu.CpuRegisters.P & PFlags.C) << 7));
            cpu.Bus.WriteByte(address, @new);
            cpu.CpuRegisters.SetCFlag((data & 1) == 1);
            // ADC
            var result = cpu.CpuRegisters.A + @new + (byte) (cpu.CpuRegisters.P & PFlags.C);
            var af = cpu.CpuRegisters.A >> 7;
            var bf = @new >> 7;
            var cf = (result >> 7) & 1;
            cpu.CpuRegisters.SetVFlag(af == bf && af != cf);
            cpu.CpuRegisters.SetCFlag(((result >> 8) & 1) == 1);
            cpu.CpuRegisters.A = unchecked((byte) result);
            cpu.CpuRegisters.SetZAndNFlags(cpu.CpuRegisters.A);
            return 0;
        }
    }
}