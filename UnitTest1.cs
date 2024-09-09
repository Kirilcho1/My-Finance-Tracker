using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyFinanceTracker.Models;
using MyFinanceTracker.ViewModels;
using System.Security.Claims;
using Xunit;
using Moq;
using MyFinanceTracker.Controllers;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using Microsoft.Extensions.Logging;
using MyFinanceTracker.Models;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace MyFinanceTrackerTest
{

    public class UserControllerTests
    {
        [Fact]
        public void Index_ReturnsAViewResult()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            using (var context = new ApplicationDbContext(options))
            {
                new User { UserId = 1, Username = "Kiril", Password = "Kikimiki01!", Email = "Kiril2001@abv.bg" };
                context.SaveChanges();
            }

            // Act
            using (var context = new ApplicationDbContext(options))
            {
                var logger = new Mock<ILogger<UserController>>();
                var controller = new UserController(context, logger.Object);
                var result = controller.Login();

                // Assert
                Assert.IsType<ViewResult>(result);
            }
        }
    }
}