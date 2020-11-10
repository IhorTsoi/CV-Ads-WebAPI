using CV_Ads_WebAPI.Contracts.DTOs.Response;
using CV_Ads_WebAPI.Domain.Options;
using Microsoft.Azure.CognitiveServices.Vision.Face;
using Microsoft.Azure.CognitiveServices.Vision.Face.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using ApplicationGender = CV_Ads_WebAPI.Domain.Constants.Gender;
using MicrosoftGender = Microsoft.Azure.CognitiveServices.Vision.Face.Models.Gender;

namespace CV_Ads_WebAPI.Services
{
    public class FaceDetectionService
    {
        private readonly FaceDetectionOptions _faceDetectionOptions;

        public FaceDetectionService(IOptions<FaceDetectionOptions> faceDetectionOptions)
        {
            _faceDetectionOptions = faceDetectionOptions.Value;
        }

        public async Task<IEnumerable<FaceDetectedResponse>> DetectFaces(Stream imageStream)
        {
            IFaceClient client = AuthorizeFaceClient();

            FaceAttributeType?[] returnFaceAttributes = new FaceAttributeType?[] 
            { 
                FaceAttributeType.Gender,
                FaceAttributeType.Age 
            };
            IList<DetectedFace> detectedFaces = await client.Face.DetectWithStreamAsync(
                imageStream, returnFaceAttributes: returnFaceAttributes);

            return detectedFaces.Select(MapDomainToResponseFaceDetected);
        }

        private IFaceClient AuthorizeFaceClient() =>
            new FaceClient(new ApiKeyServiceClientCredentials(_faceDetectionOptions.SubscriptionKey))
            {
                Endpoint = _faceDetectionOptions.Endpoint
            };

        private FaceDetectedResponse MapDomainToResponseFaceDetected(DetectedFace detectedFace)
        {
            ApplicationGender gender = ApplicationGender.NotSpecified;
            int? age = null;

            if (detectedFace.FaceAttributes.Gender != null)
            {
                gender = (detectedFace.FaceAttributes.Gender == MicrosoftGender.Male) ?
                    ApplicationGender.Male :
                    ApplicationGender.Female;
            }

            if (detectedFace.FaceAttributes.Age != null)
            {
                age = Convert.ToInt32(detectedFace.FaceAttributes.Age);
            }

            return new FaceDetectedResponse(gender, age);
        }
    }
}
