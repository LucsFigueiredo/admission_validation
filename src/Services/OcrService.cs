using Tesseract;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;

namespace admission_validation.Services
{
    public class OcrService
    {
        public string ExtractText(string filePath)
        {
            var processedPath = PreprocessImage(filePath);

            try
            {
                using var engine = new TesseractEngine(@"./tessdata", "por", EngineMode.Default);

                using var img = Pix.LoadFromFile(processedPath);

                using var page = engine.Process(img);

                return page.GetText();
            }
            finally
            {
                File.Delete(processedPath);
            }
        }
        
        public string PreprocessImage(string filePath)
        {
            using var image = Image.Load(filePath);

            image.Mutate(x =>
                x.Grayscale()              // remove cores
                .Contrast(1.8f)           // aumenta contraste
                .Resize(image.Width * 2, image.Height * 2)       // dobra resolução
                .GaussianSharpen()          // melhora nitidez
            );

            var newPath = filePath.Replace(".", "_processed.");

            image.Save(newPath);

            return newPath;
        }
    }
}
