using Ywxt.Cens.Core.Cpu;
using Ywxt.Cens.Core.Cpu.Instruction;
using Ywxt.Cens.Core.Exceptions;

namespace Ywxt.Cens.Core.Utils
{
    public static class InstructionExtension
    {
        public static byte GetData(this IInstruction ins, ushort address, ICpu cpu, byte instruction)
        {
            if (!ins.OpCodes.TryGetValue(instruction, out var mode))
            {
                throw new UnknownInstructionException(instruction);
            }

            var addressingMode = AddressingModes.Get(mode);
            if (addressingMode.AddressingType == AddressingType.Address)
            {
                return cpu.Bus.ReadByte(address);
            }

            return (byte) address;
        }
    }
}