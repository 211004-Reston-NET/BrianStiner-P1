using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xunit;
using BusinessLogic;
using Microsoft.EntityFrameworkCore;
using DataAccessLogic;
using Models;

namespace UnitTests
{

    [TestClass]
    public class UserSecurityTests
    {
        private readonly DbContextOptions<revaturedatabaseContext> _options;

        public UserSecurityTests()
        {
            _options = new DbContextOptionsBuilder<revaturedatabaseContext>() //In-memory database
                .UseSqlite("Filename = TestingDatabase.Db")
                .Options;

                Seed();
        }

        // public bool CreateUser(string p_username, string p_unhashedpassword1, string p_unhashedpassword2, string p_email = "", string p_phone = "");
        // public bool Login(string p_username, string p_passwordtocheck);

        // // User methods for dealing with password security and user authentication.
        [Fact]
        public void IsEqualPasswordTest(){
                // Arrange
                var b = new Business(new RepositorySQL(new revaturedatabaseContext(_options)));

                // Act
                var result = b.IsEqual("password", "password");

                // Assert
                Xunit.Assert.True(result);
        }

        [Fact]
        public void SaltedHashPasswordTest(){
                // Arrange
                var b = new Business(new RepositorySQL(new revaturedatabaseContext(_options)));


                // Act
                var result = b.SaltedHashPassword("username", "password");

                // Assert
                Xunit.Assert.NotNull(result);
                Xunit.Assert.NotEqual("password", result);
                Xunit.Assert.NotEqual("usernamepassword", result);

        }

        [Fact]
        public void CheckPasswordTest(){
                // Arrange
                var b = new Business(new RepositorySQL(new revaturedatabaseContext(_options)));
                var u = new User("user2", b.SaltedHashPassword("user2", "password2"), "email2", "phone2", 2);

                // Act
                b.Add(u);
                var result = b.CheckPassword("password2", u);
                var result2 = b.CheckPassword("password11", u);

                // Assert
                Xunit.Assert.True(result);
                Xunit.Assert.False(result2);
        }

        [Fact]
        public void IsUniqueUsernameTest(){
                // Arrange
                var b = new Business(new RepositorySQL(new revaturedatabaseContext(_options)));
                var u = new User("user2", "password2", "email2", "phone2", 2);
                b.Add(u);

                // Act
                var result = b.IsUniqueUsername("user");
                var result2 = b.IsUniqueUsername("user2");
                var result3 = b.IsUniqueUsername("user3");

                // Assert
                Xunit.Assert.False(result);
                Xunit.Assert.False(result2);
                Xunit.Assert.True(result2);
        }







        private void Seed(){
            using (var context = new revaturedatabaseContext(_options)) //makes a customer and store
            {
                var b = new Business(new RepositorySQL(new revaturedatabaseContext(_options)));
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                context.Users.Add(new User("user", b.SaltedHashPassword("user", "password"), "email", "phone", 1));
                context.SaveChanges();
            }
        }
        
    }
}