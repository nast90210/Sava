using System.Collections.Generic;
using Sava.Service;

namespace Sava.Data
{
    public class TempAudioFile
    {
        private string _result;
        private string _abonentName, _nomerName;
        private string _abonent, _nomer;

        public TempAudioFile()
        {
            SourceResult = new List<IVoskResult>();
            SourceResultChannel0 = new List<IVoskResult>();
            SourceResultChannel1 = new List<IVoskResult>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string SourceFile { get; set; }
        public string ConvertedFile { get; set; }

        public bool Converted
            => ConvertedFile != null;

        public Meta Meta { get; set; }

        public string MetaPath { get; set; }

        public string Result
        {
            get
            {
                if (_result == null)
                    return IsStereo
                        ? VoskService.GetCombinedResultNew(SourceResultChannel0, AbonentName, SourceResultChannel1, NomerName)
                        : VoskService.GetSplitResult(SourceResult);

                return _result;
            }
            set => _result = value;
        }

        public List<IVoskResult> SourceResult { get; set; }

        public string Abonent
        {
            get => _abonent;   
            set => _abonent = Phone.ValidatePhoneNumber(value);
        }
        
        public string IdAbonent { get; set; }

        public string AbonentName
        {
            get => _abonentName ?? Abonent;
            set => _abonentName = value;
        }

        public string IdAbonentName { get; set; }
        
        public string Nomer
        {
            get => _nomer;   
            set => _nomer = Phone.ValidatePhoneNumber(value);
        }
        public string IdNomer { get; set; }

        public string NomerName
        {
            get => _nomerName ?? Nomer;
            set => _nomerName = value;
        }

        public string IdNomerName { get; set; }
        
        public string DateTime { get; set; }
        public string Duration { get; set; }
        public string Contacts { get; set; }
        public bool IsCollapsed { get; set; }
        public RecognitionStatus CurrentStatus { get; set; }
        public bool IsStereo { get; set; }
        public string SplitedFileChannel0 { get; set; }
        public string SplitedFileChannel1 { get; set; }
        public List<IVoskResult> SourceResultChannel0 { get; set; }
        public List<IVoskResult> SourceResultChannel1 { get; set; }
    }
}