using System;
using System.Diagnostics;
using System.Threading.Tasks;
using PCLStorage;
using FileSystem = PCLStorage.FileSystem;

namespace GPSTracker
{
    public class FileStorageService
    {
        private static readonly Lazy<FileStorageService> lazy = new Lazy<FileStorageService>(() => new FileStorageService());
        public static FileStorageService Instance { get { return lazy.Value; } }

        public async Task<bool> WriteTextAsync(string folderName, string fileName, string content)
        {
            try
            {
                IFolder folder = await FileSystem.Current.GetFolderFromPathAsync(folderName);
                IFile file = await CreateFile(fileName, folder);
                await file.WriteAllTextAsync(content);
                return true;
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                return false;
            }           
        }
        private async Task<IFile> CreateFile(string fileName, IFolder folder)
        {
            try
            {
                return await folder.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
                return null;
            }
        }
    }
}
