using System;
using System.Collections.Generic;

namespace Lab7
{
    public static class FilterEngine
    {
        public static List<Frame> FilterFrames(List<Frame> frames, EFeatureFlags features)
        {
            List<Frame> filteredFrames = new List<Frame>();

            foreach (Frame frame in frames)
            {
                if ((frame.HasFeatures(features)))
                {
                    filteredFrames.Add(frame);
                }
            }

            return filteredFrames;
        }
        public static List<Frame> FilterOutFrames(List<Frame> frames, EFeatureFlags features)
        {
            List<Frame> filteredFrames = new List<Frame>();

            foreach (Frame frame in frames)
            {
                if (frame.HasFeatures(features) == false)
                {
                    filteredFrames.Add(frame);
                }
            }

            return filteredFrames;
        }

        public static List<Frame> Intersect(List<Frame> frames1, List<Frame> frames2)
        {
            List<Frame> sharedFrames = new List<Frame>();

            foreach (Frame frame in frames1)
            {
                foreach (Frame frame2 in frames2)
                {
                    if (frame.IsSame(frame2))
                    {
                        sharedFrames.Add(frame);
                        break;
                    }
                }
            }

            return sharedFrames;
        }

        public static List<int> GetSortKeys(List<Frame> frames, List<EFeatureFlags> features)
        {
            List<int> outFrames = new List<int>(frames.Count);

            for (int i = 0; i < frames.Count; ++i)
            {
                Frame frame = frames[i];
                outFrames.Add(0);

                for (int j = 0; j < features.Count; ++j)
                {
                    EFeatureFlags featureFlags = features[j];

                    if (frame.IsSameFeature(featureFlags))
                    {
                        outFrames[i] += (int)Math.Pow(2, 7 - j);
                    }
                }
            }

            return outFrames;
        }


    }
}
