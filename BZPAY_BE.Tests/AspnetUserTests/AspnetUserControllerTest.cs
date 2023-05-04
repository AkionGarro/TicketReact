using BZPAY_BE.BussinessLogic.auth.ServiceInterface;
using BZPAY_BE.Controllers;
using BZPAY_BE.DataAccess;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace BZPAY_BE.UnitTests.AspnetUserTests
{
    /// <summary>
    /// AspnetUser controller unit tests
    /// </summary>
    public class AspnetUserControllerTest
    {
        #region Fields
        private readonly Mock<IAspnetUserService> _serviceMock;
        private readonly AspnetUserController _controller;
        #endregion

        #region Constructor
        public AspnetUserControllerTest()
        {
            _serviceMock = new Mock<IAspnetUserService>();
            _controller = new AspnetUserController(_serviceMock.Object);
        }
        #endregion

        #region Public Methods    


        [Fact]
        /// <summary>
        /// StartSession Success
        /// </summary>
        public async Task Controller_ShouldReturnSuccess_WhenStartSession()
        {
            // Arrange
            var fakeRequest = new LoginRequest() { Username = "PLK_TEST1", Password = "RtEmMLgq" };
            AspnetUserDo fakeResponse = GetFakeAspnetUserDo();
            _serviceMock.Setup(x => x.StartSessionAsync(It.IsAny<LoginRequest>()))
                               .Returns(Task.FromResult(fakeResponse))
                               .Verifiable();
            // Act
            var actionResult = await _controller.StartSessionAsync(fakeRequest);

            // Assert
            _serviceMock.Verify();
            Assert.Equal((actionResult.Result as OkObjectResult).StatusCode, StatusCodes.Status200OK);
            Assert.Equal(fakeRequest.Username, (((ObjectResult)actionResult.Result).Value as AspnetUserDo).UserName);
        }

        [Fact]
        /// <summary>
        /// StartSession Not Found
        /// </summary>
        public async Task Controller_ShouldReturnNotFound_WhenStartSession()
        {
            // Arrange
            var fakeRequest = new LoginRequest() { Username = "PLK_TEST1", Password = "RtEmMLgq" };
            AspnetUserDo fakeResponse = null;
            _serviceMock.Setup(x => x.StartSessionAsync(It.IsAny<LoginRequest>()))
                               .Returns(Task.FromResult(fakeResponse))
                               .Verifiable();
            // Act
            var actionResult = await _controller.StartSessionAsync(fakeRequest);

            // Assert
            _serviceMock.Verify();
            Assert.Equal((actionResult.Result as NotFoundResult).StatusCode, StatusCodes.Status404NotFound);
            Assert.Null(fakeResponse);
        }

        [Fact]
        /// <summary>
        /// Forgot Password Success
        /// </summary>
        public async Task Controller_ShouldReturnSuccess_WhenForgotPassword()
        {
            // Arrange
            var fakeRequest = "PLK_TEST1";
            AspnetUserDo fakeResponse = GetFakeAspnetUserDo();
            _serviceMock.Setup(x => x.ForgotPasswordAsync(It.IsAny<string>()))
                               .Returns(Task.FromResult(fakeResponse))
                               .Verifiable();
            // Act
            var actionResult = await _controller.ForgotPasswordAsync(fakeRequest);

            // Assert
            _serviceMock.Verify();
            Assert.Equal((actionResult.Result as OkObjectResult).StatusCode, StatusCodes.Status200OK);
            Assert.Equal(fakeRequest, (((ObjectResult)actionResult.Result).Value as AspnetUserDo).UserName);
        }

        [Fact]
        /// <summary>
        /// ForgotPassword Not Found
        /// </summary>
        public async Task Controller_ShouldReturnNotFound_WhenForgotPassword()
        {
            // Arrange
            var fakeRequest = "PLK_TEST1";
            AspnetUserDo fakeResponse = null;
            _serviceMock.Setup(x => x.ForgotPasswordAsync(It.IsAny<string>()))
                               .Returns(Task.FromResult(fakeResponse))
                               .Verifiable();
            // Act
            var actionResult = await _controller.ForgotPasswordAsync(fakeRequest);

            // Assert
            _serviceMock.Verify();
            Assert.Equal((actionResult.Result as NotFoundResult).StatusCode, StatusCodes.Status404NotFound);
            Assert.Null(fakeResponse);
        }

        [Fact]
        /// <summary>
        /// UpdatePassword success
        /// </summary>
        public async Task Controller_ShouldReturnSuccess_WhenUpdatePassword()
        {
            // Arrange
            var fakeRequest = new UpdatePasswordRequest() { Username = "PLK_TEST1", Password = "RtEmMLgq" };
            AspnetUserDo fakeResponse = GetFakeAspnetUserDo();
            _serviceMock.Setup(x => x.UpdatePasswordAsync(It.IsAny<UpdatePasswordRequest>()))
                               .Returns(Task.FromResult(fakeResponse))
                               .Verifiable();
            // Act
            var actionResult = await _controller.UpdatePasswordAsync(fakeRequest);

            // Assert
            _serviceMock.Verify();
            Assert.Equal((actionResult.Result as OkObjectResult).StatusCode, StatusCodes.Status200OK);
            Assert.Equal(fakeRequest.Username, (((ObjectResult)actionResult.Result).Value as AspnetUserDo).UserName);
        }

        [Fact]
        /// <summary>
        /// UpdatePassword Not Found
        /// </summary>
        public async Task Controller_ShouldReturnNotFound_WhenUpdatePassword()
        {
            // Arrange
            var fakeRequest = new UpdatePasswordRequest() { Username = "PLK_TEST1", Password = "RtEmMLgq" };
            AspnetUserDo fakeResponse = null;
            _serviceMock.Setup(x => x.UpdatePasswordAsync(It.IsAny<UpdatePasswordRequest>()))
                               .Returns(Task.FromResult(fakeResponse))
                               .Verifiable();
            // Act
            var actionResult = await _controller.UpdatePasswordAsync(fakeRequest);

            // Assert
            _serviceMock.Verify();
            Assert.Equal((actionResult.Result as NotFoundResult).StatusCode, StatusCodes.Status404NotFound);
            Assert.Null(fakeResponse);
        }
        #endregion

        #region Private Methods

        /// <summary>
        /// Returns fake CompanyOrder
        /// </summary>
        private static AspnetUserDo GetFakeAspnetUserDo()
        {
            return
            new AspnetUserDo()
            {
                UserId = new Guid("B76C7D21-AC33-4CED-8B91-80FD3BB350DF"),
                UserName = "PLK_TEST1",
                LoweredUserName = "plk_test1",
                MobileAlias = "",
                IsAnonymous = false,
                LastActivityDate = DateTime.Now,
                Membership = new AspnetMembershipDo()
                {
                    UserId = new Guid("B76C7D21-AC33-4CED-8B91-80FD3BB350DF"),
                    Password = "6PnZ8LY6wCBnrCNnRs+EzuWb2A8=",
                    PasswordFormat = 1,
                    PasswordSalt = "amCREOsrZDLI/9yu8TbkUw==",
                    MobilePin = "",
                    Email = "lleiton@bzpay.com.mx",
                    LoweredEmail = "lleiton@bzpay.com.mx",
                    PasswordQuestion = "Algo por ahí",
                    PasswordAnswer = "Algo por allá",
                    IsApproved = true,
                    IsLockedOut = false,
                    CreateDate = new DateTime(2022, 05, 10, 23, 34, 00),
                    LastLoginDate = new DateTime(2022, 05, 10, 23, 34, 00),
                    LastPasswordChangedDate = new DateTime(2022, 05, 10, 23, 34, 00),
                    LastLockoutDate = new DateTime(2022, 05, 10, 23, 34, 00),
                    FailedPasswordAttemptCount = 1,
                    FailedPasswordAttemptWindowStart = new DateTime(2022, 05, 10, 23, 34, 00),
                    FailedPasswordAnswerAttemptCount = 1,
                    FailedPasswordAnswerAttemptWindowStart = new DateTime(2022, 05, 10, 23, 34, 00),
                    Comment = ""
                }
            };
        }

        #endregion
    }
}