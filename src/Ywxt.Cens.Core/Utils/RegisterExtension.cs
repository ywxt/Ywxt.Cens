using Ywxt.Cens.Core.Cpu;

namespace Ywxt.Cens.Core.Utils
{
    public static class RegisterExtension
    {
        public static void SetZAndN(this Registers registers, byte flag)
        {
            if (flag == 0)
            {
                registers.P |= PFlags.Z;
            }
            else
            {
                registers.P &= ~PFlags.Z;
            }

            if (flag >> 7 == 1)
            {
                registers.P |= PFlags.N;
            }
            else
            {
                registers.P &= ~PFlags.N;
            }
        }
    }
}