using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI_react_redux.Db
{
    public class Settings
    {
        public string connectionString;
        public string dbName;
        public IConfigurationRoot ConfigurationRoot;
    }
}