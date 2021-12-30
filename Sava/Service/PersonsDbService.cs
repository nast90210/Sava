using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sava.Data;
using Sava.Models;

namespace Sava.Service
{
    public class PersonsDbService
    {
        private readonly DataBaseContext _dataBaseContext;

        public PersonsDbService(DataBaseContext dataBase)
        {
            _dataBaseContext = dataBase;
        }

        public async Task<IEnumerable<Person>> FindAllAsync(string lastName)
        {
            return await Task.Run(() => FindAll(lastName));
        }
        
        
        public IEnumerable<Person> FindAll(string text)
        {
            var temp = _dataBaseContext.Persons.AsEnumerable();
            var result = temp
                .Where(phone =>
                    phone.sFull_Last_Name != null &&
                    phone.sFull_Last_Name.Contains(text, StringComparison.OrdinalIgnoreCase))
                .OrderBy(person => person.idObj)
                .Take(20)
                .ToList(); // Warning По какой-то причине любой другой ти коллекции лагает при выходе в AvayaRecognitionForm 
            temp = null;
            return result;
        }
    }
}