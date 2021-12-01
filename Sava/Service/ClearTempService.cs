using System.IO;

namespace Sava.Service
{
    public class ClearTempService
    {
        private readonly string[] _folders = { "audio", "audio/temp", "meta", "reports"};

        public ClearTempService()
        {
            Clear();
        }

        private void Clear()
        {
            foreach (var folder in _folders)
            {
                if (Directory.Exists(Path.Combine("wwwroot", folder)))
                    DeleteFiles(Path.Combine("wwwroot", folder));
            }            
        }
        
        private static void DeleteFiles(string folder)
        {
            if (Directory.GetFiles(folder).Length <= 0) 
                return;
            
            foreach (var file in Directory.GetFiles(folder))
            {
                File.Delete(file);
            }
        }
    }
}