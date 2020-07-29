using System.Collections.Generic;
using Ywxt.Cens.Core.Utils;

namespace Ywxt.Cens.Core.Cpu.Instruction
{
    public sealed class BccInstruction : IInstruction
    {
        IReadOnlyDictionary<byte, AddressingMode> IInstruction.OpCodes { get; }
            = new Dictionary<byte, AddressingMode>
            {
                {0xF0, AddressingMode.RelativeAddressingMode}
            };

        public int Invoke(ICpu cpu, byte instruction, ushort address, bool pageCrossed)
        {
            var jmpSuccess = cpu.Registers.P.HasFlag(PFlags.Z);
            if (jmpSuccess)
            {
                cpu.Registers.Pc = address;
            }


            return instruction switch
            {
                0xF0 => 2 + InstructionUtil.GetJmpClockCycleIncrement(jmpSuccess, pageCrossed),
                _ => 0
            };
        }
    }
}