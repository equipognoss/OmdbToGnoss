using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmdbToGnoss.Services
{
    public static class StringExtensions
    {
        public static Encoding EncodingANSI = Encoding.GetEncoding("iso8859-1");

        public static string ToUTF8FromANSI(this string ansiString)
        {
            return EncodingANSI.GetString(Encoding.UTF8.GetBytes(ansiString));
        }

        public static string ToANSIFromUTF8(this string utf8String)
        {
            return Encoding.UTF8.GetString(EncodingANSI.GetBytes(utf8String));
        }
    }
}
