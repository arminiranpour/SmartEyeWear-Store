using Microsoft.Azure.CognitiveServices.Vision.Face;
using Microsoft.Azure.CognitiveServices.Vision.Face.Models;
using System.Drawing;

namespace SmartEyewearStore.Helpers
{
    public class FaceApiHelper
    {
        private readonly FaceClient _client;

        public FaceApiHelper(string endpoint, string key)
        {
            _client = new FaceClient(new ApiKeyServiceClientCredentials(key))
            {
                Endpoint = endpoint
            };
        }

        public async Task<(string faceShape, string skinTone)> AnalyzeFaceAsync(Stream imageStream)
        {
            imageStream.Position = 0;
            var faces = await _client.Face.DetectWithStreamAsync(imageStream,
                returnFaceAttributes: new List<FaceAttributeType> { FaceAttributeType.HeadPose });

            var face = faces.FirstOrDefault();
            if (face == null)
                throw new InvalidOperationException("No face detected");

            var rect = face.FaceRectangle;
            string shape = GetFaceShape(rect);

            imageStream.Position = 0;
            string tone = GetSkinTone(imageStream, rect);

            return (shape, tone);
        }

        private static string GetFaceShape(FaceRectangle rect)
        {
            double ratio = (double)rect.Width / rect.Height;
            if (ratio >= 1.2) return "Round";
            if (ratio <= 0.8) return "Long";
            return "Oval";
        }

        private static string GetSkinTone(Stream stream, FaceRectangle rect)
        {
            using var bmp = new Bitmap(stream);
            var crop = bmp.Clone(new Rectangle(rect.Left, rect.Top, rect.Width, rect.Height), bmp.PixelFormat);
            long r = 0, g = 0, b = 0;
            for (int x = 0; x < crop.Width; x++)
            {
                for (int y = 0; y < crop.Height; y++)
                {
                    var c = crop.GetPixel(x, y);
                    r += c.R; g += c.G; b += c.B;
                }
            }
            int count = crop.Width * crop.Height;
            double brightness = (0.299 * r + 0.587 * g + 0.114 * b) / count;
            if (brightness >= 180) return "Light";
            if (brightness >= 100) return "Medium";
            return "Dark";
        }
    }
}