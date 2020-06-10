namespace Ywxt.Cens.Core.Utils
{
    public static class InstructionUtil
    {
        public static int GetClockCycleByCrossingPage(bool pageCrossed)
        {
            return pageCrossed ? 1 : 0;
        }
    }
}