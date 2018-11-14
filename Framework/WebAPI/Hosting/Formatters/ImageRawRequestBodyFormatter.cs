
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Net.Http.Headers;
using System;
using System.IO;
using System.Threading.Tasks;

//Example: https://weblog.west-wind.com/posts/2017/Sep/14/Accepting-Raw-Request-Body-Content-in-ASPNET-Core-API-Controllers
namespace Framework.WebAPI.Hosting.Formatters
{
    /// <summary>
    /// Formatter that allows content of type image/png.  Allows for a single input parameter
    /// in the form of:
    /// 
    /// public string RawString([FromBody] string data)
    /// </summary>
    public class ImageRawRequestBodyFormatter : InputFormatter
    {
        public ImageRawRequestBodyFormatter()
        {
            SupportedMediaTypes.Add(new MediaTypeHeaderValue("image/png"));
        }


        /// <summary>
        /// Allowimage/png to be processed
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override bool CanRead(InputFormatterContext context)
        {
            if (context == null) throw new ArgumentNullException(nameof(context));

            var contentType = context.HttpContext.Request.ContentType;
            var lenght = context.HttpContext.Request.ContentLength;

            if (!lenght.HasValue)
                return false;

            //Validate content-lenght
            if (lenght > 2097152)
                return false;

            if (string.IsNullOrEmpty(contentType) || contentType == "image/png")
                return true;

            return false;
        }

        /// <summary>
        /// Handle image/png for byte[] results
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public override async Task<InputFormatterResult> ReadRequestBodyAsync(InputFormatterContext context)
        {
            var request = context.HttpContext.Request;
            var contentType = context.HttpContext.Request.ContentType;

            if (contentType == "image/png")
            {
                using (var reader = new StreamReader(request.Body))
                {
                    var content = Convert.FromBase64String(await reader.ReadToEndAsync());
                    return await InputFormatterResult.SuccessAsync(content);
                }
            }

            return await InputFormatterResult.FailureAsync();
        }
    }
}
