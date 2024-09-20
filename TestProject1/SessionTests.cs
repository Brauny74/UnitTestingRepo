using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject1
{
    public class SessionTests
    {
        [Fact]
        public async Task IndexReturnsARedirectToIndexHomeWhenIdIsNull()
        {
            //Arrange
            var controller = new SessionController(sessionRepository: null);

            //Act
            var result = await controller.Index(id: null);

            //
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Home", redirectToActionResult.ControllerName);
            Assert.Equal("Index", redirectToActionResult.ActionName);
        }

        [Fact]
        public async Task IndexReturnsContentWithSessionNotFoundWHenSessionNotFound()
        {
            //Arrange
            int testSessionId = 1;
            var mockRepo = new Mock<IBrainStormSessionRepository>();
            mockRepo.Setup(repo => repo.GetByIdAsync(testSessionId))
                .ReturnsAsync((BrainstormSession)null);
            var controller = new SessionController(mockRepo.Object);

            //Act
            var result = await controller.Index(testSessionId);

            //Assert
            var contentResult = Assert.IsType<ContentResult>(result);
            Assert.Equal("Session not found.", contentResult.Content);
        }

        [Fact]
        public async Task IndexReturnsViewResultWithStormSessionViewModel()
        {
            //Arrange
            int testSessionId = 1;
            var mockRepo = new Mock<IBrainStormSessionRepository>();
            mockRepo.Setup(repo => repo.GetByIdAsync(testSessionId))
                .ReturnsAsync(SessionTestData.GetTestSessions().FirstOrDefault(
                    s => s.Id == testSessionId
                    ));
            var controller = new SessionController(mockRepo.Object);

            //Act
            var result = await controller.Index(testSessionId);

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<StormSessionViewModel>(
                viewResult.ViewData.Model);
            Assert.Equal("Test One", model.Name);
            Assert.Equal(2, model.CreatedDate.Day);
            Assert.Equal(testSessionId, model.Id);
        }
    }
}
