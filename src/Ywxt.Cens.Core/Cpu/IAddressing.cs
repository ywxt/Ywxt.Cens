namespace Ywxt.Cens.Core.Cpu
{
    public interface IAddressing
    {
        ushort Addressing(Registers registers,IBus cpuBus);
    }
}