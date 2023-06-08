using CodebridgeTask.Controllers;
using CodebridgeTask.Models;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace CodeBridgeTests.Controllers
{
    public class DogsControllerTests
    {
        private readonly DogsController _dogsController;

        public DogsControllerTests()
        {
            // Create an in-memory database for testing
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(databaseName: "CodeBridgeDB")
                .Options;

            // Create an instance of AppDbContext using the in-memory database options
            var dbContext = new AppDbContext(options);

            // Create an instance of DogsController with the AppDbContext
            _dogsController = new DogsController(dbContext);
        }

        // Write your unit tests using xUnit assertions
        [Fact]
        public void Test_CreateDog_ReturnsBadRequest_WhenInvalidData()
        {
            // Arrange
            var invalidDog = new Dog();

            // Act
            var result = _dogsController.CreateDog(invalidDog);

            // Assert
            // Add your assertions here to check the expected response
        }
    }
}
