using Microsoft.Extensions.Options;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace DAL
{
    public class BaseDAL
    {
        private readonly IOptions<AppSettingModels> appSettings;
        protected string connectionString;

        public BaseDAL(IOptions<AppSettingModels> appSettings)
        {
            this.appSettings = appSettings;
            connectionString = this.appSettings.Value.ConStr;
        }
    }
}