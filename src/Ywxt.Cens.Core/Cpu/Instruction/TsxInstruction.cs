using System.Collections.Generic;
using Ywxt.Cens.Core.Utils;

namespace Ywxt.Cens.Core.Cpu.Instruction
{
    public sealed class TsxInstruction : IInstruction
    {
        public IReadOnlyDictionary<byte, (AddressingMode mode, InstructionType insType, int cycles)> OpCodes { get; }
            = new Dictionary<byte, (AddressingMode, InstructionType, int)>
            {
                {0xBA, (AddressingMode.ImplicitAddressingMode, InstructionType.Common, 2)}
            };

        public int Invoke(ICpu cpu, byte instruction, ushort address)
        {
            cpu.Registers.X = cpu.Registers.Sp;
            cpu.Registers.SetZAndNFlags(cpu.Registers.X);
            return 0;
        }
    }
}