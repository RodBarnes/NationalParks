using System.Text;

namespace NationalParks.Models
{
    public class Hours
    {
        public string Monday { get; set; }
        public string Tuesday { get; set; }
        public string Wednesday { get; set; }
        public string Thursday { get; set; }
        public string Friday { get; set; }
        public string Saturday { get; set; }
        public string Sunday { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();

            // Key is the value; e.g., "Closed"
            // Value is abbreviated property name; e.g., Monday
            var dict = new Dictionary<string, string>();

            CheckForDailyHours(dict, "Mon", Monday);
            CheckForDailyHours(dict, "Tue", Tuesday);
            CheckForDailyHours(dict, "Wed", Wednesday);
            CheckForDailyHours(dict, "Thu", Thursday);
            CheckForDailyHours(dict, "Fri", Friday);
            CheckForDailyHours(dict, "Sat", Saturday);
            CheckForDailyHours(dict, "Sun", Sunday);

            foreach (var key in dict.Keys)
            {
                sb.Append($"{dict[key]}: {key}\n");
            }

            return sb.ToString();
        }

        private static void CheckForDailyHours(Dictionary<string, string> dict, string propertyName, string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                if (dict.ContainsKey(value))
                {
                    dict[value] += $",{propertyName}";
                }
                else
                {
                    dict.Add(value, propertyName);
                }
            }
        }
    }
}
