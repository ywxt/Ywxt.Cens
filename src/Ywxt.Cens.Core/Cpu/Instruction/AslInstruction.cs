using System.Collections.Generic;
using Ywxt.Cens.Core.Utils;

namespace Ywxt.Cens.Core.Cpu.Instruction
{
    public sealed class AslInstruction : IInstruction
    {
        public IReadOnlyDictionary<byte, AddressingMode> OpCodes { get; }
            = new Dictionary<byte, AddressingMode>
            {
                {0x0A, AddressingMode.AccumulatorAddressingMode},
                {0x06, AddressingMode.ZeroPageAddressingMode},
                {0x16, AddressingMode.ZeroPageXAddressingMode},
                {0x0E, AddressingMode.AbsoluteAddressingMode},
                {0x1E, AddressingMode.AbsoluteXAddressingMode}
            };

        public int Invoke(ICpu cpu, byte instruction, ushort address, bool pageCrossed)
        {
            var data = this.GetData(address, cpu, instruction);
            byte @new = 0;
            switch (instruction)
            {
                case 0x0A:
                    cpu.Registers.A = (byte) (data << 1);
                    @new = cpu.Registers.A;
                    break;
                case 0x06:
                case 0x16:
                case 0x0E:
                case 0x1E:
                    @new = (byte) (data << 1);
                    cpu.Bus.WriteByte(address, @new);
                    break;
            }

            cpu.Registers.SetC(data >> 7 == 1);
            cpu.Registers.SetZAndN(@new);

            return instruction switch
            {
                0x0A => 2,
                0x06 => 5,
                0x16 => 6,
                0x0E => 6,
                0x1E => 7,
                _ => 0
            };
        }
    }
}