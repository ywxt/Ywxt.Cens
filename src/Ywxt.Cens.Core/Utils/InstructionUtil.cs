namespace Ywxt.Cens.Core.Utils
{
    public static class InstructionUtil
    {
        public static int GetCrossingPageClockCycles(bool pageCrossed) => pageCrossed ? 1 : 0;

        public static bool IsPageCrossed(ushort old, ushort @new) => (old & 0xFF00) != (@new & 0xFF00);

        public static int GetBranchClockCycle(bool success) => success ? 1 : 0;
    }
}