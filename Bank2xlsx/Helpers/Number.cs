namespace Bank2xlsx.Helpers
{
    internal class Number
    {
        public static bool IsNumeric(string value)
        {
            value = value.Replace(",", "");

            return decimal.TryParse(value, out _);
        }
    }
}
