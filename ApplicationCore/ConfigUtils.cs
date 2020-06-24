using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;

namespace ApplicationCore
{
    public class ConfigInfo
    {
        public string MediaServer { get; set; }
        public string MediaFolder { get; set; }
        public string APIServer { get; set; }
        public string WebServer { get; set; }
        public string AngularAssetFilePath { get; set; }
        public string ConnectionString { get; set; }
        public string EnvironmentName { get; set; }
        
        //public string APIServerMediaFiles
        //{
        //    get
        //    {
        //        return APIServer + "/MediaFiles";
        //    }
        //}

    }
    public class ServerUtils
    
    {
        private static ConfigInfo _configInfo;

        public static ConfigInfo ConfigInfo
        {
            get
            {
                if (_configInfo == null)
                {
                    var environmentName = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", EnvironmentVariableTarget.Machine);

                    IConfigurationRoot Configuration =
                    new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .AddJsonFile($"appsettings.{environmentName}.json", true)
                    .AddEnvironmentVariables()
                    .Build();

                    _configInfo = new ConfigInfo()
                    {
                        EnvironmentName = environmentName,
                        MediaServer = Configuration.GetValue<string>("MediaServer"),
                        MediaFolder = Configuration.GetValue<string>("MediaFolder"),
                        APIServer = Configuration.GetValue<string>("APIServer"),
                        AngularAssetFilePath = Configuration.GetValue<string>("AngularAssetFilePath"),
                        WebServer = Configuration.GetValue<string>("WebServer"),
                        ConnectionString = Configuration.GetConnectionString("DefaultConnection")
                    };
                }
                
                return _configInfo;
            }
        }

        //public static string ChangeMediaServer(string text)
        //{
        //    if (text == null) return null;
        //    return text.Replace(ConfigInfo.APIServerMediaFiles, ConfigInfo.MediaServer);
        //}
    }
}