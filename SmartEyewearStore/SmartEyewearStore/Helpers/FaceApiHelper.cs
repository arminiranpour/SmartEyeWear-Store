using Microsoft.Azure.CognitiveServices.Vision.Face;
using Microsoft.Azure.CognitiveServices.Vision.Face.Models;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Advanced;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using Rectangle = SixLabors.ImageSharp.Rectangle;

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

        public async Task<(string faceShape, string skinTone)> AnalyzeFaceAsync(Stream faceStream, Stream skinStream)
        {
            faceStream.Position = 0;
            var faces = await _client.Face.DetectWithStreamAsync(faceStream,
                returnFaceLandmarks: true,
                returnFaceAttributes: new List<FaceAttributeType> { FaceAttributeType.HeadPose });

            var face = faces.FirstOrDefault();
            if (face == null || face.FaceLandmarks == null)
                throw new InvalidOperationException("Face or landmarks not detected");

            string shape = GetFaceShape(face.FaceLandmarks);

            skinStream.Position = 0;
            string tone = GetSkinTone(skinStream, face.FaceRectangle);

            return (shape, tone);
        }

        private static string GetFaceShape(FaceLandmarks landmarks)
        {
            double foreheadWidth = Distance(landmarks.EyebrowLeftOuter, landmarks.EyebrowRightOuter);
            double jawWidth = Distance(landmarks.MouthLeft, landmarks.MouthRight); // تغییر اصلی اینجاست
            double faceCenterY = MidPoint(landmarks.EyebrowLeftOuter, landmarks.EyebrowRightOuter);
            double faceLength = Distance(landmarks.NoseTip, new Coordinate { X = (landmarks.EyebrowLeftOuter.X + landmarks.EyebrowRightOuter.X) / 2, Y = faceCenterY }) * 1.7;

            double avgWidth = (foreheadWidth + jawWidth) / 2;
            double lengthToWidthRatio = faceLength / avgWidth;
            double foreheadToJawRatio = foreheadWidth / jawWidth;

            // Round
            if (lengthToWidthRatio < 1.2 && Math.Abs(foreheadToJawRatio - 1) < 0.1)
                return "Round";

            // Square
            if (lengthToWidthRatio < 1.2 && Math.Abs(foreheadToJawRatio - 1) < 0.2)
                return "Square";

            // Oval
            if (lengthToWidthRatio >= 1.3 && lengthToWidthRatio < 1.5 && Math.Abs(foreheadToJawRatio - 1) < 0.2)
                return "Oval";

            // Oblong
            if (lengthToWidthRatio >= 1.5 && Math.Abs(foreheadToJawRatio - 1) < 0.2)
                return "Oblong";

            // Heart
            if (foreheadToJawRatio > 1.1)
                return "Heart";

            // Triangle
            if (foreheadToJawRatio < 0.9)
                return "Triangle";

            // Diamond
            return "Diamond";
        }

        private static double Distance(Coordinate p1, Coordinate p2)
        {
            return Math.Sqrt(Math.Pow(p1.X - p2.X, 2) + Math.Pow(p1.Y - p2.Y, 2));
        }

        private static double MidPoint(Coordinate p1, Coordinate p2)
        {
            return (p1.Y + p2.Y) / 2;
        }

        private static string GetSkinTone(Stream stream, FaceRectangle rect)
        {
            stream.Position = 0;
            using var image = Image.Load<Rgba32>(stream);
            var cropRectangle = new Rectangle(rect.Left, rect.Top, rect.Width, rect.Height);
            image.Mutate(x => x.Crop(cropRectangle));

            long r = 0, g = 0, b = 0;
            for (int y = 0; y < image.Height; y++)
            {
                var rowMemory = image.DangerousGetPixelRowMemory(y);
                var rowSpan = rowMemory.Span;
                for (int x = 0; x < image.Width; x++)
                {
                    var pixel = rowSpan[x];
                    r += pixel.R;
                    g += pixel.G;
                    b += pixel.B;
                }
            }

            int count = image.Width * image.Height;
            double brightness = (0.299 * r + 0.587 * g + 0.114 * b) / count;

            if (brightness >= 220) return "Very Fair";
            if (brightness >= 190) return "Fair";
            if (brightness >= 170) return "Light";
            if (brightness >= 150) return "Medium Light";
            if (brightness >= 130) return "Medium";
            if (brightness >= 110) return "Medium Dark";
            if (brightness >= 85) return "Dark";
            return "Very Dark";
        }
    }
}
