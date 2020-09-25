using Ywxt.Cens.Core.Utils;

namespace Ywxt.Cens.Core.Cpu
{
    public class ImplicitAddressing : IAddressing
    {
        public AddressingType AddressingType { get; } = AddressingType.Data;

        public (ushort address, bool pageCrossed) Addressing(CpuRegisters registers, IBus cpuBus)
        {
            return (0, false);
        }
    }

    public class AccumulatorAddressing : IAddressing
    {
        public AddressingType AddressingType { get; } = AddressingType.Data;

        public (ushort address, bool pageCrossed) Addressing(CpuRegisters registers, IBus cpuBus)
        {
            return (registers.A, false);
        }
    }

    public class ImmediateAddressing : IAddressing
    {
        public AddressingType AddressingType { get; } = AddressingType.Data;

        public (ushort address, bool pageCrossed) Addressing(CpuRegisters registers, IBus cpuBus)
        {
            return (cpuBus.ReadByte(registers.Pc++), false);
        }
    }

    public class AbsoluteAddressing : IAddressing
    {
        public AddressingType AddressingType { get; } = AddressingType.Address;

        public (ushort address, bool pageCrossed) Addressing(CpuRegisters registers, IBus cpuBus)
        {
            var address = cpuBus.ReadWord(registers.Pc);
            registers.Pc += 2;
            return (address, false);
        }
    }

    public class AbsoluteXAddressing : IAddressing
    {
        public AddressingType AddressingType { get; } = AddressingType.Address;

        public (ushort address, bool pageCrossed) Addressing(CpuRegisters registers, IBus cpuBus)
        {
            var address = registers.Pc;
            registers.Pc += 2;
            address = cpuBus.ReadWord(address);
            var result = (ushort) (address + registers.X);
            return (result, InstructionUtil.IsPageCrossed(address, result));
        }
    }

    public class AbsoluteYAddressing : IAddressing
    {
        public AddressingType AddressingType { get; } = AddressingType.Address;

        public (ushort address, bool pageCrossed) Addressing(CpuRegisters registers, IBus cpuBus)
        {
            var address = registers.Pc;
            registers.Pc += 2;
            address = cpuBus.ReadWord(address);
            var result = (ushort) (address + registers.Y);
            return (result, InstructionUtil.IsPageCrossed(address, result));
        }
    }

    public class ZeroPageAddressing : IAddressing
    {
        public AddressingType AddressingType { get; } = AddressingType.Address;

        public (ushort address, bool pageCrossed) Addressing(CpuRegisters registers, IBus cpuBus)
        {
            return (cpuBus.ReadByte(registers.Pc++), false);
        }
    }

    public class ZeroPageXAddressing : IAddressing
    {
        public AddressingType AddressingType { get; } = AddressingType.Address;

        public (ushort address, bool pageCrossed) Addressing(CpuRegisters registers, IBus cpuBus)
        {
            return ((ushort) ((cpuBus.ReadByte(registers.Pc++) + registers.X) & 0x00FF), false);
        }
    }

    public class ZeroPageYAddressing : IAddressing
    {
        public AddressingType AddressingType { get; } = AddressingType.Address;

        public (ushort address, bool pageCrossed) Addressing(CpuRegisters registers, IBus cpuBus)
        {
            return ((ushort) ((cpuBus.ReadByte(registers.Pc++) + registers.Y) & 0x00FF), false);
        }
    }

    public class RelativeAddressing : IAddressing
    {
        public AddressingType AddressingType { get; } = AddressingType.Address;

        public (ushort address, bool pageCrossed) Addressing(CpuRegisters registers, IBus cpuBus)
        {
            var op = unchecked((sbyte) cpuBus.ReadByte(registers.Pc++));
            var result = (ushort) (registers.Pc + op);
            return (result, InstructionUtil.IsPageCrossed(registers.Pc, result));
        }
    }

    public class IndirectAddressing : IAddressing
    {
        public AddressingType AddressingType { get; } = AddressingType.Address;

        public (ushort address, bool pageCrossed) Addressing(CpuRegisters registers, IBus cpuBus)
        {
            var low = cpuBus.ReadWord(registers.Pc);
            registers.Pc += 2;
            var high = (ushort) ((low & 0xFF00) | ((low + 1) & 0x00FF));
            var result = (ushort) ((cpuBus.ReadByte(high) << 8) | cpuBus.ReadByte(low));
            return (result, false);
        }
    }

    public class IndirectXAddressing : IAddressing
    {
        public AddressingType AddressingType { get; } = AddressingType.Address;

        public (ushort address, bool pageCrossed) Addressing(CpuRegisters registers, IBus cpuBus)
        {
            var address = (byte) (registers.X + cpuBus.ReadByte(registers.Pc++));
            var low = cpuBus.ReadByte(address);
            var high = cpuBus.ReadByte((byte) (address + 1));
            var result = (ushort) (low + (high << 8));
            return (result, InstructionUtil.IsPageCrossed(result, address));
        }
    }

    public class IndirectYAddressing : IAddressing
    {
        public AddressingType AddressingType { get; } = AddressingType.Address;

        public (ushort address, bool pageCrossed) Addressing(CpuRegisters registers, IBus cpuBus)
        {
            ushort address = cpuBus.ReadByte(registers.Pc++);
            var low = cpuBus.ReadByte((byte) address);
            var high = cpuBus.ReadByte((byte) (address + 1));
            address = (ushort) (low + (high << 8));
            var result = (ushort) (address + registers.Y);
            return (result, InstructionUtil.IsPageCrossed(result, address));
        }
    }
}