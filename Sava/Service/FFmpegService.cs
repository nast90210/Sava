using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Xabe.FFmpeg;

namespace Sava.Service
{
    public class FFmpegService
    {
        public FFmpegService()
        {
            FFmpeg.SetExecutablesPath(Path.Combine("wwwroot","ffmpeg"));
        }
        
        public async Task<bool> UnsupportedCodecAsync(string audioFile)
        {
            var mediaInfo = await Info(audioFile);
            var audioStream = mediaInfo.AudioStreams.FirstOrDefault();
            return audioStream?.Codec != "pcm_s16le";
        }

        public async Task<string> ConvertAsync(string audioFile, string channel = null)
        {
            string channelPref = null;
            
            if (channel != null)
                channelPref = "channel" + channel + "_";

            var result = Path.Combine("audio", "temp", channelPref + Path.GetFileName(audioFile));
            
            if(File.Exists(Path.Combine("wwwroot", result)))
                File.Delete(Path.Combine("wwwroot", result));
            
            var mediaInfo = await Info(Path.Combine("wwwroot", audioFile));

            if(channel == null)
                await Convert(mediaInfo, Path.Combine("wwwroot", result), mediaInfo.AudioStreams.First().Channels);
            else
                await Convert(mediaInfo, Path.Combine("wwwroot", result), int.Parse(channel), true);
            
            return result;
        }

        public async Task<bool> IsStereo(string audioFile)
        {
            var mediaInfo = await Info(Path.Combine("wwwroot", audioFile));
            return mediaInfo.AudioStreams.FirstOrDefault()?.Channels > 1 ;
        }
        
        public static async Task<IMediaInfo> Info(string source)
            => await FFmpeg.GetMediaInfo(source);
        
        private static async Task Convert(IMediaInfo info, string outputPath, int channel, bool splitting = false)
        {
            if(splitting) 
                await Conversion(info,outputPath,1).AddParameter("-map_channel 0.0." + channel).Start();
            else
                await Conversion(info, outputPath, channel).Start();
        }
        
        private static IConversion Conversion(IMediaInfo info, string outputPath, int channel)
        {
            return FFmpeg.Conversions.New()
                .AddStream(info.AudioStreams.First()
                    .SetCodec(AudioCodec.pcm_s16le)
                    .SetChannels(channel)
                    .SetSampleRate(16000))
                .SetOutput(outputPath);
        }
    }
}