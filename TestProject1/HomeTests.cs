namespace TestProject1
{
    public class HomeTests
    {
        [Fact]
        public async Task Index_ReturnsAViewResult_WithAListOfBrainstormSessions()
        {
            //Arrange
            var mockRepo = new Mock<IBrainStormSessionRepository>();
            mockRepo.Setup(repo => repo.ListAsync())
                .ReturnsAsync(SessionTestData.GetTestSessions());
            var controller = new HomeController(mockRepo.Object);

            //Act
            var result = await controller.Index();

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<StormSessionViewModel>>
                (viewResult.ViewData.Model);
            Assert.Equal(2, model.Count());
        }

        [Fact]
        public async Task IndexPost_ReturnsBadRequestResult_WhenModelStatsIsNotValid()
        { 
            //Arrange
            var mockRepo = new Mock<IBrainStormSessionRepository>();
            mockRepo.Setup(repo => repo.ListAsync()).
                ReturnsAsync(SessionTestData.GetTestSessions());
            var controller = new HomeController(mockRepo.Object);
            controller.ModelState.AddModelError("SessionName", "Required");
            var newSession = new HomeController.NewSessionModel();

            //Act
            var result = await controller.Index(newSession);

            //Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.IsType<SerializableError>(badRequestResult.Value);
        }

        [Fact]
        public async Task IndexPost_ReturnsARedirectAndAddsSession_WhenStateIsValid()
        {
            //Arrange
            var mockRepo = new Mock<IBrainStormSessionRepository>();
            mockRepo.Setup(repo => repo.AddAsync(It.IsAny<BrainstormSession>())).
                Returns(Task.CompletedTask).Verifiable();
            var controller = new HomeController(mockRepo.Object);
            var newSession = new HomeController.NewSessionModel()
            {
                SessionName = "Test Name"
            };

            //Act
            var result = await controller.Index(newSession);

            //Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Null(redirectToActionResult.ControllerName);
            Assert.Equal("Index", redirectToActionResult.ActionName);
            mockRepo.Verify();
        }

    }
}