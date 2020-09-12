using System;
using System.Collections.Generic;
using Ywxt.Cens.Core.Utils;

namespace Ywxt.Cens.Core.Cpu.Instruction
{
    public sealed class BvsInstruction : IInstruction
    {
        public IReadOnlyDictionary<byte, (AddressingMode mode, InstructionType insType, int cycles)> OpCodes { get; }
            = new Dictionary<byte, (AddressingMode, InstructionType, int)>
            {
                {0x70, (AddressingMode.RelativeAddressingMode, InstructionType.Branch, 2)}
            };

        public int Invoke(ICpu cpu, byte instruction, ushort address)
        {
            var jmpSuccess = cpu.Registers.P.HasFlag(PFlags.V);
            if (jmpSuccess)
            {
                cpu.Registers.Pc = address;
            }

            return Convert.ToInt32(jmpSuccess);
        }
    }
}