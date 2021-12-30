using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Sava.Data;
using Vosk;

namespace Sava.Service
{
    public class VoskService
    {
        private readonly Model _model;

        public VoskService()
        {
            try
            {
                Vosk.Vosk.GpuInit();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            _model = new Model("wwwroot/model");
        }

        //TODO: CancellationToken to method
        public async Task<List<VoskResult>> RecognizeAsync(string file)
        {
            var results = new List<VoskResult>();

            await Task.Run(() =>
            {
                var rec = new VoskRecognizer(_model, 16000.0f);
                rec.SetMaxAlternatives(0);
                rec.SetWords(true);
                using Stream source = File.OpenRead(file);
                var buffer = new byte[4096];
                int bytesRead;
                while ((bytesRead = source.Read(buffer, 0, buffer.Length)) > 0)
                    if (rec.AcceptWaveform(buffer, bytesRead))
                        results.Add(JsonConvert.DeserializeObject<VoskResult>(rec.Result()));

                results.Add(JsonConvert.DeserializeObject<VoskResult>(rec.FinalResult()));
            });

            return results;
        }

        public static string GetCombinedResultNew(List<VoskResult> sourceResultChannel0, string abonent,
            List<VoskResult> sourceResultChannel1, string nomer)
        {
            var builder = new StringBuilder();
            var abonentResults = new Queue<VoskResult>();
            var nomerResults = new Queue<VoskResult>();

            try
            {
                foreach (var voskResult in sourceResultChannel0.AsQueryable())
                    abonentResults.Enqueue(voskResult);

                foreach (var voskResult in sourceResultChannel1)
                    nomerResults.Enqueue(voskResult);

                string currentSpeaker = null;
                VoskResult abonentCurrent = null;
                VoskResult nomerCurrent = null;

                if (abonentResults.Count > 0)
                    abonentCurrent = abonentResults.Dequeue();
                if (nomerResults.Count > 0)
                    nomerCurrent = nomerResults.Dequeue();

                do
                {
                    if (abonentCurrent == null)
                    {
                        if (nomerCurrent != null) builder.AddWord(ref currentSpeaker, nomer, nomerCurrent.text);
                        nomerCurrent = nomerResults.Count > 0 ? nomerResults.Dequeue() : null;

                        if (nomerCurrent == null)
                            break;

                        continue;
                    }

                    if (nomerCurrent == null)
                    {
                        builder.AddWord(ref currentSpeaker, abonent, abonentCurrent.text);
                        abonentCurrent = abonentResults.Count > 0 ? abonentResults.Dequeue() : null;

                        if (abonentCurrent == null)
                            break;

                        continue;
                    }

                    if (abonentCurrent.Start < nomerCurrent.Start)
                    {
                        builder.AddWord(ref currentSpeaker, abonent, abonentCurrent.text);
                        abonentCurrent = abonentResults.Count > 0 ? abonentResults.Dequeue() : null;
                    }
                    else
                    {
                        builder.AddWord(ref currentSpeaker, nomer, nomerCurrent.text);
                        nomerCurrent = nomerResults.Count > 0 ? nomerResults.Dequeue() : null;
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