using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
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

        public IEnumerable<Person> Find(string text)
        {
            var parts = text.Split(" ");

            return parts.Length switch
            {
                1 => Surname(parts[0]),
                2 => SurnameAndName(parts[0], parts[1]),
                3 => SurnameAndNameAndMiddlename(parts[0], parts[1], parts[2]),
                _ => FindAll(text)
            };
        }

        private IEnumerable<Person> Surname(string surname) 
            => _dataBaseContext.Persons.AsEnumerable()                
                .Where(phone =>
                    phone.sFull_Last_Name != null &&
                    phone.sFull_Last_Name.StartsWith(surname, StringComparison.OrdinalIgnoreCase))
                .OrderBy(person => person.idObj)
                .Take(20)
                .ToList();
        
        private IEnumerable<Person> SurnameAndName(string surname,string name) 
            => _dataBaseContext.Persons.AsEnumerable()                
                .Where(phone =>
                    phone.sFull_Last_Name != null &&
                    phone.sFull_Last_Name.StartsWith(surname, StringComparison.OrdinalIgnoreCase) &&
                    phone.sShort_First_Name != null &&
                    phone.sShort_First_Name.StartsWith(name, StringComparison.OrdinalIgnoreCase))
                .OrderBy(person => person.idObj)
                .Take(20)
                .ToList();

        private IEnumerable<Person> SurnameAndNameAndMiddlename(string surname,string name, string middlename) 
            => _dataBaseContext.Persons.AsEnumerable()                
                .Where(phone =>
                    phone.sFull_Last_Name != null &&
                    phone.sFull_Last_Name.StartsWith(surname, StringComparison.OrdinalIgnoreCase) &&
                    phone.sShort_First_Name != null &&
                    phone.sShort_First_Name.StartsWith(name, StringComparison.OrdinalIgnoreCase) &&
                    phone.sOtherLang_Mid_Name != null &&
                    phone.sOtherLang_Mid_Name.StartsWith(middlename, StringComparison.OrdinalIgnoreCase))
                .OrderBy(person => person.idObj)
                .Take(20)
                .ToList();
        
        private IEnumerable<Person> FindAll(string text)
        {
            var temp = _dataBaseContext.Persons.AsEnumerable();
            var result = temp
                .Where(phone =>
                    phone.sFull_Last_Name != null &&
                    phone.sFull_Last_Name.Contains(text, StringComparison.OrdinalIgnoreCase))
                .OrderBy(person => person.idObj)
                .Take(20)
                .ToList(); // Warning По какой-то причине любой другой ти коллекции лагает при выходе в AvayaRecognitionForm 
            return result;
        }
    }
}