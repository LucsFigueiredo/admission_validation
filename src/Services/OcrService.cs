using Tesseract;

namespace admission_validation.Services
{
    public class OcrService
    {
        public string ExtractText(string filePath)
        {
            
            var extension = Path.GetExtension(filePath).ToLower();

            if (extension != ".png" && extension != ".jpg" && extension != ".jpeg")
            {
                throw new Exception("Formato não suportado pelo OCR (use imagem)");
            }

            using var engine = new TesseractEngine(@"./tessdata", "por", EngineMode.Default);

            using var img = Pix.LoadFromFile(filePath);

            using var page = engine.Process(img);

            return page.GetText();
        }
    }
}
