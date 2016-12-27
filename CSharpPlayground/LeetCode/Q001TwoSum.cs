using System.Collections.Generic;

namespace LeetCode
{
    class Q001TwoSum
    {
        public static void RunQ001()
        {
            int[] nums = new int[] {2,7,11,15};
            int target = 9;

            int[] result = TwoSum(nums, target);

            foreach (int i in result)
            { 
                System.Diagnostics.Debug.WriteLine(i);
            }
        }

        public static int[] TwoSum(int[] nums, int target)
        {
            System.Collections.Generic.Dictionary<int, int> dict = new Dictionary<int, int>();

            for (int i = 0; i < nums.Length; ++i)
            {
                if (dict.ContainsKey(target - nums[i]))
                {
                    return new int[] { i, dict[target - nums[i]] };
                }

                dict.Add(nums[i], i);
            }
            return new int[] { 0, 0 };
        }
    }
}
