using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using admission_validation.Models;

namespace admission_validation.Services
{
    public class FileStorageService
    {
        private readonly string _basePath;

            public FileStorageService(IConfiguration config)
            {
                _basePath = config["Storage:BasePath"] ?? "Uploads";
            }

            public void SaveFiles(DocumentUploadRequest request, string status)
            {
                var employeeFolder = request.EmployeeType.ToString();

                var path = Path.Combine(
                    _basePath,
                    employeeFolder,
                    status,
                    Sanitize(request.CandidateName)
                );

                Directory.CreateDirectory(path);

                var properties = typeof(DocumentUploadRequest)
                    .GetProperties()
                    .Where(p => p.PropertyType == typeof(IFormFile));

                foreach (var prop in properties)
                {
                    var file = prop.GetValue(request) as IFormFile;

                    if (file != null)
                    {
                        SaveFile(file, path, prop.Name);
                    }
                }
            }

            private void SaveFile(IFormFile file, string folder, string name)
            {
                var filePath = Path.Combine(folder, $"{name}{Path.GetExtension(file.FileName)}");

                using var stream = new FileStream(filePath, FileMode.Create);
                file.CopyTo(stream);
            }

            private string Sanitize(string name)
            {
                return name.Replace(" ", "_");
            }
    }
}