namespace Assignment3
{
    public sealed class ZeroNoise : INoise
    {
        public int GetNext(int level)
        {
            return 0;
        }
    }
}
