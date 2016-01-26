using System;
using System.Text;

namespace BestPlaylists.Common
{
    public class RandomGenerator
    {
        private const string AlphaNum = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

        private string[] words = new[]
        {
            "funny", "annoying", "monkeys", "cute", "and", "monkey", "compilation", "are", "so", "but", "sometimes", "they",
            "can", "be", "real", "jerks", "just", "look", "how", "what", "do", "to", "people", "cats", "dogs", "on",
            "hope", "you", "like", "our", "please", "share", "it", "watch", "also", "other", "videos", "at", "the",
            "zoo", "stealing", "things", "was", "aftermath", "of", "a", "big", "festival", "two", "were", "prowling",
            "together", "one", "saw", "cake", "mieued", "jumped", "up", "picket", "first", "cat", "said", "give", "me",
            "is", "i", "who", "keep", "away", "from", "picked", "fighting", "there", "no", "solution", "then", "passed",
            "by", "he", "thought", "foolish", "must", "let", "make", "use", "this", "chance", "came", "in", "loud",
            "voice", "don't", "fight", "among", "both", "handed", "over", "split", "into", "tow", "parts", "shook",
            "his", "head", "oho", "bigger", "smaller", "had", "bit", "now", "has", "become", "ate", "thus", "went",
            "eating", "part", "finally", "finished", "whole", "poor", "disappointed"
        };

        private static Random random = new Random();

        public int GetRandomNumber(int minLength = 0, int maxLength = int.MaxValue - 1)
        {
            return random.Next(minLength, maxLength + 1);
        }

        public string GetRandowSentance(int minLength = 0, int maxLength = 61)
        {
            var builder = new StringBuilder();
            var length = this.GetRandomNumber(minLength, maxLength + 1);

            for (int i = 0; i < length; i++)
            {
                builder.Append(words[this.GetRandomNumber(0, words.Length - 1)]);
                builder.Append(" ");

                if (builder.Length >= maxLength)
                {
                    break;
                }
            }

            var result = builder[0].ToString().ToUpper();
            result += builder.ToString().Substring(1, builder.Length - 1);

            return result.Trim();
        }

        public string GetRandomString(int minLength = 0, int maxLength = 61)
        {
            var length = random.Next(minLength, maxLength + 1);
            var result = new StringBuilder();

            for (int i = 0; i < length; i++)
            {
                result.Append(AlphaNum[random.Next(0, AlphaNum.Length - 1)]);
            }

            // Simulate spacing
            result.Replace('A', ' ').Replace('a', ' ');

            return result.ToString().Trim();
        }

        public DateTime GetRandomDate(DateTime? after = null, DateTime? before = null)
        {
            var minDate = after ?? new DateTime(2000, 1, 1, 0, 0, 0);
            var maxDate = before ?? new DateTime(2050, 12, 31, 23, 59, 59);

            var second = this.GetRandomNumber(minDate.Second, maxDate.Second);
            var minute = this.GetRandomNumber(minDate.Minute, maxDate.Minute);
            var hour = this.GetRandomNumber(minDate.Hour, maxDate.Hour);
            var day = this.GetRandomNumber(minDate.Day, maxDate.Day);
            var month = this.GetRandomNumber(minDate.Month, maxDate.Month);
            var year = this.GetRandomNumber(minDate.Year, maxDate.Year);

            if (day > 28)
            {
                day = 28;
            }

            return new DateTime(year, month, day, hour, minute, second);
        }
    }
}
