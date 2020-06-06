using System.Collections.Generic;
using Ywxt.Cens.Core.Utils;

namespace Ywxt.Cens.Core.Cpu.Instruction
{
    public sealed class PlaInstruction : IInstruction
    {
        public IReadOnlyDictionary<byte, (AddressingType addrType, AddressingMode addrMode)> OpCodes { get; }
            = new Dictionary<byte, (AddressingType addrType, AddressingMode addrMode)>
            {
                {0x68, (AddressingType.Data, AddressingMode.ImplicitAddressingMode)}
            };

        public int Invoke(ICpu cpu, byte instruction, ushort data)
        {
            cpu.Registers.A = cpu.Stack.PopByte();
            cpu.Registers.SetZAndN(cpu.Registers.A);
            return instruction switch
            {
                0x68 => 4,
                _ => 0
            };
        }
    }
}