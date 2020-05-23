using System.Collections.Generic;

namespace Ywxt.Cens.Core.Cpu
{
    public interface IInstruction
    {
        public IReadOnlyList<byte> OpCodes { get;}

        public (int deferCycle, ushort dataSize) Invoke(ICpu cpu, byte instruction);
        
        
    }
}