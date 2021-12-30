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
    }
}