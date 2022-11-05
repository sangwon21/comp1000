using System;
using System.Collections.Generic;

namespace Assignment3
{
    public class StepMaker
    {
        private const int THRESHOLD = 10;
        private const int INVALID_INDEX = -1;
        private const int BETWEEN_LENGTH = 6;
        public static List<int> MakeSteps(int[] steps, INoise noise)
        {
            return MakeStepsHelper(steps, noise, 0);
        }

        private static int GetDivergentStepIndex(int[] steps)
        {
            for (int i = 0; i < steps.Length - 1; ++i)
            {
                if (Math.Abs(steps[i + 1] - steps[i]) > THRESHOLD)
                {
                    return i;
                }
            }

            return -1;
        }

        private static List<int> MakeStepsHelper(int[] steps, INoise noise, int currentRecursiveLevel)
        {
            if (GetDivergentStepIndex(steps) == INVALID_INDEX)
            {
                return new List<int>(steps);
            }

            List<int> outStepList = new List<int>();

            /**
             * right 때문에 index를 끝까지 돌면 안 된다
             */
            for (int i = 0; i < steps.Length - 1; ++i)
            {
                int left = steps[i];
                int right = steps[i + 1];

                outStepList.Add(left);

                if (Math.Abs(left - right) > THRESHOLD)
                {
                    int[] between = new int[BETWEEN_LENGTH];
                    between[0] = left;
                    between[5] = right;

                    for (int j = 1; j < BETWEEN_LENGTH - 1; ++j)
                    {
                        between[j] = GetLinearInterpolatedValue(left, right, j, BETWEEN_LENGTH - 1 - j) + noise.GetNext(currentRecursiveLevel);
                    }

                    List<int> splitted = MakeStepsHelper(between, noise, currentRecursiveLevel + 1);

                    /**
                     * left는 밖의 for문에서, right는 다음 번 밖의 for문에서 포함된다
                     */
                    for (int j = 1; j < splitted.Count - 1; ++j)
                    {
                        outStepList.Add(splitted[j]);
                    }
                }
            }

            /**
             * right 때문에 제외된 요소 추가
             */
            outStepList.Add(steps[steps.Length - 1]);
            return outStepList;
        }

        private static int GetLinearInterpolatedValue(int left, int right, int leftWeight, int rightWeight)
        {
            return (leftWeight * right + rightWeight * left) / (leftWeight + rightWeight); ;
        }
    }


}
