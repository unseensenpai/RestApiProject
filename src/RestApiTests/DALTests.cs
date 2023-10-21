using DevExpress.Xpo;
using RestApiTests.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public void DALTest()
        {
            Assert.IsNotNull(_dataLayer);
            using UnitOfWork uow = new(_dataLayer);
        }
    }
}
