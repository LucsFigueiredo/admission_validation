using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using admission_validation.Models;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace admission_validation.Services
{
    public class FileStorageService
    {
        private readonly string _basePath;

        public FileStorageService(IConfiguration config)
        {
            _basePath = config["Storage:BasePath"] ?? "Uploads";
        }

        public string SaveFiles(DocumentUploadRequest request, string status)
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

            return path;
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

        public string SaveTemp(IFormFile file)
        {
            var tempPath = Path.Combine(_basePath, "Temp");

            Directory.CreateDirectory(tempPath);

            var filePath = Path.Combine(
                tempPath,
                $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}"
            );

            using var stream = new FileStream(filePath, FileMode.Create);
            file.CopyTo(stream);

            return filePath;
        }

        public void GenerateReport(string folder, string candidateName, List<DocumentValidationDetail> details)
        {
            var path = Path.Combine(folder, "report.txt");

            var sb = new System.Text.StringBuilder();

            sb.AppendLine($"Candidato: {candidateName}");
            sb.AppendLine("");

            foreach (var doc in details)
            {
                sb.AppendLine($"{doc.DocumentName}:");
                sb.AppendLine($"- Score: {doc.Score}");
                sb.AppendLine($"- Status: {doc.Status}");
                sb.AppendLine($"- Observação: {doc.Message}");
                sb.AppendLine("");
            }

            File.WriteAllText(path, sb.ToString());
        }
    }
}