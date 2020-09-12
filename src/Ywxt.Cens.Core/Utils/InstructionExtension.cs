using Ywxt.Cens.Core.Cpu;
using Ywxt.Cens.Core.Cpu.Instruction;
using Ywxt.Cens.Core.Exceptions;

namespace Ywxt.Cens.Core.Utils
{
    public static class InstructionExtension
    {
        public static byte ReadData(this IInstruction ins, ushort address, ICpu cpu, byte instruction)
        {
            if (!ins.OpCodes.TryGetValue(instruction, out var value))
            {
                throw new UnknownInstructionException(instruction);
            }

            var addressingMode = AddressingModes.Get(value.mode);
            if (addressingMode.AddressingType == AddressingType.Address)
            {
                return cpu.Bus.ReadByte(address);
            }

            return (byte) address;
        }

        public static void WriteData(this IInstruction ins, ushort address, byte data, ICpu cpu, byte instruction)
        {
            if (!ins.OpCodes.TryGetValue(instruction, out var value))
            {
                throw new UnknownInstructionException(instruction);
            }

            var addressingMode = AddressingModes.Get(value.mode);
            if (value.mode == AddressingMode.AccumulatorAddressingMode)
            {
                cpu.Registers.A = data;
                return;
            }

            if (addressingMode.AddressingType == AddressingType.Address)
            {
                cpu.Bus.WriteByte(address, data);
            }
        }
    }
}