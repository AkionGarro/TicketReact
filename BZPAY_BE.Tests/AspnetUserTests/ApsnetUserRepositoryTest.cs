using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;
using BZPAY_BE.Models;
using BZPAY_BE.Repositories.Implementations;

namespace BZPAY_BE.UnitTests.AspnetUserTests
{
    public class ApsnetUserRepositoryTest
    {
        #region Fields
        private readonly DbContextOptions<MembershipContext> _dbOptions;
        private readonly MembershipContext _context;
        private readonly AspnetUserRepository _repository;
        #endregion

        #region Contructor
        public ApsnetUserRepositoryTest()
        {
            _dbOptions = new DbContextOptionsBuilder<MembershipContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            _context = new MembershipContext(_dbOptions);
            _repository = new AspnetUserRepository(_context);
            SetUpFakeAspnetUsers().ConfigureAwait(false);
        }
        #endregion

        #region Public Methods

        /// <summary>
        /// Get User by UserName
        /// </summary>
        [Fact]
        public async Task Repository_ShouldGetUserByUserName()
        {
            // Arrange            
            var fakeUsername = "PLK_TEST3";

            // Act
            AspnetUser result = await _repository.GetUserByUserNameAsync(fakeUsername);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.UserName.Equals(fakeUsername));
        }

        #endregion

        #region Private Methods
        /// <summary>
        /// Set ups fake AspnetUsers
        /// </summary>
        private async Task SetUpFakeAspnetUsers()
        {
            _context.AddRange(GetFakeAspnetUsers());
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Returns fake aspnetUsersList
        /// </summary>
        private static IEnumerable<AspnetUser> GetFakeAspnetUsers() 
        {
            return new List<AspnetUser>()
            {
                new AspnetUser()
                {
                    UserId = new Guid("6fb7097c-335c-4d07-b4fd-000004e2d28c"),
                    UserName = "PLK_TEST1",
                    LoweredUserName = "plk_test1",
                    MobileAlias = "",
                    IsAnonymous = false,
                    LastActivityDate = DateTime.Now,
                    AspnetMembership = new AspnetMembership()
                    {
                        UserId = new Guid("6fb7097c-335c-4d07-b4fd-000004e2d28c"),
                        Password = "fdsfds",
                        PasswordFormat = 1,
                        PasswordSalt = "dfsfdsfdsf",
                        MobilePin = "",
                        Email = "alguien@correo.com",
                        LoweredEmail = "alguien@correo.com",
                        PasswordQuestion = "Algo por ahí",
                        PasswordAnswer = "Algo por allá",
                        IsApproved = true,
                        IsLockedOut = false,
                        CreateDate = DateTime.Now,
                        LastLoginDate = DateTime.Now,
                        LastPasswordChangedDate = DateTime.Now,
                        LastLockoutDate = new DateTime(2011, 6, 10),
                        FailedPasswordAttemptCount = 1,
                        FailedPasswordAttemptWindowStart = new DateTime(2011, 6, 10),
                        FailedPasswordAnswerAttemptCount = 1,
                        FailedPasswordAnswerAttemptWindowStart = new DateTime(2011, 6, 10),
                        Comment = ""
                    }
                },
                new AspnetUser()
                {
                    UserId = new Guid("7fb7097c-335c-4d07-b4fd-000004e2d28c"),
                    UserName = "PLK_TEST2",
                    LoweredUserName = "plk_test2",
                    MobileAlias = "",
                    IsAnonymous = false,
                    LastActivityDate = DateTime.Now,
                    AspnetMembership = new AspnetMembership()
                    {
                        UserId = new Guid("7fb7097c-335c-4d07-b4fd-000004e2d28c"),
                        Password = "fdsfds",
                        PasswordFormat = 1,
                        PasswordSalt = "dfsfdsfdsf",
                        MobilePin = "",
                        Email = "alguien@correo.com",
                        LoweredEmail = "alguien@correo.com",
                        PasswordQuestion = "Algo por ahí",
                        PasswordAnswer = "Algo por allá",
                        IsApproved = true,
                        IsLockedOut = false,
                        CreateDate = DateTime.Now,
                        LastLoginDate = DateTime.Now,
                        LastPasswordChangedDate = DateTime.Now,
                        LastLockoutDate = new DateTime(2011, 6, 10),
                        FailedPasswordAttemptCount = 1,
                        FailedPasswordAttemptWindowStart = new DateTime(2011, 6, 10),
                        FailedPasswordAnswerAttemptCount = 1,
                        FailedPasswordAnswerAttemptWindowStart = new DateTime(2011, 6, 10),
                        Comment = ""
                    }
                },
                new AspnetUser()
                {
                    UserId = new Guid("8fb7097c-335c-4d07-b4fd-000004e2d28c"),
                    UserName = "PLK_TEST3",
                    LoweredUserName = "plk_test3",
                    MobileAlias = "",
                    IsAnonymous = false,
                    LastActivityDate = DateTime.Now,
                    AspnetMembership = new AspnetMembership()
                    {
                        UserId = new Guid("8fb7097c-335c-4d07-b4fd-000004e2d28c"),
                        Password = "fdsfds",
                        PasswordFormat = 1,
                        PasswordSalt = "dfsfdsfdsf",
                        MobilePin = "",
                        Email = "alguien@correo.com",
                        LoweredEmail = "alguien@correo.com",
                        PasswordQuestion = "Algo por ahí",
                        PasswordAnswer = "Algo por allá",
                        IsApproved = true,
                        IsLockedOut = false,
                        CreateDate = DateTime.Now,
                        LastLoginDate = DateTime.Now,
                        LastPasswordChangedDate = DateTime.Now,
                        LastLockoutDate = new DateTime(2011, 6, 10),
                        FailedPasswordAttemptCount = 1,
                        FailedPasswordAttemptWindowStart = new DateTime(2011, 6, 10),
                        FailedPasswordAnswerAttemptCount = 1,
                        FailedPasswordAnswerAttemptWindowStart = new DateTime(2011, 6, 10),
                        Comment = ""
                    }
                }
            };    
        }

        #endregion
    }
}