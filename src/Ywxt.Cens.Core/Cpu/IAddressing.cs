namespace Ywxt.Cens.Core.Cpu
{
    public interface IAddressing
    {
        AddressingType AddressingType { get; }
        ushort Addressing(Registers registers,IBus cpuBus);
    }
}