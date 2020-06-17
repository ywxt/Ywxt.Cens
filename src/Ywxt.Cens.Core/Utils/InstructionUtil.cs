namespace Ywxt.Cens.Core.Utils
{
    public static class InstructionUtil
    {
        public static int GetClockCycleByCrossingPage(ushort pc, ushort address)
        {
            return (pc & 0xFF00) != (address & 0xFF00) ? 1 : 0;
        }

        public static bool IsPageCrossed(ushort old, ushort @new)
        {
            return (old & 0xFF00) != (@new & 0xFF00);
        }
    }
}