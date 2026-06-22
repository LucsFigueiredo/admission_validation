using admission_validation.Models;

namespace admission_validation.Services
{
    public class FileStorageService
    {
        private readonly string _basePath = "Uploads/Temporary";

        public void SaveFiles(DocumentUploadRequest request)
        {
            var candidateFolder = Path.Combine(_basePath, request.CandidateName);

            Directory.CreateDirectory(candidateFolder);

            SaveFile(request.RGFront, candidateFolder, "RG (frente)");
            SaveFile(request.RGBack, candidateFolder, "RG (verso)");
            SaveFile(request.CPF, candidateFolder, "CPF");
        }

        private void SaveFile(IFormFile file, string folder, string name)
        {
            if (file == null) return;

            var path = Path.Combine(folder, $"{name}{Path.GetExtension(file.FileName)}");

            using var stream = new FileStream(path, FileMode.Create);
            file.CopyTo(stream);
        }
    }
}