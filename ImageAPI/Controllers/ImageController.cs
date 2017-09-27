using Amazon;
using Amazon.Rekognition;
using Amazon.S3;
using Amazon.S3.Transfer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ImageAPI.Controllers
{
    public class ImageController : ApiController
    {
        // GET: api/Image
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Image/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Image
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Image/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Image/5
        public void Delete(int id)
        {
        }
        static string bucketName = "mytest12333test";
        static string keyName = "test";
        static IAmazonS3 client;

        void uploadImage()
        {
            using (client  = new AmazonS3Client(RegionEndpoint.USEast1))
            {

                string filePath = @"C:\Users\dilee\Pictures\3.jpg";
                TransferUtility utility = new TransferUtility(client);
                utility.Upload(filePath, bucketName);
            }
        }

        void compareFaces()
        {
            Amazon.Rekognition.Model.S3Object s3 = new Amazon.Rekognition.Model.S3Object();
            s3.Bucket = bucketName;
            s3.Name = "1.jpg";
            Amazon.Rekognition.Model.Image source = new Amazon.Rekognition.Model.Image()
            {
                S3Object = s3,
            };

            Amazon.Rekognition.Model.S3Object s3target = new Amazon.Rekognition.Model.S3Object();
            s3target.Bucket = bucketName;
            s3target.Name = "2.jpg";
            Amazon.Rekognition.Model.Image target = new Amazon.Rekognition.Model.Image()
            {
                S3Object = s3target
            };
            Amazon.Rekognition.Model.CompareFacesRequest re = new Amazon.Rekognition.Model.CompareFacesRequest();

            re.SourceImage = source;
            re.TargetImage = target;

            var RekognitionClient = new AmazonRekognitionClient(RegionEndpoint.USEast1);

            var result = RekognitionClient.CompareFaces(re);

        }
    }
}
