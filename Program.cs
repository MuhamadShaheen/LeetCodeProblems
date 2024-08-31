using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LeetCodeBLIND75_Problems
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int[] nums = new int[] { 0 , 1 ,3 ,4 ,0 , 8 };
            MoveZeroes(nums);
        }
        static public int[] TwoSum(int[] nums, int target) //O(n2)
        {
            int[] result = new int[2];
            for (int i = 0; i < nums.Length; i++)
            {
                for (int j = 0; j < nums.Length; j++)
                {
                    if (nums[i] + nums[j] == target && i != j)
                    {
                        result[0] = i;
                        result[1] = j;
                    }
                }
            }
            return result;

        }

        static public int[] TwoSumEffecient(int[] nums, int target) //O(n log n)
        {

            int[] OrderedNums = nums.OrderBy(x => x).ToArray(); // Not Guaranteed That the array is sorted so we must sort it first
            int[] result = new int[2];
            int StartIndex = 0;
            int EndIndex = OrderedNums.Length - 1;
            int Start = OrderedNums[StartIndex];
            int End = OrderedNums[EndIndex];

            while (Start + End != target)
            {
                if (Start + End > target)
                {
                    EndIndex--;
                    End = OrderedNums[EndIndex];
                }
                else if (Start + End < target)
                {
                    StartIndex++;
                    Start = OrderedNums[StartIndex];
                }
            }
            result[0] = Array.FindIndex(nums, x => x == OrderedNums[StartIndex]);
            result[1] = Array.FindLastIndex(nums , x => x == OrderedNums[EndIndex]);

            return result;

        }
        static public int[] TwoSumImproved(int[] nums, int target) // O(N)
        {
            Dictionary<int, int> ValuesWithTheirIndecies = new Dictionary<int, int>();
            int Difference = 0;
            for (int i = 0; i < nums.Length; i++)
            {
                Difference = target - nums[i];
                if (ValuesWithTheirIndecies.ContainsKey(Difference))
                {
                    return new int[] { i, ValuesWithTheirIndecies[Difference] };
                }
                else
                {
                    if (!ValuesWithTheirIndecies.ContainsKey(nums[i]))
                    {
                        ValuesWithTheirIndecies.Add(nums[i], i);
                    }
                }
            }
            return new int[2] { -1, -1 }; // will never reach because its garanteed that solution exists
        }
        static public int[] TwoSumII(int[] numbers, int target) // Now its guaranteed that its sorted
        {

            int[] result = new int[2];
            int StartIndex = 0;
            int EndIndex = numbers.Length - 1;
            int Start = numbers[StartIndex];
            int End = numbers[EndIndex];

            while (Start + End != target)
            {
                if (Start + End > target)
                {
                    EndIndex--;
                    End = numbers[EndIndex];
                }
                else if (Start + End < target)
                {
                    StartIndex++;
                    Start = numbers[StartIndex];
                }
            }
            result[0] = Array.FindIndex(numbers, x => x == numbers[StartIndex]) + 1; // the solution want it 1-based not 0-based index.. that's why we increment by 1
            result[1] = Array.FindLastIndex(numbers, x => x == numbers[EndIndex]) + 1;

            return result;
        }
        
        static public int MaxProfit(int[] prices) //using two pointer technique
        {
            int left = 0; //Left Pointer For Buy
            int right = 1;// Right ptr for Sale
            int currentProfit = 0;
            int Profit = 0;
            while (right < prices.Length)
            {
                if (prices[left] < prices[right]) //Check if profitable
                {
                    Profit = prices[right] - prices[left];
                    currentProfit = Math.Max(currentProfit, Profit);
                }
                else
                {
                    left = right;
                }
                                    right++;
            }

            return currentProfit;
        }

        static public bool ContainsDuplicate(int[] nums)
        {
            var Counter = nums.Distinct().Count();

            return nums.Length != Counter; 
        }

        static public int ProductOf(int[]nums) //Helper Function
        {
            int result = 1;

            foreach (var num in nums)
            {
                result *= num;
            }
            return result;
        }
        static public int[] ProductOfArrayExceptSelf(int[] nums) // 1 , 2 ,4 , 6
        {
            int ProductOfEntireArray = ProductOf(nums);
            int[] ProductArray = new int[nums.Length];

            for (int i =0; i < nums.Length; i++)
            {
                ProductArray[i] = ProductOfEntireArray / nums[i];
            }

            return ProductArray;
        }
        static public int MaximumSubArray(int[] nums) // O(N2) // int[] nums = new int[] { 2, -5, 10, -2, 6, -3 };
        {
            int Maximum = Int32.MinValue;

            for (int i = 0; i < nums.Length; i++)
            {
                int WindowSum = 0;

                for (int j = i; j < nums.Length; j++)
                {
                    WindowSum += nums[j];

                    Maximum = Math.Max(WindowSum, Maximum);
                }
            }
            return Maximum;
        }

        static public int BinarySearch(int[] SortedNumbers, int Element) // O(log n)
        {
            int Start = 0;
            int End = SortedNumbers.Length -1; //index
            int Middle = Start + (End - Start) / 2;
            while (Start <= End)
            {
                if (SortedNumbers[Middle] == Element)
                {
                    return Middle;
                }
                else if (SortedNumbers[Middle] > Element)
                {
                    End = Middle - 1;
                }
                else
                {
                    Start = Middle + 1;
                }
                Middle = Start + (End - Start) / 2;
            }

            return -1;
        }
        static public int SearchInRotatedSortedArray(int[] nums , int target) // O(Log n) انا تعذبتتتتتتتتتتتت حتى عرفت احلها اخيرننننننننننننننن
        {
            int Start = 0;
            int End = nums.Length - 1;
            int Middle = Start + (End - Start) / 2;

            if (nums[Middle] > nums[Start] && nums[Middle] < nums[End])
            {
                return BinarySearch(nums, target);
            }
            while (Start <= End)
            {
                if (nums[Middle] > nums[Start] && nums[Middle] < nums[End])
                {
                    int[] temp = new int[nums.Length - Start];
                    Array.Copy(nums , Start , temp , 0 , temp.Length );
                    int tempResult = BinarySearch(temp, target);

                    if (tempResult != -1)
                    return tempResult + nums.Length - temp.Length;
                }
                if (nums[Start] == target)
                {
                    return Start;
                }
                if (nums[End] == target)
                {
                    return End;
                }
                if (nums[Middle] == target)
                {
                    return Middle; //Found.. Return index
                }
                else if (nums[Middle] > nums[Start])
                {
                    if (target < nums[Middle] && target > nums[Start])
                    {
                        End = Middle - 1;
                    }
                    else
                    {
                        Start = Middle + 1;
                    }
                }
                else
                {
                    if (target > nums[Middle] && target < nums[End])
                    {
                        Start = Middle + 1;
                    }
                    else
                    {
                        End = Middle - 1;
                    }
                }
                Middle = Start + (End - Start) / 2;
            }

            return -1;
        }
        static public bool IsPalindrome(int num)
        {
            string sNum = num.ToString();
            string ReversedNum = "";

            for(int i = sNum.Length -1; i >=0; i--)
            {
                ReversedNum += sNum[i];
            }
            return ReversedNum == sNum;
        }

        public enum RomanSymbols
        {
            I = 1,
            V = 5,
            X = 10,
            L = 50,
            C = 100,
            D = 500,
            M = 1000,
        }
        static public int RomanToInt(string s)
        {
            int Result = 0;
            Dictionary<char, int> RomanValues = new Dictionary<char, int>();

            RomanValues.Add('I', 1);
            RomanValues.Add('V', 5);
            RomanValues.Add('X', 10);
            RomanValues.Add('L', 50);
            RomanValues.Add('C', 100);
            RomanValues.Add('D', 500);
            RomanValues.Add('M', 1000); //IV , IX, XL ,XC, CD , CM

            s.Replace("IV", "IIII");
            s.Replace("IX", "VIIII");
            s.Replace("XL", "XXXX");
            s.Replace("XC", "LXXXX");
            s.Replace("CD", "CCCC");
            s.Replace("CM", "DCCCC");

            for (int i = 0; i < s.Length; i++)
            {
                Result += RomanValues[s[i]];
            }

            return Result;
        }

        static public int RemoveDuplicates(int[] nums)
        {
            nums = nums.Distinct().ToArray();

            return nums.Length;
        }
        static public int StrStr(string haystack, string needle)
        {

            if (haystack.Contains(needle))
            {
                return haystack.IndexOf(needle);
            }
            else
            {
                return -1;
            }
        }
        static public int[] PlusOnee(int[] digits)
        {
            string Digits = "";
            for (int i = 0; i < digits.Length; i++)
            {
                Digits += digits[i];
            }
            var ActualDigit = Convert.ToInt64(Digits);
            var Result = ActualDigit + 1;
            string ResultToString = Result.ToString();

            int[] result = new int[ResultToString.Length];

            for (int i = 0; i < result.Length; i++)
            {
                result[i] =  ResultToString[i] - 48;
            }

            return result;
        }
        static public int[] PlusOne(int[] digits) // 0000
        {
            for (int i = digits.Length- 1; i >= 0; i--)
            {
                if (digits[i] < 9)
                {
                    digits[i]++;
                    return digits;
                }

                digits[i] = 0;
            }
            int[] newDigits = new int[digits.Length + 1];
            newDigits[0] = 1;
            return newDigits;
        }
        static public int MajorityElement(int[] nums)
        {
            var Distinct = nums.Distinct().ToArray();
            int Counter = 0;
            for (int i = 0; i < Distinct.Length; i++)
            {
                Counter = nums.Count(x => x == Distinct[i]);
                if (Counter > nums.Length / 2)
                {
                    return Distinct[i];
                }
            }
            return -1;
        }
        static public int LengthOfLastWord(string s) // Hello World
        {
            s = s.Trim();
            int Counter = 0;
            for (int i = s.Length -1; i >= 0; i--)
            {
                if (s[i] == ' ')
                {
                    break;
                }
                else
                {
                    Counter++;
                }
            }
            return Counter;
        }
        static public string RemoveNonAlpha(string input)
        {
            return Regex.Replace(input, "[^a-zA-Z]", "");
        }
        public static string RemoveNonAlphaNumeric(string input)
        {
            return Regex.Replace(input, "[^a-zA-Z0-9]", "");
        }
        static public bool IsPalindrome(string s)
        {
            s = RemoveNonAlphaNumeric(s).ToLower();

            var Reversed = new string(s.Reverse().ToArray());
            return Reversed == s;
            
        }
        static public int SingleNumber(int[] nums)
        {
            for (int i = 0; i < nums.Length; i++)
            {
                if (nums.Count(x => x == nums[i]) == 1)
                {
                    return nums[i];
                }
            }
            return -1;
        }
        static public int AddDigits(int num)
        {
            if (num <= 9)
            {
                return num;
            }
            string number = num.ToString();
            int n = 0;
            for (int i = 0; i < number.Length;i++)
            {
                n += number[i] - 48;
            }
            return AddDigits(n);
        }
        public static int[] RemoveLessThanOne(int[] inputArray) //Helper
        {
            return inputArray.Where(x => x >= 1).ToArray();
        }
        static public int MissingNumber(int[] nums)
        {
            if (nums.Length == 1 && nums[0] == 1)
            {
                return 0;
            }
            int[] num = new int[nums.Length];

            for (int i = 0; i < num.Length;i++)
            {
                num[i] = i + 1;
            }
            nums = RemoveLessThanOne(nums);

            var Difference = num.Except(nums);
            if (Difference.Count() == 0)
            {
                return 0;
            }

            return Difference.First();
        }
        static public void MoveZeroes(int[] nums) // [0 ,1 ,0, 3, 12] // [1 ,0 ,0 ,3 ,12]
        {
            int Length = nums.Length;

            while (Length != 1)
            {


                for (int j = 0; j < Length - 1; j++)
                {
                    if (nums[j] == 0)
                    {
                        Swap(ref nums[j], ref nums[j+1]);
                    }
                }
                Length--;
            }
        }
        public static void Swap(ref int a, ref int b)
        {
            int temp = a;
            a = b;
            b = temp;
        }
    }


}
