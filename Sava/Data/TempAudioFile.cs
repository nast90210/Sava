using System.Collections.Generic;

namespace Sava.Data
{
    public class TempAudioFile
    {
        private string _result;
        private Meta _meta;
        private string abonentName, nomerName;

        public TempAudioFile()
        {
            SourceResult = new List<VoskResult>();
            SourceResultChannel0 = new List<VoskResult>();
            SourceResultChannel1 = new List<VoskResult>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string SourceFile { get; set; }
        public string ConvertedFile { get; set; }

        public bool Converted
            => ConvertedFile != null;

        public Meta Meta
        {
            get => _meta;

            set
            {
                _meta = value;

                if (value == null) return;

                DateTime = _meta.Description[0];
                Duration = _meta.Description[1];
                Contacts = _meta.Description[3];

                if (_meta.Contact.Parties.Count > 1)
                {
                    Abonent = _meta.Contact.Parties[^2].Name;
                    Nomer = _meta.Contact.Parties[^1].Name;
                }
            }
        }

        public string MetaPath { get; set; }

        public string Result
        {
            get
            {
                if (_result == null)
                    return IsStereo
                        ? VoskResult.GetCombinedResult(SourceResultChannel0, AbonentName, SourceResultChannel1, NomerName)
                        : VoskResult.GetSplitResult(SourceResult);

                return _result;
            }
            set => _result = value;
        }

        public List<VoskResult> SourceResult { get; set; }

        public string Abonent
        {
            get;
            set;
            // Здесь получаем Имя владельца номера из БДСервиса
        }

        public string AbonentName
        {
            get => abonentName ?? Abonent;
            set => abonentName = value;
        }

        public string Nomer
        {
            get;
            set;
            // Здесь получаем Имя владельца номера из БДСервиса
        }

        public string NomerName
        {
            get => nomerName ?? Nomer;
            set => nomerName = value;
        }

        public string DateTime { get; set; }
        public string Duration { get; set; }
        public string Contacts { get; set; }
        public bool IsCollapsed { get; set; }
        public RecognitionStatus CurrentStatus { get; set; }
        public bool IsStereo { get; set; }
        public string SplitedFileChannel0 { get; set; }
        public string SplitedFileChannel1 { get; set; }
        public List<VoskResult> SourceResultChannel0 { get; set; }
        public List<VoskResult> SourceResultChannel1 { get; set; }
    }
}