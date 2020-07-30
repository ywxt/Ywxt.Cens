using System.Collections.Generic;
using Ywxt.Cens.Core.Utils;

namespace Ywxt.Cens.Core.Cpu.Instruction
{
    public sealed class PlaInstruction : IInstruction
    {
        public IReadOnlyDictionary<byte, AddressingMode> OpCodes { get; }
            = new Dictionary<byte, AddressingMode>
            {
                {0x68, AddressingMode.ImplicitAddressingMode}
            };

        public int Invoke(ICpu cpu, byte instruction, ushort address, bool pageCrossed)
        {
            cpu.Registers.A = cpu.Stack.PopByte();
            cpu.Registers.SetZAndNFlags(cpu.Registers.A);
            return instruction switch
            {
                0x68 => 4,
                _ => 0
            };
        }
    }
}