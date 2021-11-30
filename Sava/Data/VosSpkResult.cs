using System;
using System.Collections.Generic;

namespace Sava.Data
{
    [Serializable]
    public class VoskSpkResult
    {
        public List<VoskPartialResult> result { get; set; }
        public float[] spk { get; set; }
        public int spk_frames { get; set; }
        public string text { get; set; }
    }
}