namespace Ywxt.Cens.Core.Cpu
{
    public class ImplicitAddressing : IAddressing
    {

        public ushort Addressing(Registers registers, IBus cpuBus)
        {
            return 0;
        }

        
        
    }

    public class AccumulatorAddressing : IAddressing
    {

        public ushort Addressing(Registers registers, IBus cpuBus)
        {
            return registers.A;
        }

        
        
    }

    public class ImmediateAddressing : IAddressing
    {

        public ushort Addressing(Registers registers, IBus cpuBus)
        {
            return cpuBus.ReadByte(registers.Pc++);
        }

        
        
    }

    public class AbsoluteAddressing : IAddressing
    {

        public ushort Addressing(Registers registers, IBus cpuBus)
        {
            var address = cpuBus.ReadWord(registers.Pc);
            registers.Pc += 2;
            return address;
        }

        

    }

    public class AbsoluteXAddressing : IAddressing
    {

        public ushort Addressing(Registers registers, IBus cpuBus)
        {
            return (ushort) (cpuBus.ReadWord(registers.Pc++) + registers.X);
        }

        
        
    }

    public class AbsoluteYAddressing : IAddressing
    {

        public ushort Addressing(Registers registers, IBus cpuBus)
        {
            return (ushort) (cpuBus.ReadWord(registers.Pc++) + registers.Y);
        }

        
        
    }

    public class ZeroPageAddressing : IAddressing
    {

        public ushort Addressing(Registers registers, IBus cpuBus)
        {
            return cpuBus.ReadByte(registers.Pc++);
        }

        
        
    }

    public class ZeroPageXAddressing : IAddressing
    {

        public ushort Addressing(Registers registers, IBus cpuBus)
        {
            return (ushort) ((cpuBus.ReadByte(registers.Pc++) + registers.X) & 0x00FF);
        }

        
        
    }

    public class ZeroPageYAddressing : IAddressing
    {

        public ushort Addressing(Registers registers, IBus cpuBus)
        {
            return (ushort) ((cpuBus.ReadByte(registers.Pc++) + registers.Y) & 0x00FF);
        }

        

    
    }

    public class RelativeAddressing : IAddressing
    {

        public ushort Addressing(Registers registers, IBus cpuBus)
        {
            var op = unchecked((sbyte) cpuBus.ReadByte(registers.Pc++));
            return (ushort) (registers.Pc + op);
        }

        
        
    }

    public class IndirectAddressing : IAddressing
    {

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

        public ushort Addressing(Registers registers, IBus cpuBus)
        {
            return cpuBus.ReadWord((ushort) (registers.X + cpuBus.ReadByte(registers.Pc++)));
        }

        
    }

    public class IndirectYAddressing : IAddressing
    {

        public ushort Addressing(Registers registers, IBus cpuBus)
        {
            var address = cpuBus.ReadWord(cpuBus.ReadByte(registers.Pc++));
            return (ushort) (address + registers.Y);
        }

        

    }
}