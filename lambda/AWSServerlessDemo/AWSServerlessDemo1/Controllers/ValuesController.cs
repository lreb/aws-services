using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Amazon;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2;
//using Amazon.DynamoDBv2.DataModel;
//using Amazon.DynamoDBv2.DocumentModel;
// using Amazon.Runtime;
//using Amazon.SecurityToken.Model;
using Microsoft.AspNetCore.Mvc;
using Amazon.Runtime;
using Amazon.SecurityToken;
using Amazon.SecurityToken.Model;
using Amazon.CognitoIdentity;

namespace AWSServerlessDemo1.Controllers
{

    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private readonly string AWSAccessKeyId = "";
        private readonly string AWSSecretAccessKey = "";
        private readonly string tableName = "lreb";

        private readonly string roleARN = "";
        private readonly string cognitoID = "";

        private static AWSCredentials longTermCredentials_;

        // GET api/values
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            try
            {   // COGNITO SERVICE
                //AWSCredentials credentials = new CognitoAWSCredentials(cognitoID,RegionEndpoint.USEast1);
                //var client = new AmazonDynamoDBClient(credentials,RegionEndpoint.USEast1);

                // IAM SERVICE - Local
                //var credentials = new BasicAWSCredentials(AWSAccessKeyId, AWSSecretAccessKey);
                //var client = new AmazonDynamoDBClient(credentials, RegionEndpoint.USEast1);

                // This approach works when you are depending on a role
                var client = new AmazonDynamoDBClient(RegionEndpoint.USEast1);

                ScanFilter scanFilter = new ScanFilter();
                ScanOperationConfig soc = new ScanOperationConfig()
                {
                    Filter = scanFilter
                };
                DynamoDBContext context = new DynamoDBContext(client);
                AsyncSearch<lreb> search = context.FromScanAsync<lreb>(soc, null);
                List<lreb> documentList = new List<lreb>();
                do
                {
                    documentList = await search.GetNextSetAsync(default(System.Threading.CancellationToken));
                } while (!search.IsDone);

                return Ok(documentList);
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }

            
        }

        private async Task<AssumeRoleResponse> GetAssumeRoleResponseAsync(AmazonSecurityTokenServiceClient client, AssumeRoleRequest request)
        {
            var response = await client.AssumeRoleAsync(request);
            return response;
        }

        private async Task<GetCallerIdentityResponse> GetCallerIdentityResponseAsync(AmazonSecurityTokenServiceClient client, GetCallerIdentityRequest request)
        {
            var caller = await client.GetCallerIdentityAsync(request);
            return caller;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }

    public class lreb 
    {
        public string id { get; set; }
        public string value { get; set; }
    }
}
