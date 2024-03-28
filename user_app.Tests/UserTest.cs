using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNet.OData;
using Moq;
using NUnit.Framework;
using user_app.Controllers;
using user_app.Models;

namespace user_app.Tests
{
    [TestFixture]
    public class UserTest
    {
        private UsersController _controller;
        private Mock<UsersContext> _mockContext;

        [SetUp]
        public void Setup()
        {
            _mockContext = new Mock<UsersContext>();
            _controller = new UsersController { Request = new System.Net.Http.HttpRequestMessage() };
            _controller.db = _mockContext.Object;
        }

        [Test]
        public void Get_Returns_All_Users()
        {
            var users = new List<User>
            {
                new User { Id = Guid.NewGuid(), Name = "User1" },
                new User { Id = Guid.NewGuid(), Name = "User2" }
            }.AsQueryable();

            _mockContext.Setup(m => m.Users).Returns(users);

            var result = _controller.Get();

            Assert.AreEqual(users.Count(), result.Count());
        }

        [Test]
        public async Task Post_Creates_New_User()
        {
            var newUser = new User { Name = "New User" };

            IHttpActionResult actionResult = await _controller.Post(newUser);
            var createdResult = actionResult as CreatedODataResult<User>;

            Assert.IsNotNull(createdResult);
            Assert.AreEqual(newUser.Name, createdResult.Entity.Name);
        }

    }
}
