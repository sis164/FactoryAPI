using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Text;

namespace FactoryAPI.Utilities
{
    static public class DateDecoder
    {
        public static string EncodeExpirationDate(DateTime expirationDate)
        {
            int year = expirationDate.Year;
            int month = expirationDate.Month;
            int day = expirationDate.Day;
            int hour = expirationDate.Hour;
            int minute = expirationDate.Minute;

            uint date = (uint)((year << 20) | (month << 16) | (day << 11) | (hour << 6) | minute);


            StringBuilder stringBuilder = new StringBuilder();
            int i = 25;
            for (int n = 0; n < 6; n++)
            {
                byte b6Bit = (byte)((date >> i) & 0x3F);
                stringBuilder.Append(Byte8BitToASCII(b6Bit));

                i -= 6;
                if (i < 0)
                {
                    i = 0;
                }
            }

            return stringBuilder.ToString();
        }

        static public DateTime DecodeExpirationDate(string encodedDate)
        {
            uint timeBitArray = 0;
            int i = 25;

            for (int n = 0; n < 6; n++)
            {
                byte b8Bit = FromASCIIToByte((byte)encodedDate[n]);
                timeBitArray |= (uint)b8Bit << i;

                i -= 6;
                if (i < 0)
                {
                    i = 0;
                }
            }

            int year = (int)((timeBitArray >> 20) & 0x0FFF);
            int month = (int)((timeBitArray >> 16) & 0x0F);
            int day = (int)((timeBitArray >> 11) & 0x1F);
            int hour = (int)((timeBitArray >> 6) & 0x1F);
            int minute = (int)(timeBitArray & 0x3F);

            return new DateTime(year, month, day, hour, minute, 0, DateTimeKind.Utc);
        }
        private static byte FromASCIIToByte(byte b)
        {
            if (b > 63)
            {
                b -= 64;
            }
            return b;
        }
        private static char Byte8BitToASCII(byte b)
        {
            if (b < 32)
            {
                b += 64;
            }
            return (char)b;
        }
    }
}
