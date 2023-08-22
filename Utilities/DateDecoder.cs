using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Text;

namespace FactoryAPI.Utilities
{
    static public class DateDecoder
    {
        public static string EncodeExpirationDate(DateTime expirationDate)
        {
            // Extract the components from the expiration date
            int year = expirationDate.Year % 100; // Considering the last two digits of the year
            int month = expirationDate.Month;
            int day = expirationDate.Day;
            int hour = expirationDate.Hour;
            int minute = expirationDate.Minute;

            // Encode the date using the same logic
            uint date = (uint)((year << 20) | (month << 16) | (day << 11) | (hour << 6) | minute);

            StringBuilder sBuilder = new StringBuilder();

            for (int n = 0; n < 6; n++)
            {
                byte b6Bit = (byte)((date >> (n * 6)) & 0x3F);
                sBuilder.Append(AsciiToByte8bit(b6Bit));
            }

            return sBuilder.ToString();
        }

        // Helper method to convert a 6-bit value to an ASCII character
        public static char AsciiToByte8bit(byte value)
        {
            // ASCII characters 0x20 to 0x5F represent printable characters
            return (char)(value + 0x20);
        }

        static public DateTime DecodeExpirationDate(string encodedDate)
        {
            if (encodedDate.Length != 6)
            {
                throw new ArgumentException("Закодированная строка имеет неверную длину");
            }

            int year = 2000 + int.Parse(encodedDate.Substring(0, 2));
            int month = int.Parse(encodedDate.Substring(2, 2));
            int day = int.Parse(encodedDate.Substring(4, 2));
            int hour = int.Parse(encodedDate.Substring(6, 2));
            int minute = int.Parse(encodedDate.Substring(8, 2));

            return new DateTime(year, month, day, hour, minute, 0);
        }

        // Helper method to convert an ASCII character to 6-bit value
        static byte Byte8bitToAscii(char ch)
        {
            return (byte)(ch - 0x20);
        }
    }
}
