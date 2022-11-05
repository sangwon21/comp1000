using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Lab7
{
    class Program
    {
        static void Main(string[] args)
        {
            Frame frame1 = new Frame(1, "Ray-Ban");

            frame1.ToggleFeatures(EFeatureFlags.Aviator | EFeatureFlags.Red);
            Debug.Assert(frame1.Features == (EFeatureFlags.Aviator | EFeatureFlags.Red));

            frame1.ToggleFeatures(EFeatureFlags.Aviator);
            Debug.Assert(frame1.Features == EFeatureFlags.Red);

            frame1.TurnOffFeatures(EFeatureFlags.Aviator | EFeatureFlags.Red);
            Debug.Assert(frame1.Features == 0);

            frame1.TurnOnFeatures(EFeatureFlags.Blue | EFeatureFlags.Black);
            Debug.Assert(frame1.Features == (EFeatureFlags.Blue | EFeatureFlags.Black));

            frame1.TurnOnFeatures(EFeatureFlags.Men | EFeatureFlags.Women);
            Debug.Assert(frame1.Features == (EFeatureFlags.Blue | EFeatureFlags.Black | EFeatureFlags.Men | EFeatureFlags.Women));

            List<Frame> frames = new List<Frame>
            {
                new Frame(2, "Joseph-Marc"),
                new Frame(3, "Derek Cardigan"),
                new Frame(4, "Randy Jackson"),
                new Frame(5, "Evergreen"),
                new Frame(6, "Emporio Armani"),
                new Frame(7, "Carrera"),
                new Frame(8, "Crocs")
            };

            frames[0].TurnOnFeatures(EFeatureFlags.Men | EFeatureFlags.Women | EFeatureFlags.Rectangle | EFeatureFlags.Blue);
            frames[1].TurnOnFeatures(EFeatureFlags.Women | EFeatureFlags.Black);
            frames[2].TurnOnFeatures(EFeatureFlags.Aviator | EFeatureFlags.Red | EFeatureFlags.Black);
            frames[3].TurnOnFeatures(EFeatureFlags.Round);
            frames[4].TurnOnFeatures(EFeatureFlags.Round | EFeatureFlags.Red);
            frames[5].TurnOnFeatures(EFeatureFlags.Men | EFeatureFlags.Blue | EFeatureFlags.Black);
            frames[6].TurnOnFeatures(EFeatureFlags.Black);

            List<Frame> filteredFrames = FilterEngine.FilterFrames(frames, EFeatureFlags.Men);

            Debug.Assert(filteredFrames.Count == 2);
            Debug.Assert(filteredFrames[0].ID == frames[0].ID);
            Debug.Assert(filteredFrames[1].ID == frames[5].ID);

            filteredFrames = FilterEngine.FilterFrames(frames, EFeatureFlags.Men | EFeatureFlags.Red | EFeatureFlags.Aviator);
            Debug.Assert(filteredFrames.Count == 4);
            Debug.Assert(filteredFrames[0].ID == frames[0].ID);
            Debug.Assert(filteredFrames[1].ID == frames[2].ID);
            Debug.Assert(filteredFrames[2].ID == frames[4].ID);
            Debug.Assert(filteredFrames[3].ID == frames[5].ID);

            List<Frame> filteredOutFrames = FilterEngine.FilterOutFrames(frames, EFeatureFlags.Aviator | EFeatureFlags.Women | EFeatureFlags.Red);
            Debug.Assert(filteredOutFrames.Count == 3);
            Debug.Assert(filteredOutFrames[0].ID == frames[3].ID);
            Debug.Assert(filteredOutFrames[1].ID == frames[5].ID);
            Debug.Assert(filteredOutFrames[2].ID == frames[6].ID);

            List<Frame> frames2 = new List<Frame>
            {
                new Frame(9, "Kam Dhillon"),
                frames[0],
                frames[3],
                new Frame(10, "Dior"),
                new Frame(11, "Calvin Klein"),
                frames[5],
                frames[6],
                new Frame(12, "Lacoste"),
            };

            List<Frame> intersect = FilterEngine.Intersect(frames, frames2);

            Debug.Assert(intersect.Count == 4);
            Debug.Assert(intersect.FirstOrDefault(i => i.ID == frames[0].ID) != null);
            Debug.Assert(intersect.FirstOrDefault(i => i.ID == frames[3].ID) != null);
            Debug.Assert(intersect.FirstOrDefault(i => i.ID == frames[5].ID) != null);
            Debug.Assert(intersect.FirstOrDefault(i => i.ID == frames[6].ID) != null);

            List<int> sortKeys = FilterEngine.GetSortKeys(frames, new List<EFeatureFlags> { EFeatureFlags.Aviator, EFeatureFlags.Men, EFeatureFlags.Rectangle, EFeatureFlags.Red });
            Debug.Assert(sortKeys.Count == frames.Count);

            List<Frame> sortedFrames = sort(sortKeys, frames);

            Debug.Assert(sortedFrames[0].ID == frames[2].ID);
            Debug.Assert(sortedFrames[1].ID == frames[0].ID);
            Debug.Assert(sortedFrames[2].ID == frames[5].ID);
            Debug.Assert(sortedFrames[3].ID == frames[4].ID);
            Debug.Assert(sortedFrames[4].ID == frames[1].ID || sortedFrames[4].ID == frames[3].ID || sortedFrames[4].ID == frames[6].ID);
            Debug.Assert(sortedFrames[5].ID == frames[1].ID || sortedFrames[5].ID == frames[3].ID || sortedFrames[5].ID == frames[6].ID);
            Debug.Assert(sortedFrames[6].ID == frames[1].ID || sortedFrames[6].ID == frames[3].ID || sortedFrames[6].ID == frames[6].ID);

            sortKeys = FilterEngine.GetSortKeys(frames, new List<EFeatureFlags> { EFeatureFlags.Rectangle, EFeatureFlags.Black, EFeatureFlags.Women });
            Debug.Assert(sortKeys.Count == frames.Count);

            sortedFrames = sort(sortKeys, frames);

            Debug.Assert(sortedFrames[0].ID == frames[0].ID);
            Debug.Assert(sortedFrames[1].ID == frames[1].ID);
            Debug.Assert(sortedFrames[2].ID == frames[2].ID || sortedFrames[2].ID == frames[5].ID || sortedFrames[2].ID == frames[6].ID);
            Debug.Assert(sortedFrames[3].ID == frames[2].ID || sortedFrames[3].ID == frames[5].ID || sortedFrames[3].ID == frames[6].ID);
            Debug.Assert(sortedFrames[4].ID == frames[2].ID || sortedFrames[4].ID == frames[5].ID || sortedFrames[4].ID == frames[6].ID);
            Debug.Assert(sortedFrames[5].ID == frames[3].ID || sortedFrames[5].ID == frames[4].ID);
            Debug.Assert(sortedFrames[6].ID == frames[3].ID || sortedFrames[6].ID == frames[4].ID);
        }

        private static List<Frame> sort(List<int> sortKeys, List<Frame> frames)
        {
            List<Tuple<int, Frame>> tuples = new List<Tuple<int, Frame>>();
            for (int i = 0; i < sortKeys.Count; i++)
            {
                tuples.Add(new Tuple<int, Frame>(sortKeys[i], frames[i]));
            }

            tuples.Sort((t1, t2) =>
            {
                return t2.Item1 - t1.Item1;
            });

            return tuples.Select(t => t.Item2).ToList();
        }
    }
}