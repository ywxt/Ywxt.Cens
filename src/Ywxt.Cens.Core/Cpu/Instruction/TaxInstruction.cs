using System.Collections.Generic;
using Ywxt.Cens.Core.Utils;

namespace Ywxt.Cens.Core.Cpu.Instruction
{
    public sealed  class TaxInstruction : IInstruction 
    {
        public IReadOnlyDictionary<byte, AddressingMode> OpCodes { get; }
            = new Dictionary<byte, AddressingMode>
            {
                {0xAA,AddressingMode.ImplicitAddressingMode}
            };

        public AddressingType AddressingType { get; }= AddressingType.Data;

        public int Invoke(ICpu cpu, byte instruction, ushort data, bool pageCrossed)
        {
            cpu.Registers.X = cpu.Registers.A;
            cpu.Registers.SetZAndN(cpu.Registers.X);
            return instruction switch
            {
                0xAA => 2,
                _ => 0
            };

        }
    }
}