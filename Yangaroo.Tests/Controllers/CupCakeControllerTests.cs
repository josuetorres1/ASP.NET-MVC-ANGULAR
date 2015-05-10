using Yangaroo.Core;
using Yangaroo.Core.Repositories;
using Yangaroo.Controllers;
using Moq;
using NUnit.Framework;

namespace Yangaroo.Tests.Controllers
{
    class CupCakeControllerTests
    {
        [TestFixture]
        public class WhenTellingEventsControllerToGiveUsEventInformation
        {
            private CupCakeApiController _controller;
            private Mock<ICupCakeRepository> _mockCupCakesRepository;
            
            [SetUp]
            public void BeforeEachTest()
            {
                var mockUnitOfWorkFactory = new Mock<IUnitOfWorkFactory>();
                var mockUnitOfWork = new Mock<IUnitOfWork>();
                _mockCupCakesRepository = new Mock<ICupCakeRepository>();
                
                mockUnitOfWorkFactory.Setup(x => x.Create()).Returns(mockUnitOfWork.Object);
                mockUnitOfWork.Setup(x => x.CupCakeRepository).Returns(_mockCupCakesRepository.Object);

                _controller = new CupCakeApiController(mockUnitOfWorkFactory.Object);
            }
        }
    }
}
