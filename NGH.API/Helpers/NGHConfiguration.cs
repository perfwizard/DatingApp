using System;
using Microsoft.Extensions.Configuration;

namespace NGH.API.Helpers
{
    public class NGHConfiguration
    {
        private readonly IConfiguration config;
        public NGHConfiguration(IConfiguration config)
        {
            this.config = config;

        }
        public int GetMaxPageSize()
        {
            return Int32.Parse(this.config.GetSection("Pagination")["MaxPageSize"]);
        }
    }
}