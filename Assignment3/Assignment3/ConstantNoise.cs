namespace Assignment3
{
    public sealed class ConstantNoise : INoise
    {
        public int GetNext(int level)
        {
            return 1;
        }
    }
}
