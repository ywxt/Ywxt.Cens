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
                {0x27, (AddressingMode.ZeroPageAddressingMode, InstructionType.Common, )},
                {0x37, (AddressingMode.ZeroPageXAddressingMode, InstructionType.Common, )},
                {0x2F, (AddressingMode.AbsoluteAddressingMode, InstructionType.Common, )},
                {0x3F, (AddressingMode.AbsoluteXAddressingMode, InstructionType.Common, )},
                {0x3B, (AddressingMode.AbsoluteYAddressingMode, InstructionType.Common, )},
                {0x23, (AddressingMode.IndirectXAddressingMode, InstructionType.Common, )},
                {0x33, (AddressingMode.IndirectYAddressingMode, InstructionType.Common, )}
            };

        public int Invoke(ICpu cpu, byte instruction, ushort address)
        {
            var data = cpu.Bus.ReadByte(address);
            var @new = (byte) ((data << 1) | (byte) (cpu.Registers.P & PFlags.C));
            cpu.Bus.WriteByte(address, @new);
            cpu.Registers.SetCFlag(data >> 7 == 1);
            cpu.Registers.A &= @new;
            cpu.Registers.SetZAndNFlags(cpu.Registers.A);
            return instruction switch
            {
                0x27 => 5,
                0x37 => 6,
                0x2F => 6,
                0x3F => 7,
                0x3B => 7,
                0x23 => 8,
                0x33 => 8,
                _ => 0
            };
        }
    }
}