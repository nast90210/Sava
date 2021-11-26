using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Sava.Models;

namespace Sava.Data
{
    public class dbService 
    {
        private readonly DataBaseContext _dataBaseContext;

        public dbService(DataBaseContext dataBase)
        {
            _dataBaseContext = dataBase;
        }
        
        public async Task<List<TempAudioFile>> GetAudioFilesAsync()
        {
            return await _dataBaseContext.TempAudioFiles.ToListAsync();
        }

        public async Task AddAudioFileAsync(TempAudioFile tempAudioFile)
        {
            try
            {
                _dataBaseContext.TempAudioFiles.Add(tempAudioFile);
                await _dataBaseContext.SaveChangesAsync();
            }
            catch (Exception exception)
            {
                throw new Exception($"Cannot Add TempAudioFile {tempAudioFile.Name}", exception);
            }
        }
        
        public async Task<TempAudioFile> UpdateAudioFileAsync(TempAudioFile tempAudioFile)
        {
            try
            {
                var audioFiletExist = _dataBaseContext.TempAudioFiles.FirstOrDefault(_audioFile => _audioFile.Id == tempAudioFile.Id);
                if (audioFiletExist != null)
                {
                    _dataBaseContext.Update(tempAudioFile);
                    await _dataBaseContext.SaveChangesAsync();
                }
            }
            catch (Exception exception)
            {
                throw new Exception($"Cannot Update TempAudioFile {tempAudioFile.Name}", exception);
            }
            return tempAudioFile;
        }
        
        public async Task DeleteProductAsync(TempAudioFile tempAudioFile)
        {
            try
            {
                _dataBaseContext.TempAudioFiles.Remove(tempAudioFile);
                await _dataBaseContext.SaveChangesAsync();
            }
            catch (Exception exception)
            {
                throw new Exception($"Cannot Delete TempAudioFile {tempAudioFile.Name}", exception);
            }
        }
        
    }
}