using System.Collections.Generic;
using Ywxt.Cens.Core.Utils;

namespace Ywxt.Cens.Core.Cpu.Instruction
{
    public sealed class DexInstruction : IInstruction
    {
        public IReadOnlyDictionary<byte, AddressingMode> OpCodes { get; }
            = new Dictionary<byte, AddressingMode>
            {
                {0xCA, AddressingMode.ImplicitAddressingMode}
            };

        public int Invoke(ICpu cpu, byte instruction, ushort address, bool pageCrossed)
        {
            cpu.Registers.X--;
            cpu.Registers.SetZAndNFlags(cpu.Registers.X);
            return instruction switch
            {
                0xCA => 2,
                _ => 0
            };
        }
    }
}