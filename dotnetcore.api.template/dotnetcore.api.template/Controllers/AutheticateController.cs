using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnetcore.api.model.template.Common;
using dotnetcore.api.model.template.Constants;
using dotnetcore.api.model.template.Users;
using dotnetcore.api.template.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace dotnetcore.api.template.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class AutheticateController : ControllerBase
    {
        #region "Private Members"

        private readonly IAuthenticateService _authenticateService = null;

        #endregion

        #region "Constructors"

        public AutheticateController(IAuthenticateService authenticateService)
        {
            this._authenticateService = authenticateService;
        }

        #endregion

        #region "Http Methods"    
        [AllowAnonymous]
        [HttpPost]
        [Route("TryRequestToken")]
        public HttpResponse<UserAuth> TryRequestToken([FromBody] User obj)
        {
            HttpResponse<UserAuth> response = null;
            UserAuth auth = null;

            try
            {
                obj.Username = obj.Username.ToLower();

                if (!this._authenticateService.TryRequestToken(obj.Username, obj.Password, out auth))
                {
                    if (auth == null)
                        throw new CustomException(ErrorMessages.LoginInvalid);
                }

                response = new HttpResponse<UserAuth>(raw: auth);
            }
            catch (CustomException ex)
            {
                response = new HttpResponse<UserAuth>(raw: auth, errorMessage: ex.Message);
            }
            catch (Exception ex)
            {
                response = new HttpResponse<UserAuth>(raw: auth, errorMessage: ErrorMessages.ExceptionError);
            }

            return response;
        }

        [Authorize]
        [HttpGet]
        [Route("TryConnect")]
        public HttpResponse<object> TryConnect()
        {
            return new HttpResponse<object>();
        }

        #endregion

    }
}