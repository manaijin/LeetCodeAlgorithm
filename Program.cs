using System;
using System.Collections.Generic;

namespace LeetCodeAlgorithm
{
    class Program
    {
        static void Main(string[] args)
        {
            LengthOfLongestSubstring("abcabcbb");
            Console.WriteLine("Hello World!");
        }

        /// <summary>
        /// 1. 求两数指定和的index
        /// 给定一个整数数组nums和一个整数目标值target，
        /// 请你在该数组中找出 和为目标值的那两个整数，并返回它们的数组下标。
        /// </summary>
        /// <param name="nums"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        public static int[] TwoSum(int[] nums, int target)
        {
            int[] result = new int[2];
            // 利用字典的Hash快速搜索减少二重遍历
            Dictionary<int, int> num2index = new Dictionary<int, int>();

            for (int i = 0; i < nums.Length; i++)
            {
                int num = nums[i];
                int neenNum = target - num;
                int index;

                if (num2index.TryGetValue(neenNum, out index))
                {
                    result[1] = i;
                    result[0] = index;
                    break;
                }
                else
                {
                    num2index[num] = i;
                }
            }
            return result;
        }


        public class ListNode
        {
            public int val;
            public ListNode next;
            public ListNode(int val = 0, ListNode next = null)
            {
                this.val = val;
                this.next = next;
            }
        }

        public static ListNode AddTwoNumbers(ListNode l1, ListNode l2)
        {
            var result = new ListNode();
            ListNode node;
            int add = 0;
            node = result;
            for (ListNode item1 = l1, item2 = l2; item1 != null || item2 != null; item1 = item1?.next, item2 = item2?.next)
            {
                int val1 = 0;
                if (item1 != null)
                    val1 = item1.val;

                int val2 = 0;
                if (item2 != null)
                    val2 = item2.val;

                int sum = val1 + val2 + add;
                int mod = sum % 10;
                add = (sum >= 10) ? 1 : 0;

                node.next = new ListNode(mod);
                node = node.next;
            }

            return result.next;
        }

        /// <summary>
        /// 3. 无重复字符的最长子串
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static int LengthOfLongestSubstring(string s)
        {
            int max = 0;
            Dictionary<char, int> sub_str_map = new Dictionary<char, int>();
            int count = s.Length;
            for (int i = 0; i < count; i++)
            {
                sub_str_map.Clear();
                sub_str_map.Add(s[i], 1);
                int length = 1;
                for (int j = i + 1; j < count; j++)
                {
                    if (!sub_str_map.ContainsKey(s[j]))
                    {
                        sub_str_map.Add(s[j], 1);
                        length++;
                    }
                    else
                        break;
                }
                if (length > max)
                    max = length;

                if (max >= count - i)
                    return max;
            }
            return max;
        }
    }
}
