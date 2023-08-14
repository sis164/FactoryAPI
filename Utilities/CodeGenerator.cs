using FactoryAPI.Controllers;
using FactoryAPI.Models;
using NuGet.Packaging;
using System.Text;

namespace FactoryAPI.Utilities
{
    public class CodeGenerator
    {
        private readonly ApplicationContext _context;
        private readonly List<char> chars;
        public CodeGenerator(ApplicationContext context)
        {
            _context = context;
            chars = new List<char>();
            for (int i = 48; i <= 122; i++)
            {
                char c = (char)i;
                if (char.IsLetterOrDigit(c))
                    chars.Add(c);
            }
        }
        private bool IsUnique(string Code)
        {
            if (_context.Card.FirstOrDefault(card => card.Code == Code) is null)
            {
                return true;
            }
            return false;
        }

        public string GenerateCode()
        {
            string result;
            do
            {
                StringBuilder stringBuilder = new(5);
                Random random = new();

                for (int i = 0; i < 5; i++)
                {
                    int index = random.Next(chars.Count);
                    stringBuilder.Append(chars[index]);
                }

                result = stringBuilder.ToString();

            } while (!IsUnique(result));

            return result;
        }
    }
}
