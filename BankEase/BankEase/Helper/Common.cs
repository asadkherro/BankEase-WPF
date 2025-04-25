using System.Text;

namespace BankEase.Helper
{
    public static class Common
    {
        public static string GenerateAccountNumber()
        {
            StringBuilder accountNumber = new StringBuilder();
            Random random = new Random();
            for (int i = 0; i < 4; i++)
            {
                char randomLetter = (char)random.Next('A', 'Z' + 1);
                accountNumber.Append(randomLetter);
            }
            for (int i = 0; i < 4; i++)
            {
                int randomNumber = random.Next(0, 10);
                accountNumber.Append(randomNumber);
            }
            return accountNumber.ToString();
        }
    }
}
