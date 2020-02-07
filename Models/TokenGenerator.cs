using System.Security.Cryptography;
using System.Text;

namespace MeetingScheduler.Models
{
  public class TokenGenerator
  {
    public static string Generate(int size, bool easy)
    {
      var charSet = "abcdefghijklmnopqrstuvwxyz-_.ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
      if (easy)
      {
        charSet = "ABCDEFGHJKLMNPQRSTUVWXYZ23456789";
      }
      var chars = charSet.ToCharArray();
      var data = new byte[1];
      var crypto = new RNGCryptoServiceProvider();
      crypto.GetNonZeroBytes(data);
      data = new byte[size];
      crypto.GetNonZeroBytes(data);
      var result = new StringBuilder(size);
      foreach (var b in data)
      {
        result.Append(chars[b % (chars.Length)]);
      }
      return result.ToString();
    }
  }
}