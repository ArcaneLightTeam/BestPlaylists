using System;
using System.Text;

namespace BestPlaylists.Common
{
    public class RandomGenerator
    {
        private const string AlphaNum = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

        private static Random random = new Random();

        public int GetRandomNumber(int minLength = 0, int maxLength = int.MaxValue - 1)
        {
            return random.Next(minLength, maxLength + 1);
        }

        public string GetRandomString(int minLength = 0, int maxLength = 61)
        {
            var length = random.Next(minLength, maxLength + 1);
            var result = new StringBuilder();

            for (int i = 0; i < length; i++)
            {
                result.Append(AlphaNum[random.Next(0, AlphaNum.Length - 1)]);
            }

            return result.ToString();
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
