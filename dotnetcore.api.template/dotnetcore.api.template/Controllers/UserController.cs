using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dotnetcore.api.model.template.Common;
using dotnetcore.api.model.template.Users;
using dotnetcore.api.template.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace dotnetcore.api.template.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {

        #region "Private Members"

        private readonly IUserService _userService = null;

        #endregion

        #region "Constructors"

        public UserController(IUserService userService)
        {
            this._userService = userService;
        }

        [HttpPut("{id}")]
        public HttpResponse<object> Put(long id, [FromBody] User obj)
        {
            HttpResponse<object> response = null;

            _userService.Update(obj);

            return response = new HttpResponse<object>(raw: obj);

        }

        [HttpPost]
        public HttpResponse<object> Post([FromBody] User obj)
        {
            HttpResponse<object> response = null;

            _userService.Insert(obj);

            return response = new HttpResponse<object>(raw: obj);

        }


        [HttpGet("{id}")]
        public HttpResponse<object> Get(long id)
        {
            HttpResponse<object> response = null;

            var obj = _userService.Select(id);

            return response = new HttpResponse<object>(raw: obj);

        }

        [HttpGet]
        public HttpResponse<object> Get()
        {
            HttpResponse<object> response = null;

            var obj = _userService.Select();

            return response = new HttpResponse<object>(raw: obj);

        }


        [HttpDelete("{id}")]
        public HttpResponse<object> Delete(long id)
        {
            HttpResponse<object> response = null;

            _userService.Delete(id);

            return response = new HttpResponse<object>(raw: id);

        }

        #endregion
    }
}