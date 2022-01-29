using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Sava
{
    public class FolderService
    {
        //TODO: возможно стоит добавить проверку наличие модели VOSK и ffmpeg и уведомление на Index
        private readonly List<string> _folders = new (){
            "meta",
            "reports",
            "pdf",
            "audio",
            Path.Combine("audio","temp"),
        };

        public FolderService()
        {
            CheckSystemFolderExist();
        }

        private void CheckSystemFolderExist()
        {
            foreach (var folder in _folders.Where(folder => !Directory.Exists(folder)))
            {
                Directory.CreateDirectory(Path.Combine("wwwroot",folder));
            }
        }

        public void ClearTemp()
        {
            foreach (var folder in _folders.Where(folder => Directory.Exists(Path.Combine("wwwroot", folder))))
            {
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