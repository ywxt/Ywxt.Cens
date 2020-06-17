using System;

namespace Ywxt.Cens.Core.Cpu.Instruction
{
    public sealed class InstructionProcessor : IInstructionProcessor
    {
        public int Process(ICpu cpu, byte instruction)
        {
            var ins = Instructions.Get(instruction);
            var addrMode = AddressingModes.Get(ins.OpCodes[instruction]);
            ushort address = 0;
            byte data = 0;
            var pageCrossed = false;
            var result = addrMode.Addressing(cpu.Registers, cpu.Bus);
            switch (addrMode.AddressingType)
            {
                case AddressingType.Data:
                    data = (byte) result.address;
                    pageCrossed = result.pageCrossed;
                    break;
                case AddressingType.Address:
                    address = result.address;
                    pageCrossed = result.pageCrossed;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            // 寻址是地址，需要的是数据
            if (ins.AddressingType == AddressingType.Data &&
                addrMode.AddressingType == AddressingType.Address)
            {
                data = cpu.Bus.ReadByte(address);
            }

            return ins.Invoke(cpu, instruction, address, data, pageCrossed);
        }
    }
}