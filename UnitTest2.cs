using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using MyFinanceTracker.Controllers;
using MyFinanceTracker.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace MyFinanceTrackerTest
{
    public class TransactionsControllerTests
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
                context.Transactions.Add(new Transaction { TransactionId = 1, CategoryId = 1, Amount = 100, Date = DateTime.Now });
                context.SaveChanges();
            }

            // Act
            using (var context = new ApplicationDbContext(options))
            {
                var logger = new Mock<ILogger<TransactionsController>>();
                var controller = new TransactionsController(context, logger.Object);
                var result = controller.Create(new Transaction { TransactionId = 2, CategoryId = 2, Amount = 130, Date = DateTime.Now });
                Assert.NotNull(result);
            }
            // Assert
            using (var context = new ApplicationDbContext(options))
            {
                var count = context.Transactions.Count();
                Assert.Equal(1, count);
            }
        }
    }
}  