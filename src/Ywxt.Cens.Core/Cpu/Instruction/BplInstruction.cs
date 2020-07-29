using System.Collections.Generic;
using Ywxt.Cens.Core.Utils;

namespace Ywxt.Cens.Core.Cpu.Instruction
{
    public sealed class BplInstruction : IInstruction
    {
        public IReadOnlyDictionary<byte, AddressingMode> OpCodes { get; }
            = new Dictionary<byte, AddressingMode>
            {
                {0x10, AddressingMode.RelativeAddressingMode}
            };

        public int Invoke(ICpu cpu, byte instruction, ushort address, bool pageCrossed)
        {
            var jmpSuccess = !cpu.Registers.P.HasFlag(PFlags.N);
            if (jmpSuccess)
            {
                cpu.Registers.Pc = address;
            }

            return instruction switch
            {
                0x10 => 2 + InstructionUtil.GetJmpClockCycleIncrement(jmpSuccess, pageCrossed),
                _ => 0
            };
        }
    }
}