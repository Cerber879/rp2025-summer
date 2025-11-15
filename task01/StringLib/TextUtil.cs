using System.Text.RegularExpressions;

namespace StringLib;

public static class TextUtil
{
    public static List<string> SplitIntoWords(string text)
    {
        if (string.IsNullOrEmpty(text))
        {
            return [];
        }

        const string pattern = @"\p{L}+(?:[\-\']\p{L}+)*";
        Regex regex = new(pattern, RegexOptions.Compiled);

        return regex.Matches(text)
            .Select(match => match.Value)
            .ToList();
    }

    public static int CountVowels(string text)
    {
        if (string.IsNullOrEmpty(text))
        {
            return 0;
        }

        const string vowels = "aeiouyаеёиоуыэюя";

        return text
            .ToLower()
            .Count(c => vowels.Contains(c));
    }
}