using System.Collections.Generic;
using Ywxt.Cens.Core.Utils;

namespace Ywxt.Cens.Core.Cpu.Instruction
{
    public sealed class BccInstruction : IInstruction
    {
        public AddressingType AddressingType { get; } = AddressingType.Address;

        IReadOnlyDictionary<byte, AddressingMode> IInstruction.OpCodes { get; }
            = new Dictionary<byte, AddressingMode>
            {
                {0xF0, AddressingMode.RelativeAddressingMode}
            };

        public int Invoke(ICpu cpu, byte instruction, ushort address, byte data, bool pageCrossed)
        {
            var oldAddress = cpu.Registers.Pc;
            var jmpSuccess = cpu.Registers.P.HasFlag(PFlags.Z);
            if (jmpSuccess)
            {
                cpu.Registers.Pc = address;
            }


            return instruction switch
            {
                0xF0 => 2 + BranchInstructionUtil.GetClockCycleIncrement(jmpSuccess, oldAddress, cpu.Registers.Pc),
                _ => 0
            };
        }
    }
}