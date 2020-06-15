using System.Collections.Generic;
using Ywxt.Cens.Core.Utils;

namespace Ywxt.Cens.Core.Cpu.Instruction
{
    public sealed class LsrInstruction : IInstruction
    {
        public IReadOnlyDictionary<byte, AddressingMode> OpCodes { get; }
            = new Dictionary<byte, AddressingMode>
            {
                {0x4A, AddressingMode.AccumulatorAddressingMode},
                {0x46, AddressingMode.ZeroPageAddressingMode},
                {0x56, AddressingMode.ZeroPageXAddressingMode},
                {0x4E, AddressingMode.AbsoluteAddressingMode},
                {0x5E, AddressingMode.AbsoluteXAddressingMode}
            };

        public AddressingType AddressingType { get; } = AddressingType.Data;

        public int Invoke(ICpu cpu, byte instruction, ushort address, byte data, bool pageCrossed)
        {
            var old = data;
            byte @new = 0;
            switch (instruction)
            {
                case 0x4A:
                    cpu.Registers.A = (byte) (data >> 1);
                    @new = cpu.Registers.A;
                    break;
                case 0x46:
                case 0x56:
                case 0x4E:
                case 0x5E:
                    cpu.Bus.WriteByte(address, (byte) (data >> 1));
                    @new = (byte) (data >> 1);
                    break;
            }

            cpu.Registers.SetC((old & 1) == 1);
            cpu.Registers.SetZAndN(@new);

            return instruction switch
            {
                0x4A => 2,
                0x46 => 5,
                0x56 => 6,
                0x4E => 6,
                0x5E => 7,
                _ => 0
            };
        }
    }
}