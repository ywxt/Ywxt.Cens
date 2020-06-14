namespace Ywxt.Cens.Core.Cpu
{
    public class ImplicitAddressing : IAddressing
    {
        public AddressingType AddressingType { get; } = AddressingType.Data;

        public ushort Addressing(Registers registers, IBus cpuBus)
        {
            return 0;
        }
    }

    public class AccumulatorAddressing : IAddressing
    {
        public AddressingType AddressingType { get; } = AddressingType.Data;

        public ushort Addressing(Registers registers, IBus cpuBus)
        {
            return registers.A;
        }
    }

    public class ImmediateAddressing : IAddressing
    {
        public AddressingType AddressingType { get; } = AddressingType.Data;

        public ushort Addressing(Registers registers, IBus cpuBus)
        {
            return cpuBus.ReadByte(registers.Pc++);
        }
    }

    public class AbsoluteAddressing : IAddressing
    {
        public AddressingType AddressingType { get; } = AddressingType.Address;

        public ushort Addressing(Registers registers, IBus cpuBus)
        {
            var address = cpuBus.ReadWord(registers.Pc);
            registers.Pc += 2;
            return address;
        }
    }

    public class AbsoluteXAddressing : IAddressing
    {
        public AddressingType AddressingType { get; } = AddressingType.Address;

        public ushort Addressing(Registers registers, IBus cpuBus)
        {
            var address = registers.Pc;
            registers.Pc += 2;
            return (ushort) (cpuBus.ReadWord(address) + registers.X);
        }
    }

    public class AbsoluteYAddressing : IAddressing
    {
        public AddressingType AddressingType { get; } = AddressingType.Address;

        public ushort Addressing(Registers registers, IBus cpuBus)
        {
            var address = registers.Pc;
            registers.Pc += 2;
            return (ushort) (cpuBus.ReadWord(address) + registers.Y);
        }
    }

    public class ZeroPageAddressing : IAddressing
    {
        public AddressingType AddressingType { get; } = AddressingType.Address;

        public ushort Addressing(Registers registers, IBus cpuBus)
        {
            return cpuBus.ReadByte(registers.Pc++);
        }
    }

    public class ZeroPageXAddressing : IAddressing
    {
        public AddressingType AddressingType { get; } = AddressingType.Address;

        public ushort Addressing(Registers registers, IBus cpuBus)
        {
            return (ushort) ((cpuBus.ReadByte(registers.Pc++) + registers.X) & 0x00FF);
        }
    }

    public class ZeroPageYAddressing : IAddressing
    {
        public AddressingType AddressingType { get; } = AddressingType.Address;

        public ushort Addressing(Registers registers, IBus cpuBus)
        {
            return (ushort) ((cpuBus.ReadByte(registers.Pc++) + registers.Y) & 0x00FF);
        }
    }

    public class RelativeAddressing : IAddressing
    {
        public AddressingType AddressingType { get; } = AddressingType.Address;

        public ushort Addressing(Registers registers, IBus cpuBus)
        {
            var op = unchecked((sbyte) cpuBus.ReadByte(registers.Pc++));
            return (ushort) (registers.Pc + op);
        }
    }

    public class IndirectAddressing : IAddressing
    {
        public AddressingType AddressingType { get; } = AddressingType.Address;

        public ushort Addressing(Registers registers, IBus cpuBus)
        {
            var low = cpuBus.ReadWord(registers.Pc);
            registers.Pc += 2;
            var high = (ushort) ((low & 0xFF00) | ((low + 1) & 0x00FF));
            return (ushort) ((cpuBus.ReadByte(high) << 8) | cpuBus.ReadByte(low));
        }
    }

    public class IndirectXAddressing : IAddressing
    {
        public AddressingType AddressingType { get; } = AddressingType.Address;

        public ushort Addressing(Registers registers, IBus cpuBus)
        {
            var address = (ushort) (registers.X + cpuBus.ReadByte(registers.Pc++));
            var low = cpuBus.ReadByte((ushort) (address & 0x00FF));
            var high = cpuBus.ReadByte((ushort) ((address + 1) & 0x00FF));
            return (ushort) (low + (high << 8));
        }
    }

    public class IndirectYAddressing : IAddressing
    {
        public AddressingType AddressingType { get; } = AddressingType.Address;

        public ushort Addressing(Registers registers, IBus cpuBus)
        {
            var address = cpuBus.ReadWord(cpuBus.ReadByte(registers.Pc++));
            return (ushort) (address + registers.Y);
        }
    }
}