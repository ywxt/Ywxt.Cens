namespace Ywxt.Cens.Core.Cpu
{
    public interface IAddressing
    {
        AddressingType AddressingType { get; }
        (ushort address, bool pageCrossed) Addressing(CpuRegisters registers, IBus cpuBus);
    }
}