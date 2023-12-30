using DevExpress.Xpo;
using RestApiProject.DAL.DataObjects.User;
using RestApiTests.Core;

namespace RestApiTests
{
    [TestFixture]
    public class DALTests : BaseTest
    {
        private IDataLayer _dataLayer;

        [SetUp]
        public void Setup()
        {
            _dataLayer = MssqlDataLayer;
        }

        [Test]
        public async Task DALTest()
        {
            Assert.IsNotNull(_dataLayer);
            using UnitOfWork uow = new(_dataLayer);
            var result = await uow.Query<UserObject>().ToListAsync();
            Assert.IsNotNull(result);
        }
    }
}
