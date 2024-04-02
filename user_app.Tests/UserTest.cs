using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Results;
using NUnit; 
using NUnit.Framework;
using Moq;
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

        [TestCase]
        public void Get_Returns_All_Users()
        {
            var users = new List<User>
            {
                new User { Id = Guid.NewGuid(),
                    FirstName = "User1",
                    LastName = "LastName",
                    IsEnabled=true,
                    PasswordHash="***",
                    ExpiryDate= DateTime.Now.AddDays(100),
                    PasswordChangeDate= DateTime.Now.AddDays(60),
                },
                new User { Id = Guid.NewGuid(),
                    FirstName = "User2",
                    LastName = "LastName2",
                    IsEnabled=true,
                    PasswordHash="***2",
                    ExpiryDate= DateTime.Now.AddDays(100),
                    PasswordChangeDate= DateTime.Now.AddDays(60),
                }
            }.AsQueryable();

            _mockContext.Setup(m => m.Users).Returns((System.Data.Entity.DbSet<User>)users);

            var result = _controller.Get();

            Assert.Equals(users.Count(), result.Count());
        }

        [TestCase]
        public async Task Post_Creates_New_User()
        {
            var newUser = new User
            {
                Id = Guid.NewGuid(),
                FirstName = "User1",
                LastName = "LastName",
                IsEnabled = true,
                PasswordHash = "***",
                ExpiryDate = DateTime.Now.AddDays(100),
                PasswordChangeDate = DateTime.Now.AddDays(60)
            };

            IHttpActionResult actionResult = await _controller.Post(newUser);
            var createdResult = actionResult as CreatedODataResult<User>;

            Assert.Equals(newUser.Name, createdResult.Entity.Name);
        }

        [TestCase]
        public async Task Put_Updates_Existing_User()
        {
            Guid ID = Guid.NewGuid();
            var existingUser = new User
            {
                Id = ID,
                FirstName = "User1",
                LastName = "LastName",
                IsEnabled = true,
                PasswordHash = "***",
                ExpiryDate = DateTime.Now.AddDays(100),
                PasswordChangeDate = DateTime.Now.AddDays(60),
            };

            var users = new List<User> { existingUser }.AsQueryable();
            _mockContext.Setup(m => m.Users).Returns((System.Data.Entity.DbSet<User>)users);
            _mockContext.Setup(m => m.Users.FindAsync(ID)).ReturnsAsync(existingUser);

            var updatedUserData = new User
            {
                Id = ID,
                FirstName = "Updated First Name",
                LastName = "Updated Last Name",
                IsEnabled = false, 
                PasswordHash = "Updated Password Hash",
                ExpiryDate = DateTime.Now.AddDays(200), 
                PasswordChangeDate = DateTime.Now.AddDays(90) 
            };

            IHttpActionResult actionResult = await _controller.Put(ID, updatedUserData);
            var updatedResult = actionResult as UpdatedODataResult<User>;

            var updatedUser = updatedResult.Entity;
            Assert.That(updatedUserData.FirstName, Is.EquivalentTo(updatedUser.FirstName));
            Assert.That(updatedUserData.LastName, Is.EquivalentTo(updatedUser.LastName));
            Assert.That(updatedUserData.PasswordHash, Is.EquivalentTo(updatedUser.PasswordHash));
        }


        [TestCase]
        public async Task Delete_Creates_New_User()
        {
            Guid ID = Guid.NewGuid();
            var users = new List<User>
            {
                new User { Id = ID,
                    FirstName = "User1",
                    LastName = "LastName",
                    IsEnabled=true,
                    PasswordHash="***",
                    ExpiryDate= DateTime.Now.AddDays(100),
                    PasswordChangeDate= DateTime.Now.AddDays(60),
                },
                new User { Id = Guid.NewGuid(),
                    FirstName = "User2",
                    LastName = "LastName2",
                    IsEnabled=true,
                    PasswordHash="***2",
                    ExpiryDate= DateTime.Now.AddDays(100),
                    PasswordChangeDate= DateTime.Now.AddDays(60),
                }
            }.AsQueryable();

            _mockContext.Setup(m => m.Users).Returns((System.Data.Entity.DbSet<User>)users);
            var result = await _controller.Delete(ID);
            var results = _controller.Get();

            Assert.Equals(results.Count(), users.Count() -1 );
        }
    }
}
