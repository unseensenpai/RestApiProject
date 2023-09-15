using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using DevExpress.Xpo.Metadata;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RestApiProject.DAL.Datalayers;
using RestApiProject.DAL.DataLayers;
using RestApiProject.DAL.DataObjects;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestApiProject.DAL
{
    public static class DbConnector
    {
        public static readonly object _lockObject = new();
        private static IConfiguration Configuration;
        private static IRestApiDataLayer RestApiDataLayer;

        public static void SetConfiguration(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public static IRestApiDataLayer GetRestApiDataLayer()
        {
            Console.WriteLine("Testststst");
            lock (_lockObject)
            {
                string connectionString = Configuration.GetConnectionString("PostgresqlTest");
                if (connectionString is not null)
                {
                    RestApiDataLayer = CreateDataLayer<IRestApiDataLayer, RestApiThreadSafeDataLayer>(connectionString);
                }
                return RestApiDataLayer;
            }
        }

        private static T CreateDataLayer<T, K>(string connectionString) where T : IDataLayer where K : ThreadSafeDataLayer, new()
        {
            Log.Information("{database} DataLayer Created! {date}", nameof(T), DateTime.Now);
            XpoDefault.Session = new Session
            {
                CaseSensitive = false,
                AutoCreateOption = AutoCreateOption.DatabaseAndSchema
            };
            XPDictionary dictionary = new ReflectionDictionary();
            dictionary.GetDataStoreSchema(typeof(BaseDataObject).Assembly);
            return (T)Activator.CreateInstance(typeof(T), dictionary, GetDataStore(connectionString));
        }

        private static IDataStore GetDataStore(string connectionString)
            => PostgreSqlConnectionProvider
                .CreateProviderFromString(
                    connectionString, 
                    AutoCreateOption.DatabaseAndSchema, 
                    out _);

    }
}
