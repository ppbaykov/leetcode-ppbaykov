// See https://aka.ms/new-console-template for more information

using leetcode1Console;

var decision = new Solution();
var input = new int[] { 3,2,4 };
var twoDigits = decision.TwoSum(input, 6);

Console.WriteLine(twoDigits[0]+twoDigits[1]);