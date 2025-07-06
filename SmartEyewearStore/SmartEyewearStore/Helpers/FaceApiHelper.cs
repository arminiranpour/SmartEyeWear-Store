using Microsoft.Azure.CognitiveServices.Vision.Face;
using Microsoft.Azure.CognitiveServices.Vision.Face.Models;
using System.IO;
using System.Threading.Tasks;

namespace SmartEyewearStore.Helpers
{
    public class FaceApiHelper
    {
        private readonly IFaceClient _faceClient;

        public FaceApiHelper(string endpoint, string key)
        {
            _faceClient = new FaceClient(new ApiKeyServiceClientCredentials(key))
            {
                Endpoint = endpoint
            };
        }

        public async Task<DetectedFace> DetectFaceAttributesAsync(Stream imageStream)
        {
            var faceAttributes = new FaceAttributeType[]
            {
                FaceAttributeType.HeadPose,
                FaceAttributeType.Age,
                FaceAttributeType.Gender
            };

            var faces = await _faceClient.Face.DetectWithStreamAsync(imageStream, returnFaceLandmarks: true, returnFaceAttributes: faceAttributes);
            return faces.Count > 0 ? faces[0] : null;
        }
    }
}
