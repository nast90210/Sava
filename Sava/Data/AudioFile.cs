using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sava.Data
{
    public class AudioFile
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string DateTime { get; set; }
        public string Duration { get; set; }
        public string Name { get; set; }
        public string Abonent { get; set; }
        public string IdAbonent { get; set; }
        public string AbonentName { get; set; }
        public string IdAbonentName { get; set; }
        public string Nomer { get; set; }
        public string IdNomer { get; set; }
        public string NomerName { get; set; }
        public string IdNomerName { get; set; }
        public byte[] SourceFile { get; set; }
        public string Result { get; set; }
    }
}