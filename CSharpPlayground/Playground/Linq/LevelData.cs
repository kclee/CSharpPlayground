using System.Collections.Generic;
using System.Linq;

namespace Playground.Linq
{
    public static class LevelData
    {
        public static class LevelDataCompare
        {
            public static void RunDistinctTest()
            {
                SimpleLevelData[] levels = new SimpleLevelData[]
                {
                    new SimpleLevelData(1, new int[] {1,2}, new decimal[] {3m,5m}),
                    new SimpleLevelData(1, new int[] {1,2}, new decimal[] {3m,5m}),
                    new SimpleLevelData(1, new int[] {1,2,3}, new decimal[] {3m,5m}),
                };

                // For complex objects,
                // Default only compare by reference, so no effects after Distinct()
                var levelsAfterDistinceByReference = levels.Distinct().ToList();
                // levels = [ { Difficulty = 1; PassCodes = [1, 2]; TreePositions = [3m, 5m] },
                //            { Difficulty = 1; PassCodes = [1, 2]; TreePositions = [3m, 5m] },
                //            { Difficulty = 1; PassCodes = [1, 2, 3]; TreePositions = [3m, 5m] } ]

                // Needs to provide a custom IEqualityComparer
                var levelsAfterDistinceByValue = levels.Distinct(new SimpleLevelDataComparer()).ToList();
                // levels = [ { Difficulty = 1; PassCodes = [1, 2]; TreePositions = [3m, 5m] },
                //            { Difficulty = 1; PassCodes = [1, 2, 3]; TreePositions = [3m, 5m] } ]
            }
        }

        public sealed class SimpleLevelData
        {
            public SimpleLevelData(int difficulty, int[] passCodes, decimal[] treePositions)
            {
                Difficulty = difficulty;
                PassCodes = passCodes;
                TreePositions = treePositions;
            }

            public readonly int       Difficulty;     // Difficulty of this level. (1 is easiest)
            public readonly int[]     PassCodes;      // Codes for enter this level
            public readonly decimal[] TreePositions;  // Positions for all the trees
        }

        public class SimpleLevelDataComparer : IEqualityComparer<SimpleLevelData>
        {
            public bool Equals(SimpleLevelData d1, SimpleLevelData d2)
            {
                // compare all the values in SimpleLevelData
                return d1.Difficulty.Equals(d2.Difficulty)
                    && d1.PassCodes.SequenceEqual(d2.PassCodes)
                    && d1.TreePositions.SequenceEqual(d2.TreePositions);
            }

            public int GetHashCode(SimpleLevelData levelData)
            {
                // Note* different strategies to generate HashCode
                // (idea is try to avoid Hash collision)
                return levelData.Difficulty.GetHashCode();
            }
        }
    }
}
