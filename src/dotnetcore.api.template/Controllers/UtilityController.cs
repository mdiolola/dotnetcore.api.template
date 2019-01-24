using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnetcore.api.model.Common;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace dotnetcore.api.template.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UtilityController : Controller
    {
        #region "Private Members"

        private readonly Security _security = null;
        private readonly IHostingEnvironment _hostingEnv = null;

        #endregion

        #region "Constructor"

        public UtilityController(IOptions<Security> security, IHostingEnvironment env)
        {
            this._security = security.Value;
            this._hostingEnv = env;
        }
        #endregion
        
        #region "Public Methods"

        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { _hostingEnv.EnvironmentName, _hostingEnv.ApplicationName };
        }

        /// <summary>
        /// TODO: For testing only. remove this before deploying.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="passphrase"></param> 
        /// <returns></returns>
        [HttpGet("testEncoding")]
        public HttpResponse<JsonResult> TestEnconding(string text, string passphrase)
        {
            var encrypt = text.Encrypt(passphrase);
            return new HttpResponse< JsonResult>(
                raw:
                Json(new Dictionary<string, string>()
                {
                    { "encrypt", encrypt },
                    { "Decrypt", encrypt.Decrypt(passphrase) }
                }
                ));
        }

        /// <summary>
        /// TODO: Remove this, for testing only.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        [HttpPut("Encode")]
        public HttpResponse<string> Encode([FromBody] TestData text)
        {
            if (text == null)
                return null;
            return new HttpResponse<string> (raw: text.Str.Encrypt(_security.PassPhrase));
        }

        /// <summary>
        /// TODO: Remove this, for testing only.
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        [HttpPut("Decode")]
        public HttpResponse<string> Decode([FromBody] TestData text)
        {
            if (text == null)
                return null;
            return new HttpResponse<string>(raw: text.Str.Decrypt(_security.PassPhrase));
        }

        [HttpGet("Environment")]
        public HttpResponse<string> Environment()
        {
            return new HttpResponse<string>(raw: _hostingEnv.EnvironmentName);
        }


        #endregion

    }

    public class TestData
    {
        public string Str { get; set; }
    }
}