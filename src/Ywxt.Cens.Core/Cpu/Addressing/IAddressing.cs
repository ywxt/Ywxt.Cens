namespace Ywxt.Cens.Core.Cpu.Addressing
{
    public interface IAddressing
    {
        Registers Registers { get; }
        
        IBus CpuBus { get; }
        ushort Addressing(byte op1 = 0, byte op2 = 0);
    }
}