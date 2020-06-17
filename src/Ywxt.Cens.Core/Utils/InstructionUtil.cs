namespace Ywxt.Cens.Core.Utils
{
    public static class InstructionUtil
    {
        public static int GetClockCycleByCrossingPage(ushort pc, ushort address)
        {
            return (pc & 0xFF00) != (address & 0xFF00) ? 1 : 0;
        }
    }
}