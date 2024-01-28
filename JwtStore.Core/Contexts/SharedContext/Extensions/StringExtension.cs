using System.Text;

namespace JwtStore.Core.Contexts.SharedContext.Extensions;

public static class StringExtension
{
     public static string ToBase64(this string value)
     {
          var plainTextBytes = Encoding.UTF8.GetBytes(value);
          return Convert.ToBase64String(plainTextBytes);
     }
}