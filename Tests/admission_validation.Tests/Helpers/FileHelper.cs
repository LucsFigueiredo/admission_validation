using Microsoft.AspNetCore.Http;
using System.IO;

namespace admission_validation.Tests.Helpers
{
    public static class FileHelper
    {
        public static IFormFile CreateFakeFile(string name = "file.pdf")
        {
            var stream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes("fake file content"));

            return new FormFile(stream, 0, stream.Length, "file", name);
        }
    }
}