using System;
using System.Threading.Tasks;
using MakeCodeBetter.Business;
using Moq;
using Xunit;

namespace MakeCodeBetter
{
    public class UserServiceUnitTests
    {
        [Fact]
        public async Task GetByIdInHappyPath()
        {
            var mockUserRepository = new Mock<IUserRepository>();
            mockUserRepository.Setup(i => i.GetAsync(It.IsAny<int>())).ReturnsAsync(new UserEntity()
            {
                Id = 2,
                Birthday = "1998-07-12"
            });
            var service = new UserService(mockUserRepository.Object);
            var testResultUnderTest = await service.GetAsync(2);
            // current date is 2020-04-03, expected age is 21
            Assert.True(testResultUnderTest.Age == 21);
        }

        [Fact]
        public async Task GetByIdWhenBirthdayIsInvalid()
        {
            var mockUserRepository = new Mock<IUserRepository>();
            mockUserRepository.Setup(i => i.GetAsync(It.IsAny<int>())).ReturnsAsync(new UserEntity()
            {
                Id = 2,
                Birthday = "1999-23-12"
            });
            var service = new UserService(mockUserRepository.Object);
            var testResultUnderTest = await service.GetAsync(2);
            Assert.True(testResultUnderTest.Age == -1);
        }

        [Fact]
        public async Task GetByIdWhenUserRepositoryThrowException()
        {
            var mockUserRepository = new Mock<IUserRepository>();
            mockUserRepository.Setup(i => i.GetAsync(It.IsAny<int>())).ThrowsAsync(new DatabaseConnectionNotAvailableException());
            var service = new UserService(mockUserRepository.Object);

            var shouldBeNullValueException = await Record.ExceptionAsync(async () =>
            {
                var testResultUnderTest = await service.GetAsync(2);
            });
            Assert.IsType<UserServiceNotAvailableException>(shouldBeNullValueException);
        }
    }
}
