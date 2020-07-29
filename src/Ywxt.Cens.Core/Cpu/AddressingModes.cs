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
        private static readonly ImplicitAddressing ImplicitAddressingMode = new ImplicitAddressing();

        private static readonly AccumulatorAddressing AccumulatorAddressingMode = new AccumulatorAddressing();

        private static readonly ImmediateAddressing ImmediateAddressingMode = new ImmediateAddressing();

        private static readonly AbsoluteAddressing AbsoluteAddressingMode = new AbsoluteAddressing();

        private static readonly AbsoluteXAddressing AbsoluteXAddressingMode = new AbsoluteXAddressing();

        private static readonly AbsoluteYAddressing AbsoluteYAddressingMode = new AbsoluteYAddressing();

        private static readonly ZeroPageAddressing ZeroPageAddressingMode = new ZeroPageAddressing();

        private static readonly ZeroPageXAddressing ZeroPageXAddressingMode = new ZeroPageXAddressing();

        private static readonly ZeroPageYAddressing ZeroPageYAddressingMode = new ZeroPageYAddressing();

        private static readonly RelativeAddressing RelativeAddressingMode = new RelativeAddressing();

        private static readonly IndirectAddressing IndirectAddressingMode = new IndirectAddressing();

        private static readonly IndirectXAddressing IndirectXAddressingMode = new IndirectXAddressing();

        private static readonly IndirectYAddressing IndirectYAddressingMode = new IndirectYAddressing();

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