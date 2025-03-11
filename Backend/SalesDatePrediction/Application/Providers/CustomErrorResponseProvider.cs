using Application.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Net;

namespace Application.Providers
{
    public class CustomErrorResponseProvider : IErrorResponseProvider
    {
        public IActionResult CreateResponse(ErrorResponseContext context)
        {
            var result = Result<string>.BadRequest("The requested API version is invalid or not supported.");
            var jsonResponse = JsonConvert.SerializeObject(result, new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                NullValueHandling = NullValueHandling.Ignore,
                ContractResolver = new DefaultContractResolver
                {
                    NamingStrategy = new CamelCaseNamingStrategy(),
                }
            });
            return new ContentResult
            {
                Content = jsonResponse,
                ContentType = "application/json",
                StatusCode = (int)HttpStatusCode.BadRequest
            };
        }
    }
}
