namespace Ywxt.Cens.Core.Cpu.Addressing
{
    public class ImplicitAddressing : IAddressing
    {
        public Registers Registers { get; }
        public IBus CpuBus { get; }

        public ImplicitAddressing(Registers registers, IBus cpuBus)
        {
            Registers = registers;
            CpuBus = cpuBus;
        }

        public ushort Addressing(byte op1 = 0, byte op2 = 0)
        {
            return 0;
        }
    }

    public class AccumulatorAddressing : IAddressing
    {
        public Registers Registers { get; }
        public IBus CpuBus { get; }

        public AccumulatorAddressing(Registers registers, IBus cpuBus)
        {
            Registers = registers;
            CpuBus = cpuBus;
        }

        public ushort Addressing(byte op1 = 0, byte op2 = 0)
        {
            return Registers.A;
        }
    }

    public class ImmediateAddressing : IAddressing
    {
        public Registers Registers { get; }
        public IBus CpuBus { get; }

        public ImmediateAddressing(Registers registers, IBus cpuBus)
        {
            Registers = registers;
            CpuBus = cpuBus;
        }

        public ushort Addressing(byte op1, byte op2 = 0)
        {
            return op1;
        }
    }

    public class AbsoluteAddressing : IAddressing
    {
        public Registers Registers { get; }
        public IBus CpuBus { get; }

        public AbsoluteAddressing(Registers registers, IBus cpuBus)
        {
            Registers = registers;
            CpuBus = cpuBus;
        }

        public ushort Addressing(byte op1, byte op2)
        {
            return CpuBus.ReadWord((ushort) ((op2 << 8) | op1));
        }
    }

    public class ZeroPageAddressing : IAddressing
    {
        public Registers Registers { get; }
        public IBus CpuBus { get; }

        public ZeroPageAddressing(Registers registers, IBus cpuBus)
        {
            Registers = registers;
            CpuBus = cpuBus;
        }

        public ushort Addressing(byte op1, byte op2 = 0)
        {
            return CpuBus.ReadWord(op1);
        }
    }

    public class ZeroPageXAddressing : IAddressing
    {
        public Registers Registers { get; }
        public IBus CpuBus { get; }

        public ZeroPageXAddressing(Registers registers, IBus cpuBus)
        {
            Registers = registers;
            CpuBus = cpuBus;
        }

        public ushort Addressing(byte op1, byte op2 = 0)
        {
            return CpuBus.ReadWord((ushort) ((op1 + Registers.X) & 0x00FF));
        }
    }
    public class ZeroPageYAddressing : IAddressing
    {
        public Registers Registers { get; }
        public IBus CpuBus { get; }

        public ZeroPageYAddressing(Registers registers, IBus cpuBus)
        {
            Registers = registers;
            CpuBus = cpuBus;
        }

        public ushort Addressing(byte op1, byte op2 = 0)
        {
            return CpuBus.ReadWord((ushort) ((op1 + Registers.Y) & 0x00FF));
        }
    }
}