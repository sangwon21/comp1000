namespace Assignment4.Image
{
    public struct Color
    {
        public byte R { get; private set; }
        public byte G { get; private set; }
        public byte B { get; private set; }

        public Color(byte r, byte g, byte b)
        {
            R = r;
            G = g;
            B = b;
        }

        public static Color FromArgb(byte r, byte g, byte b)
        {
            return new Color(r, g, b);
        }
    }
}
