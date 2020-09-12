using System.Collections.Generic;
using Ywxt.Cens.Core.Utils;

namespace Ywxt.Cens.Core.Cpu.Instruction
{
    public sealed class PlaInstruction : IInstruction
    {
        public IReadOnlyDictionary<byte, (AddressingMode mode, InstructionType insType, int cycles)> OpCodes { get; }
            = new Dictionary<byte, (AddressingMode, InstructionType, int)>
            {
                {0x68, (AddressingMode.ImplicitAddressingMode, InstructionType.Common, 4)}
            };

        public int Invoke(ICpu cpu, byte instruction, ushort address)
        {
            cpu.Registers.A = cpu.Stack.PopByte();
            cpu.Registers.SetZAndNFlags(cpu.Registers.A);
            return 0;
        }
    }
}