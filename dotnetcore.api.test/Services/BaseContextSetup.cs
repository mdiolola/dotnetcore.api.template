using dotnetcore.api.template.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace dotnetcore.api.test.Services
{
    public class BaseContextSetup
    {
        protected readonly DbContextOptions<ApiContext> _options;

        public BaseContextSetup()
        {
            _options = new DbContextOptionsBuilder<ApiContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

        }
    }
}
