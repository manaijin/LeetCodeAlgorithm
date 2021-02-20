using System;
using System.Collections.Generic;

namespace LeetCodeAlgorithm
{
    class Program
    {
        static void Main(string[] args)
        {
            string s = Convert("PAYPALISHIRING", 3);
            Console.WriteLine(s);
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
        /// 关键：若k~m为不重复字符串，则k+n~m也为不重复字符串
        /// 因此使用双指针（滑动窗口）来处理,遍历字符串，end指针每次++，动态计算start指针
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static int LengthOfLongestSubstring(string s)
        {
            int max = 0;
            Dictionary<char, int> sub_str_map = new Dictionary<char, int>();
            int count = s.Length;
            int start_index = 0;
            int end_index = 0;
            while (end_index < count)
            {
                char c = s[end_index];
                // 记录最新的字符下标
                if (!sub_str_map.ContainsKey(c))
                    sub_str_map.Add(c, end_index);
                else
                {
                    start_index = Math.Max(sub_str_map[c] + 1, start_index);
                    sub_str_map[c] = end_index;
                }

                int length = end_index - start_index + 1;
                max = Math.Max(max, length);
                end_index++;
            }
            return max;

            //// 记录字符上一次出现的位置
            //int[] last = new int[128];
            //for (int i = 0; i < 128; i++)
            //{
            //    last[i] = -1;
            //}
            //int n = s.Length;

            //int max = 0;
            //int start = 0; // 窗口开始位置
            //for (int i = 0; i < n; i++)
            //{
            //    int index = s[i];
            //    start = Math.Max(start, last[index] + 1);
            //    max = Math.Max(max, i - start + 1);
            //    last[index] = i;
            //}

            //return max;
        }

        /// <summary>
        /// 4.两个数组的中位数
        /// </summary>
        /// <param name="nums1"></param>
        /// <param name="nums2"></param>
        /// <returns></returns>
        public static double FindMedianSortedArrays(int[] nums1, int[] nums2)
        {
            int m = nums1.Length;
            int n = nums2.Length;

            int count = m + n;
            bool flag = count % 2 == 0;
            int[] all_list = new int[count];
            double result = 0;
            int i = 0, j = 0;
            int index = 0;
            int max_index = flag ? count / 2 + 1 : (count + 1) / 2;

            while (i < m || j < n)
            {
                int item;
                if(i >= m)
                {
                    item = nums2[j];
                    j++;
                }else if(j >= n)
                {
                    item = nums1[i];
                    i++;
                }
                else if (nums1[i] < nums2[j])
                {
                    item = nums1[i];
                    i++;
                }
                else
                {
                    item = nums2[j];
                    j++;
                }

                all_list[index] = item;

                if(index >= max_index - 1)
                {
                    if (flag)
                        result = ((double)all_list[index - 1] + all_list[index]) / 2;
                    else
                        result = item;
                    break;
                }
                index++;
            };
            return result;
        }

        /// <summary>
        /// 5. 最长回文子串
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string LongestPalindrome(string s)
        {
            if (string.IsNullOrEmpty(s)) return s;
            int count = s.Length;
            if (count == 1) return s;

            string result = string.Empty;
            int max_length = 0;
            int count2 = count * 2 - 1;
            for (int i = 0; i < count2; i++)
            {
                // 计算中心点
                int center1;
                int center2;
                if(i % 2 == 0)
                    center1 = center2 = i / 2;
                else
                {
                    center1 = i / 2;
                    center2 = i / 2 + 1;
                }

                int left_index = center1, right_index = center2;
                while(left_index >= 0 && right_index < count)
                {
                    if (s[left_index] == s[right_index])
                    {
                        // 拓展中心点
                        int length = right_index - left_index + 1;
                        // 更新结果
                        if (length > max_length)
                        {
                            max_length = length;
                            result = s.Substring(left_index, max_length);
                        }
                        left_index--;
                        right_index++;
                    }
                    else
                        break;
                }
            }
            return result;
        }

        public static string Convert(string s, int numRows)
        {
            if (string.IsNullOrEmpty(s)) return s;
            if (numRows <= 1) return s;

            Dictionary<int, List<char>> map = new Dictionary<int, List<char>>(numRows);
            string result = string.Empty;
            int count = s.Length;
            int max_cir = numRows * 2 - 2;
            // 遍历字符串
            for (int i = 0; i < count; i++)
            {
                int index = i % max_cir;
                int raw = index < numRows ? index : numRows * 2 - index - 2;
                if (map.ContainsKey(raw))
                    map[raw].Add(s[i]);
                else
                    map.Add(raw, new List<char>() { s[i] });
            }

            // 生成结果
            foreach(var item in map.Values)
                result += new string(item.ToArray());
            return result;
        }
    }
}
