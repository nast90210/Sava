using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Sava.Data
{
    [Serializable]
    public class VoskResult : IVoskResult
    {
        public List<VoskPartialResult> result { get; set; } = new ();
        public string text { get; set; }
        public bool IsEmpty => result.Count == 0;

        public float End => IsEmpty ? 0 : result[^1].end;

        public float Start => IsEmpty ? 0 : result[0].start;   
        
    }
}