using System;
using System.Collections.Generic;
using Sava.Data;
using Sava.Models;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Sava.Service
{
    public class PhonesDbService
    {
        private readonly DataBaseContext _dataBaseContext;

        public PhonesDbService(DataBaseContext dataBase)
        {
            _dataBaseContext = dataBase;
        }

        public Phone Find(string phone)
            => _dataBaseContext.Phones.FirstOrDefault(p => p.sPhoneNum == Phone.ValidatePhoneNumber(phone)) ??
               _dataBaseContext.Phones.OrderBy(p => p.idObj)
                   .LastOrDefault(p => p.sPhoneNum.Length == phone.Length && p.sPhoneNum.EndsWith(phone));

        public async Task<List<Phone>> FindAll(string lastName)
            // => _dataBaseContext.Phones.ToList()
            //     .Where(phone => phone.sFull_Last_Name != null &&
            //                     phone.sFull_Last_Name.Contains(lastName, StringComparison.OrdinalIgnoreCase))
            //     .OrderBy(phone => phone.idObj)
            //     .Take(10).ToList();
        {
            var s = await _dataBaseContext.Phones.ToListAsync();
            var t = s.FindAll(phone =>
                phone.sFull_Last_Name != null &&
                phone.sFull_Last_Name.Contains(lastName, StringComparison.OrdinalIgnoreCase));
        
            var d = t.Take(10);
            return d.ToList();
        }
    }
}