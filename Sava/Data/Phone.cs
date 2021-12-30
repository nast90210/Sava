using System;
using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;

namespace Sava.Data
{
    [Keyless]
    public class Phone
    {
        public string idPhone { get; set; }
        public string sPhoneNum { get; set; }
        public string idObj { get; set; }
        public string sFull_Last_Name { get; set; }
        public string sShort_First_Name { get; set; }
        public string sOtherLang_Mid_Name { get; set; }

        public override string ToString()
            => sFull_Last_Name.Contains(" ")
                ? sFull_Last_Name
                : $"{sFull_Last_Name} {sShort_First_Name} {sOtherLang_Mid_Name}";

        public static string ValidatePhoneNumber(string sourcePhoneNumber)
        {
            if (!new Regex(@"^\d+$").IsMatch(sourcePhoneNumber)) 
                return sourcePhoneNumber;
            
            switch (sourcePhoneNumber.Length)
            {
                case 4:
                    sourcePhoneNumber = "00#" + sourcePhoneNumber;
                    break;
                case 7:
                    sourcePhoneNumber = "8495" + sourcePhoneNumber;
                    break;
                case 10:
                    sourcePhoneNumber = "8" + sourcePhoneNumber;
                    break;
                case 11:
                    if (sourcePhoneNumber.StartsWith("7", StringComparison.CurrentCulture)) 
                        sourcePhoneNumber = "8" + sourcePhoneNumber.Remove(0, 1);
                    else if (!sourcePhoneNumber.StartsWith("8", StringComparison.CurrentCulture))
                        sourcePhoneNumber = "810" + sourcePhoneNumber;
                    break;
                case 12:
                    if (!sourcePhoneNumber.StartsWith("8", StringComparison.CurrentCulture))
                        sourcePhoneNumber  = "810" + sourcePhoneNumber;
                    break;
                case 13:
                    if (sourcePhoneNumber.StartsWith("86", StringComparison.CurrentCulture))
                        sourcePhoneNumber = "810" + sourcePhoneNumber;
                    break;
            }
            
            return sourcePhoneNumber;
        }

    }
}