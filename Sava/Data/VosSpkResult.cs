using System;
using System.Collections.Generic;

namespace Sava.Data
{
    [Serializable]
    public class VoskSpkResult
    {
        public List<VoskPartialResult> result { get; set; } = new ();
        
        public double[] spk { get; set; }
        
        public int spk_frames { get; set; }
        public string text { get; set; }
        public bool IsEmpty => result.Count == 0;
        public float End => IsEmpty ? 0 : result[^1].end;
        public float Start => IsEmpty ? 0 : result[0].start;   
    }
}