using System;
using Ywxt.Cens.Core.Exceptions;

namespace Ywxt.Cens.Core.Cpu
{
    public enum AddressingMode
    {
        ImplicitAddressingMode,
        AccumulatorAddressingMode,
        ImmediateAddressingMode,
        AbsoluteAddressingMode,
        AbsoluteXAddressingMode,
        AbsoluteYAddressingMode,
        ZeroPageAddressingMode,
        ZeroPageXAddressingMode,
        ZeroPageYAddressingMode,
        RelativeAddressingMode,
        IndirectAddressingMode,
        IndirectXAddressingMode,
        IndirectYAddressingMode,
        
    }
    public static class AddressingModes
    {
        public static ImplicitAddressing ImplicitAddressingMode = new ImplicitAddressing();
        
        public static AccumulatorAddressing AccumulatorAddressingMode = new AccumulatorAddressing();
        
        public static ImmediateAddressing ImmediateAddressingMode = new ImmediateAddressing();
        
        public static AbsoluteAddressing AbsoluteAddressingMode = new AbsoluteAddressing();
        
        public static AbsoluteXAddressing AbsoluteXAddressingMode = new AbsoluteXAddressing();
        
        public static AbsoluteYAddressing AbsoluteYAddressingMode = new AbsoluteYAddressing();
        
        public static ZeroPageAddressing ZeroPageAddressingMode = new ZeroPageAddressing();
        
        public static ZeroPageXAddressing ZeroPageXAddressingMode = new ZeroPageXAddressing();
        
        public static ZeroPageYAddressing ZeroPageYAddressingMode = new ZeroPageYAddressing();
        
        public static RelativeAddressing RelativeAddressingMode = new RelativeAddressing();
        
        public static IndirectAddressing IndirectAddressingMode = new IndirectAddressing();
        
        public static IndirectXAddressing IndirectXAddressingMode = new IndirectXAddressing();
        
        public static IndirectYAddressing IndirectYAddressingMode = new IndirectYAddressing();

        public static IAddressing Get(AddressingMode addrMode)
        {
            return addrMode switch
            {
                AddressingMode.ImplicitAddressingMode => ImplicitAddressingMode,
                AddressingMode.AccumulatorAddressingMode => AccumulatorAddressingMode,
                AddressingMode.ImmediateAddressingMode => ImmediateAddressingMode,
                AddressingMode.AbsoluteAddressingMode => AbsoluteAddressingMode,
                AddressingMode.AbsoluteXAddressingMode => AbsoluteXAddressingMode,
                AddressingMode.AbsoluteYAddressingMode => AbsoluteYAddressingMode,
                AddressingMode.ZeroPageAddressingMode => ZeroPageAddressingMode,
                AddressingMode.ZeroPageXAddressingMode => ZeroPageXAddressingMode,
                AddressingMode.ZeroPageYAddressingMode => ZeroPageYAddressingMode,
                AddressingMode.RelativeAddressingMode => RelativeAddressingMode,
                AddressingMode.IndirectAddressingMode => IndirectAddressingMode,
                AddressingMode.IndirectXAddressingMode => IndirectXAddressingMode,
                AddressingMode.IndirectYAddressingMode => IndirectYAddressingMode,
                _ => throw new UnknownAddressingModeException(addrMode)
            };
        }
    }

   
}