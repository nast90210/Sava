using System.Collections.Generic;

namespace Sava.Data
{
    public class AudioFile
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Abonent { get; set; }
        public string AbonentName { get; set; }
        public string Nomer { get; set; }
        public string NomerName { get; set; }
        public byte[] SourceFile { get; set; }
        public string Result { get; set; }
    }
}