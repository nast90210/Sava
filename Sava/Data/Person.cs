using Microsoft.EntityFrameworkCore;

namespace Sava.Data
{
    [Keyless]
    public class Person
    {
        public int idObj { get; set; }
        public string sFull_Last_Name { get; set; }
        public string sShort_First_Name { get; set; }
        public string sOtherLang_Mid_Name { get; set; }

        public string Full => this.ToString();
        
        public override string ToString()
            => sFull_Last_Name.Contains(" ")
                ? sFull_Last_Name
                : $"{sFull_Last_Name} {sShort_First_Name} {sOtherLang_Mid_Name}";
    }
}