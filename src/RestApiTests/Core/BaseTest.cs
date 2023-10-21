using AutoMapper;
using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using DevExpress.Xpo.Metadata;
using RestApiProject.DAL.DataObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Loader;
using System.Text;
using System.Threading.Tasks;

namespace RestApiTests.Core
{
    public class BaseTest
    {
        protected IMapper Mapper { get; private set; }
        protected UnitOfWork UnitOfWork { get; private set; }
        protected IDataLayer MssqlDataLayer { get; private set; }
        public BaseTest()
        {
            InitializeAutomapper();
        }

        void InitializeAutomapper()
        {
            List<Type> profileTypes = new();
            var businessAsssembly = AssemblyLoadContext.Default.LoadFromAssemblyPath($"{Environment.ProcessPath}\\RestApiProject.BL.dll");
            profileTypes.AddRange(businessAsssembly.GetTypes()
                .Where(dll =>
                    dll.IsAssignableFrom(typeof(Profile)))
                .ToList());
            MapperConfiguration mapperConfiguration = new(cfg =>
            {
                profileTypes.ForEach(profileType =>
                {
                    cfg.AddProfile(profileType);
                });
            });

            Mapper = new Mapper(mapperConfiguration);
        }

        void CreateDataLayer()
        {
            XpoDefault.Session = new Session() { CaseSensitive = false };
            XPDictionary dictionary = new ReflectionDictionary();

            List<Type> dalTypes = new();
            var dalAssembly = AssemblyLoadContext.Default.LoadFromAssemblyPath($"{Environment.ProcessPath}\\RestApiProject.DAL.dll");

            dalTypes.AddRange(dalAssembly.GetTypes()
                .Where(dll =>
                    dll.IsAssignableFrom(typeof(BaseDataObject)))
                .ToList());

            dictionary.GetDataStoreSchema(dalTypes);



            MssqlDataLayer = new ThreadSafeDataLayer(dictionary, GetConnectionProvider("Data Source=localhost;Initial Catalog=TestDbForTestProject;Integrated Security=True;TrustServerCertificate=True"));
        }

        private IDataStore GetConnectionProvider(string connectionString)
            => MSSqlConnectionProvider.CreateProviderFromString(connectionString, AutoCreateOption.None, out _);
    }
}
