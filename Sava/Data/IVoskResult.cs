using System.Collections.Generic;

namespace Sava.Data
{
    public interface IVoskResult
    {
        public List<VoskPartialResult> result { get; set; }
        public string text { get; set; }
        public bool IsEmpty { get; }

        public float End { get; }

        public float Start { get;}
    }
}