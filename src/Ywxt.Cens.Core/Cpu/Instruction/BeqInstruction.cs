using System;
using System.Collections.Generic;
using Ywxt.Cens.Core.Utils;

namespace Ywxt.Cens.Core.Cpu.Instruction
{
    public sealed class BeqInstruction : IInstruction
    {
        public IReadOnlyDictionary<byte, (AddressingMode mode, InstructionType insType, int cycles)> OpCodes { get; }
            = new Dictionary<byte, (AddressingMode, InstructionType, int)>
            {
                {0x90, (AddressingMode.RelativeAddressingMode, InstructionType.Branch, 2)}
            };

        public int Invoke(ICpu cpu, byte instruction, ushort address)
        {
            var jmpSuccess = !cpu.CpuRegisters.P.HasFlag(PFlags.C);
            if (jmpSuccess)
            {
                cpu.CpuRegisters.Pc = address;
            }

            return Convert.ToInt32(jmpSuccess);
        }
    }
}