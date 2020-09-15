using System.Collections.Generic;
using Ywxt.Cens.Core.Utils;

namespace Ywxt.Cens.Core.Cpu.Instruction
{
    /// <summary>
    /// RLA - Rotate Left then 'And' - ROL + AND
    /// </summary>
    public sealed class RlaInstruction : IInstruction
    {
        public IReadOnlyDictionary<byte, (AddressingMode mode, InstructionType insType, int cycles)> OpCodes { get; }
            = new Dictionary<byte, (AddressingMode, InstructionType, int)>
            {
                {0x27, (AddressingMode.ZeroPageAddressingMode, InstructionType.Common, 5)},
                {0x37, (AddressingMode.ZeroPageXAddressingMode, InstructionType.Common, 6)},
                {0x2F, (AddressingMode.AbsoluteAddressingMode, InstructionType.Common, 6)},
                {0x3F, (AddressingMode.AbsoluteXAddressingMode, InstructionType.Common, 7)},
                {0x3B, (AddressingMode.AbsoluteYAddressingMode, InstructionType.Common, 7)},
                {0x23, (AddressingMode.IndirectXAddressingMode, InstructionType.Common, 8)},
                {0x33, (AddressingMode.IndirectYAddressingMode, InstructionType.Common, 8)}
            };

        public int Invoke(ICpu cpu, byte instruction, ushort address)
        {
            var data = cpu.Bus.ReadByte(address);
            var @new = (byte) ((data << 1) | (byte) (cpu.CpuRegisters.P & PFlags.C));
            cpu.Bus.WriteByte(address, @new);
            cpu.CpuRegisters.SetCFlag(data >> 7 == 1);
            cpu.CpuRegisters.A &= @new;
            cpu.CpuRegisters.SetZAndNFlags(cpu.CpuRegisters.A);
            return 0;
        }
    }
}