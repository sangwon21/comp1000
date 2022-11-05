namespace Lab7
{
    public class Frame
    {
        public EFeatureFlags Features { get; private set; }
        public uint ID { get; private set; }
        public string Name { get; private set; }

        public Frame(uint id, string name)
        {
            ID = id;
            Name = name;
            Features = EFeatureFlags.Default;
        }

        public void ToggleFeatures(EFeatureFlags features)
        {
            Features ^= features;
        }

        public void TurnOnFeatures(EFeatureFlags features)
        {
            Features |= features;
        }
        public void TurnOffFeatures(EFeatureFlags features)
        {
            Features &= ~features;
        }

        public bool HasFeatures(EFeatureFlags features)
        {
            return (Features & features) != 0;
        }

        public bool IsSame(Frame other)
        {
            return Features == other.Features && Name == other.Name;
        }

        public bool IsSameFeature(EFeatureFlags features)
        {
            return (Features & features) == features;
        }
    }
}
