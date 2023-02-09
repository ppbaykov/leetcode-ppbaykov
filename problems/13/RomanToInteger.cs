namespace leetcode;

public class RomanToInteger
{
   public static Dictionary<string, int> RomanToIntMap = new Dictionary<string, int>()
   {
      { "I", 1 },
      { "V", 5 },
      { "X", 10 },
      { "L", 50 },
      { "C", 100 },
      { "D", 500 },
      { "M", 1000 }
   };
   
   public static int Convert (string roman)
   {
      if (roman.Length == 0)
         return 0;

      if (roman.Length == 1)
         return RomanToIntMap[roman];

      // find first max
      var maxIndex = CountMaxIndex(roman);
      // count left
      var leftValue = Convert(roman.Substring(0, maxIndex));
      // count right
      var rightValue = Convert(roman.Substring(maxIndex + 1));

      return RomanToIntMap[roman.Substring(maxIndex,1)] - leftValue + rightValue;
   }

   private static int CountMaxIndex(string roman)
   {
      var max = RomanToIntMap.First().Value;
      var maxIndex = 0;
      for (var i = 0; i < roman.Length; i++)
      {
         var value = RomanToIntMap[roman.Substring(i, 1)];
         if (value <= max) continue;
         
         max = value;
         maxIndex = i;
      }

      return maxIndex;
   }

}