using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Sava.Data;
using Sava.Models;

namespace Sava.Service
{
    public class AudioFilesDbService 
    {
        private readonly DataBaseContext _dataBaseContext;

        public AudioFilesDbService(DataBaseContext dataBase)
        {
            _dataBaseContext = dataBase;
        }
        
        public async Task<List<AudioFile>> GetAudioFilesAsync()
        {
            return await _dataBaseContext.AudioFiles.ToListAsync();
        }

        public async Task AddAudioFileAsync(AudioFile audioFile)
        {
            try
            {
                _dataBaseContext.AudioFiles.Add(audioFile);
                await _dataBaseContext.SaveChangesAsync();
            }
            catch (Exception exception)
            {
                throw new Exception($"Cannot Add TempAudioFile {audioFile.Name}", exception);
            }
        }
        
        public async Task<AudioFile> UpdateAudioFileAsync(AudioFile audioFile)
        {
            try
            {
                var audioFiletExist = _dataBaseContext.AudioFiles.FirstOrDefault(_audioFile => _audioFile.Id == audioFile.Id);
                if (audioFiletExist != null)
                {
                    _dataBaseContext.Update(audioFile);
                    await _dataBaseContext.SaveChangesAsync();
                }
            }
            catch (Exception exception)
            {
                throw new Exception($"Cannot Update TempAudioFile {audioFile.Name}", exception);
            }
            return audioFile;
        }
        
        public async Task DeleteProductAsync(AudioFile audioFile)
        {
            try
            {
                _dataBaseContext.AudioFiles.Remove(audioFile);
                await _dataBaseContext.SaveChangesAsync();
            }
            catch (Exception exception)
            {
                throw new Exception($"Cannot Delete TempAudioFile {audioFile.Name}", exception);
            }
        }
        
    }
}