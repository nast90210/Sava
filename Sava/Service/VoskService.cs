using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using Sava.Data;
using Vosk;
using Newtonsoft.Json;

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
    }
}