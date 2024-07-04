using Book_subscription.Server.API.DTOs;
using Book_subscription.Server.Core.Entities;
using Book_subscription.Server.Core.Services;
using Book_subscription.Server.Core.Services.Interfaces;
using Book_subscription.Server.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book_subscription.tests.Services
{
    /// <summary>
    /// Tests for the UserService class.
    /// </summary>

    public class UserServiceTests
    {
        private readonly Mock<UserManager<User>> _mockUserManager;
        private readonly Mock<IJwtAuthService> _mockJwtAuthService;
        private readonly IUserService _userService;

        public UserServiceTests()
        {

            var services = new ServiceCollection();

            // Add Identity services
            services.AddIdentity<User, IdentityRole>()
                    .AddEntityFrameworkStores<ApplicationDbContext>()
                    .AddDefaultTokenProviders();

            // Add JwtAuthService mock
            _mockJwtAuthService = new Mock<IJwtAuthService>();

            // Build service provider
            var serviceProvider = services.BuildServiceProvider();

            // Initialize UserManager with proper dependencies
            _mockUserManager = new Mock<UserManager<User>>(
                Mock.Of<IUserStore<User>>(),
                Mock.Of<IOptions<IdentityOptions>>(),
                Mock.Of<IPasswordHasher<User>>(),
                new IUserValidator<User>[0],
                new IPasswordValidator<User>[0],
                Mock.Of<ILookupNormalizer>(),
                Mock.Of<IdentityErrorDescriber>(),
                serviceProvider,
                Mock.Of<ILogger<UserManager<User>>>()
            );

            _userService = new UserService(_mockUserManager.Object, _mockJwtAuthService.Object);


        }

        /// <summary>
        /// Tests the RegisterAsync method for successful registration scenarios.
        /// </summary>
        [Fact]
        public async Task RegisterAsync_SuccessfulRegistration_ReturnsUserResponseDTO()
        {
            // Arrange
            var registerDto = new RegisterUserDTO { UserName = "testuser", Email = "test@example.com", Password = "testpassword!123" };
            var user = new User { UserName = registerDto.UserName, Email = registerDto.Email };

            _mockUserManager.Setup(u => u.CreateAsync(It.IsAny<User>(), registerDto.Password))
                                   .ReturnsAsync(IdentityResult.Success);

            _mockJwtAuthService.Setup(j => j.GenerateJwtTokenAsync(It.IsAny<string>(), It.IsAny<string>()))
                               .ReturnsAsync("dummy_token");
            // Act
            var result = await _userService.RegisterAsync(registerDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(registerDto.UserName, result.UserName);
            Assert.Equal(registerDto.Email, result.Email);
            Assert.Equal("dummy_token", result.Token);
        }

        /// <summary>
        /// Tests the RegisterAsync method for failed registration scenarios.
        /// </summary>
        [Fact]
        public async Task RegisterAsync_FailedRegistration_ReturnsException()
        {
            // Arrange
            var registerUserDTO = new RegisterUserDTO
            {
                UserName = "testuser",
                Email = "testuser@example.com",
                Password = "Password@123"
            };

            var errorDescription = "Username already taken."; 

            _mockUserManager.Setup(x => x.CreateAsync(It.IsAny<User>(), It.IsAny<string>()))
                            .ReturnsAsync(IdentityResult.Failed(new IdentityError { Description = errorDescription }));

            // Act
            Func<Task> act = async () => await _userService.RegisterAsync(registerUserDTO);

            // Assert
            var exception = await Assert.ThrowsAsync<Exception>(act);
            Assert.Contains(errorDescription, exception.Message);
        }

        /// <summary>
        /// Tests the LoginAsync method for successful login scenarios.
        /// </summary>

        [Fact]
        public async Task LoginAsync_SuccessfulLogin_ReturnsUserResponseDTO()
        {
            // Arrange
            var loginDto = new LoginUserDTO { Email = "test@example.com", Password = "Password123!" };
            var user = new User { UserName = "testuser", Email = loginDto.Email };

            _mockUserManager.Setup(u => u.FindByEmailAsync(loginDto.Email))
                            .ReturnsAsync(user);

            _mockUserManager.Setup(u => u.CheckPasswordAsync(user, loginDto.Password))
                            .ReturnsAsync(true);

            _mockJwtAuthService.Setup(j => j.GenerateJwtTokenAsync(It.IsAny<string>(), It.IsAny<string>()))
                               .ReturnsAsync("dummy_token");

            // Act
            var result = await _userService.LoginAsync(loginDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(user.UserName, result.UserName);
            Assert.Equal(user.Email, result.Email);
            Assert.Equal("dummy_token", result.Token);
        }


        /// <summary>
        /// Tests the LoginAsync method for failed login scenarios.
        /// </summary>
        [Fact]
        public async Task LoginAsync_FailedLogin_ReturnsException()
        {
            // Arrange
            var loginUserDTO = new LoginUserDTO
            {
                Email = "testuser@example.com",
                Password = "InvalidPassword"
            };

            var errorDescription = "Invalid login attempt."; // Example error message you expect

            _mockUserManager.Setup(x => x.FindByEmailAsync(loginUserDTO.Email))
                            .ReturnsAsync(new User()); // Assuming a user exists for this email

            _mockUserManager.Setup(x => x.CheckPasswordAsync(It.IsAny<User>(), It.IsAny<string>()))
                            .ReturnsAsync(false);

            // Act
            Func<Task> act = async () => await _userService.LoginAsync(loginUserDTO);

            // Assert
            var exception = await Assert.ThrowsAsync<Exception>(act);
            Assert.Equal(errorDescription, exception.Message);
        }


    }

}
