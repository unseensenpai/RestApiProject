using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using DevExpress.Xpo.Metadata;
using RestApiProject.DAL.Datalayers;
using System.Reflection;

namespace RestApiProject.DAL.DataLayers
{
    public class RestApiThreadSafeDataLayer : ThreadSafeDataLayer, IRestApiDataLayer
    {
        public RestApiThreadSafeDataLayer() : base(null)
        {            
        }
        public RestApiThreadSafeDataLayer(IDataStore provider) : base(provider)
        {
        }

        public RestApiThreadSafeDataLayer(XPDictionary dictionary, IDataStore provider) : base(dictionary, provider)
        {
        }

        public RestApiThreadSafeDataLayer(IDataStore provider, params Assembly[] assemblies) : base(provider, assemblies)
        {
        }

        public RestApiThreadSafeDataLayer(IDataStore provider, IEnumerable<Assembly> assemblies) : base(provider, assemblies)
        {
        }

        public RestApiThreadSafeDataLayer(IDataStore provider, params Type[] types) : base(provider, types)
        {
        }

        public RestApiThreadSafeDataLayer(IDataStore provider, IEnumerable<Type> types) : base(provider, types)
        {
        }

        public RestApiThreadSafeDataLayer(XPDictionary dictionary, IDataStore provider, Action<XPDictionary> dictionaryInit) : base(dictionary, provider, dictionaryInit)
        {
        }

        public RestApiThreadSafeDataLayer(XPDictionary dictionary, IDataStore provider, params Assembly[] assemblies) : base(dictionary, provider, assemblies)
        {
        }

        public RestApiThreadSafeDataLayer(XPDictionary dictionary, IDataStore provider, IEnumerable<Assembly> assemblies) : base(dictionary, provider, assemblies)
        {
        }

        public RestApiThreadSafeDataLayer(XPDictionary dictionary, IDataStore provider, params Type[] types) : base(dictionary, provider, types)
        {
        }

        public RestApiThreadSafeDataLayer(XPDictionary dictionary, IDataStore provider, IEnumerable<Type> types) : base(dictionary, provider, types)
        {
        }
    }
}
