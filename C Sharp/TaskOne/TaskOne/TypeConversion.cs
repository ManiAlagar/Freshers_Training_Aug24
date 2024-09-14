using System.Text;

namespace TaskOne
{
    public class TypeConversion
    {
        public double IntegerToDouble(int number)
        {
            return Convert.ToDouble(number);

        }
        public string LongToString(long number)
        {
            return Convert.ToString(number);
        }

        public string StringToBase64(string data)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(data);
            return Convert.ToBase64String(bytes);
        }

        public int DoubleToInteger(double number)
        {
            return Convert.ToInt32(number);
        }

        public int LongToInteger(long number)
        {
            return Convert.ToInt32(number);
        }
    }
}
