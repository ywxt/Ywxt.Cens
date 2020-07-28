using System.Collections.Generic;
using Ywxt.Cens.Core.Utils;

namespace Ywxt.Cens.Core.Cpu.Instruction
{
    /// <summary>
    /// RLA - Rotate Left then 'And' - ROL + AND
    /// </summary>
    public sealed class RlaInstruction : IInstruction
    {
        public IReadOnlyDictionary<byte, AddressingMode> OpCodes { get; }
            = new Dictionary<byte, AddressingMode>
            {
                {0x27, AddressingMode.ZeroPageAddressingMode},
                {0x37, AddressingMode.ZeroPageXAddressingMode},
                {0x2F, AddressingMode.AbsoluteAddressingMode},
                {0x3F, AddressingMode.AbsoluteXAddressingMode},
                {0x3B, AddressingMode.AbsoluteYAddressingMode},
                {0x23, AddressingMode.IndirectXAddressingMode},
                {0x33, AddressingMode.IndirectYAddressingMode}
            };

        public AddressingType AddressingType { get; } = AddressingType.Data;

        public int Invoke(ICpu cpu, byte instruction, ushort address, byte data, bool pageCrossed)
        {
            var @new = (byte) ((data << 1) | (byte) (cpu.Registers.P & PFlags.C));
            cpu.Bus.WriteByte(address, @new);
            cpu.Registers.SetC(data >> 7 == 1);
            cpu.Registers.A &= @new;
            cpu.Registers.SetZAndN(cpu.Registers.A);
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