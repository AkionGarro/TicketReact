using AutoMapper;
using BZPAY_BE.Repositories.Interfaces;
using BZPAY_BE.Models;
using BZPAY_BE.DataAccess;
using BZPAY_BE.Common.Profiles;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;
using BZPAY_BE.BussinessLogic.auth.ServiceInterface;
using BZPAY_BE.BussinessLogic.auth.ServiceImplementation;
using Microsoft.Extensions.Localization;
using BZPAY_BE.Helpers.FakeMail;
using Microsoft.Extensions.Configuration;

namespace BZPAY_BE.UnitTests.AspnetUserTests
{
    /// <summary>
    /// AspnetUser service unit tests
    /// </summary>
    public class AspnetUserServiceTest
    {
        #region Fields
        private readonly Mock<IAspnetUserRepository> _repositoryMock;
        private readonly IMapper _mapper;
        private readonly Mock<IStringLocalizer<SharedResource>> _localizerMock;
        private readonly IAspnetUserService _service;
        private readonly Mock<IEmail> _emailMock;
        private readonly Mock<IConfiguration> _configMock;

        #endregion

        #region Constructor
        public AspnetUserServiceTest()
        {
            _mapper = new Mapper(new MapperConfiguration(cfg => { 
                                                                   cfg.AddProfile(new AspnetUserProfile());
                                                                   cfg.AddProfile(new AspnetMembershipProfile());
                                                                }
                                                        ));
            _emailMock = new Mock<IEmail>();
            _repositoryMock = new Mock<IAspnetUserRepository>();
            _localizerMock = new Mock<IStringLocalizer<SharedResource>>();
            _configMock = new Mock<IConfiguration>();
            _service = new AspnetUserService(_repositoryMock.Object, _mapper, _localizerMock.Object, _configMock.Object);

        }

        #endregion

        #region Public Methods

        [Fact]
        /// <summary>
        /// Start Session 
        /// </summary>
        public async Task Service_Should_StartSessionAsync()
        {
            // Arrange   
            var fakeRequest = new LoginRequest() { Username = "PLK_TEST1", Password = "RtEmMLgq" };
            _repositoryMock.Setup(repository => repository
                                            .GetUserByUserNameAsync(It.IsAny<string>()))
                                            .Returns(Task.FromResult(GetFakeAspnetUser()))
                                            .Verifiable();

            // Act
            AspnetUserDo? result = await _service.StartSessionAsync(fakeRequest);
            _repositoryMock.VerifyAll();

            // Assert
            Assert.NotNull(result);
            Assert.True(result.UserName.Equals(fakeRequest.Username));
            
        }

        [Fact]
        // <summary>
        // Forgot Password 
        // </summary>
        public async Task Service_Should_ForgotPasswordAsync()
        {
            // Arrange
            var email = new EmailListDouble();
            var subject = new ClassThatSendsEmail(email);
            subject.DoSomethingThatCausesAnEmailToGetSent();
            var fakeLocalizedString = new LocalizedString("Body1", "This is a testing Localized String");
            var fakeRequest = "PLK_TEST1";
            var fakeFrontUrl = "https://localhost:3000";

            _repositoryMock.Setup(repository => repository
                                            .GetUserByUserNameAsync(It.IsAny<string>()))
                                            .Returns(Task.FromResult(GetFakeAspnetUser()))
                                            .Verifiable();

            _localizerMock.Setup(localizer => localizer["Body1"])
                          .Returns(fakeLocalizedString)
                          .Verifiable();

            _emailMock.Setup(x => x.Send(It.IsAny<Message>()))
                      .Verifiable();

            _configMock.SetupGet(x => x[It.Is<string>(s => s == "Hosts:FrontEndURL")]).Returns(fakeFrontUrl);

            // Act
            AspnetUserDo? result = await _service.ForgotPasswordAsync(fakeRequest);
            _repositoryMock.VerifyAll();

            // Assert
            Assert.NotNull(result);
            Assert.True(result.UserName.Equals(fakeRequest));
            Assert.Contains(email, message => message.To == "bob@bob.com" && message.Body.Contains("Bob"));

        }

        [Fact]
        /// <summary>
        /// Update Password 
        /// </summary>
        public async Task Service_Should_UpdatePasswordAsync()
        {
            // Arrange
            using (Clock.NowIs(new DateTime(2022, 05, 11, 23, 15, 00)))
            {
                var fakeLocalizedString = new LocalizedString("Body1", "This is a testing Localized String");
                var fakeRequest = new UpdatePasswordRequest()
                {
                    Username = "PLK_TEST1",
                    Password = "CompuC@1821",
                    Hour = new DateTime(2022, 05, 11, 23, 05, 00)
                };

                var updatedUser = GetFakeAspnetUser();
                updatedUser.AspnetMembership.LastPasswordChangedDate = new DateTime(2022, 05, 11, 23, 15, 00);
                updatedUser.AspnetMembership.Password = "177kxJ47xgDgCsqJvU03j0WWCjE=";

                _repositoryMock.Setup(repository => repository
                                                .GetUserByUserNameAsync(It.IsAny<string>()))
                                                .Returns(Task.FromResult(GetFakeAspnetUser()))
                                                .Verifiable();

                _repositoryMock.Setup(repository => repository
                                                .UpdateAsync(It.IsAny<AspnetUser>()))
                                                .Returns(Task.FromResult(updatedUser))
                                                .Verifiable();

                _localizerMock.Setup(localizer => localizer["Body1"])
                              .Returns(fakeLocalizedString)
                              .Verifiable();

                // Act
                AspnetUserDo? result = await _service.UpdatePasswordAsync(fakeRequest);
                _repositoryMock.VerifyAll();

                // Assert
                Assert.NotNull(result);
                Assert.True(result.UserName.Equals(fakeRequest.Username));
                Assert.True(result.Membership.LastPasswordChangedDate.Equals(updatedUser.AspnetMembership.LastPasswordChangedDate));
                Assert.True(result.Membership.Password.Equals(updatedUser.AspnetMembership.Password));
            }
        }

        #endregion       

        #region Private Methods
        ///// <summary>
        ///// Returns fake AspnetUser
        ///// </summary>
        private static AspnetUser? GetFakeAspnetUser()
        {
            return
            new AspnetUser()
            {
                UserId = new Guid("B76C7D21-AC33-4CED-8B91-80FD3BB350DF"),
                UserName = "PLK_TEST1",
                LoweredUserName = "plk_test1",
                MobileAlias = "",
                IsAnonymous = false,
                LastActivityDate = DateTime.Now,
                AspnetMembership = new AspnetMembership()
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