using System;
using Microsoft.EntityFrameworkCore;

namespace Sava.Data
{
    [Serializable]
    public class VoskPartialResult
    {
        public float conf { get; set; }
        public float end { get; set; }
        public float start { get; set; }
        public string word { get; set; }
    }
}