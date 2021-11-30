using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sava.Data
{
    [Serializable]
    public class VoskResult
    {
        public List<VoskPartialResult> result { get; set; }
        public string text { get; set; }

        public static string GetCombinedResult(List<VoskResult> sourceResultChannel0, string abonent,
            List<VoskResult> sourceResultChannel1, string nomer)
        {
            var builder = new StringBuilder();
            var abonentPartialResults = new Queue<VoskPartialResult>();
            var nomerPartialResults = new Queue<VoskPartialResult>();

            try
            {
                foreach (var voskPartialResult in sourceResultChannel0.Where(voskResult => voskResult.result != null)
                    .SelectMany(voskResult => voskResult.result)) abonentPartialResults.Enqueue(voskPartialResult);

                foreach (var voskPartialResult in sourceResultChannel1.Where(voskResult => voskResult.result != null)
                    .SelectMany(voskResult => voskResult.result)) nomerPartialResults.Enqueue(voskPartialResult);


                string currentSpeaker = null;
                VoskPartialResult abonentCurrent = null, nomerCurrent = null;

                if (abonentPartialResults.Count > 0)
                    abonentCurrent = abonentPartialResults.Dequeue();
                if (nomerPartialResults.Count > 0)
                    nomerCurrent = nomerPartialResults.Dequeue();

                //TODO: реализовать более сложную связку объектов основанную на логике
                do
                {
                    if (abonentCurrent == null)
                    {
                        if (nomerCurrent != null) builder.AddWord(ref currentSpeaker, nomer, nomerCurrent.word);
                        nomerCurrent = nomerPartialResults.Count > 0 ? nomerPartialResults.Dequeue() : null;

                        if (nomerCurrent == null)
                            break;
                        
                        continue;
                    }

                    if (nomerCurrent == null)
                    {
                        builder.AddWord(ref currentSpeaker, abonent, abonentCurrent.word);
                        abonentCurrent = abonentPartialResults.Count > 0 ? abonentPartialResults.Dequeue() : null;
                       
                        if (abonentCurrent == null)
                            break;
                        
                        continue;
                    }

                    if (abonentCurrent.start < nomerCurrent.start)
                    {
                        builder.AddWord(ref currentSpeaker, abonent, abonentCurrent.word);
                        abonentCurrent = abonentPartialResults.Count > 0 ? abonentPartialResults.Dequeue() : null;
                    }
                    else
                    {
                        builder.AddWord(ref currentSpeaker, nomer, nomerCurrent.word);
                        nomerCurrent = nomerPartialResults.Count > 0 ? nomerPartialResults.Dequeue() : null;
                    }
                    
                } while (true);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            return builder.ToString();
        }

        public static string GetSplitResult(IEnumerable<VoskResult> sourceResult)
        {
            return sourceResult.Where(voskResult => voskResult.text != " ").Aggregate<VoskResult, string>(null,
                (current, voskResult) => current + "- " + voskResult.text + Environment.NewLine);
        }
    }

    public static class StringBuilderExtension
    {
        public static void AddWord(this StringBuilder builder, ref string currentSpeaker, string speaker, string word)
        {
            if (currentSpeaker == null)
            {
                currentSpeaker = speaker;
                builder.Append(speaker + " : " + word + " ");
            }
            else if (currentSpeaker == speaker)
            {
                builder.Append(word + " ");
            }
            else
            {
                currentSpeaker = speaker;
                builder.Append(Environment.NewLine + speaker + " : " + word + " ");
            }
        }
    }
}