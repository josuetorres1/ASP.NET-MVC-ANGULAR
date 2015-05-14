using System.Web;
using System.Web.Routing;
using Yangaroo.Core;
using Yangaroo.App_Start;
using Moq;
using NUnit.Framework;

namespace Yangaroo.Tests.App_Start
{
    class MvcRouteConfiguratorTests
    {
        public class GivenARoutingContext
        {
            private Mock<IYangarooSessionFactory> _mockYangarooSessionFactory;
            private Mock<IYangarooSession> _mockYangarooSession;
            private Mock<IUnitOfWorkFactory> _mockUnitOfWorkFactory;
            private Mock<IUnitOfWork> _mockUnitOfWork;
            
            [SetUp]
            public void BeforeEachTest()
            {
                _mockYangarooSessionFactory = new Mock<IYangarooSessionFactory>();
                _mockYangarooSession = new Mock<IYangarooSession>();
                _mockUnitOfWorkFactory = new Mock<IUnitOfWorkFactory>();
                _mockUnitOfWork = new Mock<IUnitOfWork>();

                _mockUnitOfWorkFactory.Setup(x => x.Create()).Returns(_mockUnitOfWork.Object);
                _mockYangarooSession.Setup(x => x.CreateUnitOfWorkFactory()).Returns(_mockUnitOfWorkFactory.Object);
                _mockYangarooSessionFactory.Setup(x => x.Create()).Returns(_mockYangarooSession.Object);
            }

            protected RouteData GetRouteData(string filePath)
            {
                var mockRequest = new Mock<HttpRequestBase>(MockBehavior.Strict);
                var mockHttpContextBase = new Mock<HttpContextBase>(MockBehavior.Strict);
                mockHttpContextBase.Setup(x => x.Request).Returns(mockRequest.Object);

                mockRequest.Setup(x => x.AppRelativeCurrentExecutionFilePath).Returns(filePath);
                mockRequest.Setup(x => x.PathInfo).Returns(string.Empty);

                return GetRoutes().GetRouteData(mockHttpContextBase.Object);
            }

            protected RouteCollection GetRoutes()
            {
                var routes = new RouteCollection();
                var routeConfigurator = new MvcRouteConfigurator(_mockYangarooSessionFactory.Object);
                routeConfigurator.Configure(routes);
                return routes;
            }

            protected static void AssertRouteValues(RouteData data, string controller = "Cupcake", string action = "Index", object id = null)
            {
                Assert.IsNotNull(data);

                Assert.AreEqual(controller, data.Values["controller"]);
                Assert.AreEqual(action, data.Values["action"]);
            }
        }

        [TestFixture]
        public class WhenConfiguringDefaultMvcRoutes : GivenARoutingContext
        {
            [Test]
            [TestCase("/blabla/bla/bla/bla")]
            public void ShouldReturnNothingForInvalidUrls(string relativeUrl)
            {
                var data = GetRouteData(relativeUrl);

                Assert.IsNull(data);
            }

            [Test]
            [TestCase("12345")]
            public void ShouldContainEventData(string eventData)
            {
                var data = GetRouteData("~/" + eventData);

                AssertRouteValues(data, eventData);
            }

            [Test]
            [TestCase("en")]
            [TestCase("fr")]
            [TestCase("mx")]
            public void ShouldContainAlternateLanguage(string language)
            {
                var data = GetRouteData("~/123/" + language);

                AssertRouteValues(data, "123", language);
            }
        }

        [TestFixture]
        public class WhenConfiguringYangarooRoutes : GivenARoutingContext
        {
            [Test]
            [TestCase("~/blabla/blabla/blabla/bla")]
            public void ShouldReturnNothingForInvalidUrls(string relativeUrl)
            {
                var data = GetRouteData(relativeUrl);

                Assert.IsNull(data);
            }

            [Test]
            [TestCase("12345")]
            public void ShouldContainEventAndLanguageData(string eventData)
            {
                var data = GetRouteData("~/" + eventData);

                AssertRouteValues(data, eventData);
            }
        }
    }
}
