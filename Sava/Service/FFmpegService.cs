using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Sava.Data;
using Xabe.FFmpeg;

namespace Sava.Service
{
    public class FFmpegService
    {
        public FFmpegService()
        {
            FFmpeg.SetExecutablesPath(Path.Combine("wwwroot","ffmpeg"));
        }
        
        private async Task<IMediaInfo> Info(string source)
            => await FFmpeg.GetMediaInfo(source);

        private async Task Convert(IMediaInfo info, string outputPath)
            => await Conversion(info,outputPath).Start();
        
        private async Task Convert(IMediaInfo info, string outputPath, int channel) 
            => await Conversion(info,outputPath,1).AddParameter("-map_channel 0.0." + channel).Start();
        
        private static IConversion Conversion(IMediaInfo info, string outputPath, int channel = 1)
        {
            return FFmpeg.Conversions.New()
                .AddStream(info.AudioStreams.First()
                    .SetCodec(AudioCodec.pcm_s16le)
                    .SetChannels(channel)
                    .SetSampleRate(16000))
                .SetOutput(outputPath);
        }

        public async Task<bool> UnsupportedCodecAsync(string audioFile)
        {
            var mediaInfo = await Info(audioFile);
            var audioStream = mediaInfo.AudioStreams.FirstOrDefault();
            return audioStream?.Codec != "pcm_s16le";
        }

        public async Task<string> ConvertAsync(TempAudioFile sourceAudioFile)
        {
            var result = Path.Combine("audio", "temp", sourceAudioFile.Name);
            
            if(File.Exists(Path.Combine("wwwroot",result)))
                File.Delete(Path.Combine("wwwroot",result));
            
            var mediaInfo = await Info(Path.Combine("wwwroot/", sourceAudioFile.SourceFile));
            await Convert(mediaInfo, Path.Combine("wwwroot", result));
            
            return result;
        }
    }
}