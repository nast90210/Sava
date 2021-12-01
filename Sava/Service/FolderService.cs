using System;
using System.Collections.Generic;
using System.IO;

namespace Sava
{
    public class FolderService
    {
        //TODO: возможно стоит добавить проверку наличие модели VOSK и ffmpeg и уведомление на Index
        private List<string> folders = new List<string>{
            "meta",
            "reports",
            "audio",
            Path.Combine("audio","temp"),
        };

        public FolderService()
        {
            CheckSystemFolderExist();
            ClearTemp();
        }

        private void CheckSystemFolderExist()
        {
            foreach(var folder in folders)
            {
                if(!Directory.Exists(folder))
                    Directory.CreateDirectory(Path.Combine("wwwroot",folder));
            }
        }

        public void ClearTemp()
        {
            foreach (var folder in folders)
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