using System.Linq;

namespace Sava.Data
{
    public class MetaFile
    {
        public string Name { get; set; }
        public string Path { get; set; }
        public MetaData MetaData { get; set; }

        public Meta FindSigmet(string fileName)
            => MetaData.Data.FirstOrDefault(meta => meta.ContactId == fileName);
    }
}