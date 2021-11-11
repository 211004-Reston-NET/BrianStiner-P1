
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Xunit;
using BusinessLogic;
using Microsoft.EntityFrameworkCore;
using DataAccessLogic;

namespace UnitTests
{

    [TestClass]
    public class IsValid
    {
        private readonly DbContextOptions<revaturedatabaseContext> _options;

        public IsValid()
        {
            _options = new DbContextOptionsBuilder<revaturedatabaseContext>() //In-memory database
                .UseSqlite("Filename = TestingDatabase.Db")
                .Options;
        }

        [Fact]
        public void NameIsValidTest()
        {     
            IBusiness b = new Business(new RepositorySQL(new revaturedatabaseContext(_options)));

            string testname = "OnlyLetters";
            Xunit.Assert.True(b.IsValidName(testname));

            testname = "OnlyLetters and Spaces";
            Xunit.Assert.True(b.IsValidName(testname));

            testname = "OnlyLetters and Spaces and Num53r2";
            Xunit.Assert.False(b.IsValidName(testname));

            testname = "OnlyLetters and Spaces and Num53r2 and S%mb@l$";
            Xunit.Assert.False(b.IsValidName(testname));

            testname = "OnlyLetters_and_Underscores_and_Num53r2_and_S%mb@l$";
            Xunit.Assert.False(b.IsValidName(testname));

            testname = "OnlyLetters-and-Hyphens-and-Num53r2-and-S%mb@l$";
            Xunit.Assert.False(b.IsValidName(testname));
        }

        [Fact]
        public void UsernameIsValidTest()
        {
            IBusiness b = new Business(new RepositorySQL(new revaturedatabaseContext(_options)));

            string testname = "OnlyLetters";
            Xunit.Assert.True(b.IsValidUsername(testname));

            testname = "OnlyLetters and Spaces";
            Xunit.Assert.False(b.IsValidUsername(testname));

            testname = "OnlyLettersandNum53r2";
            Xunit.Assert.True(b.IsValidUsername(testname));

            testname = "OnlyLettersandNum53r2andS%mb@l$";
            Xunit.Assert.False(b.IsValidUsername(testname));

            testname = "OnlyLetters_and_Underscores_and_Num53r2";
            Xunit.Assert.True(b.IsValidUsername(testname));

            testname = "OnlyLetters-and-Hyphens-and-Num53r2";
            Xunit.Assert.True(b.IsValidUsername(testname));
        }

        [Fact]
        public void EmailIsValidTest(){
            IBusiness b = new Business(new RepositorySQL(new revaturedatabaseContext(_options)));

            string testname = "OnlyLettersWith@and.com";
            Xunit.Assert.True(b.IsValidEmail(testname));

            testname = "OnlyLetters and Spaces";
            Xunit.Assert.False(b.IsValidEmail(testname));

            testname = "OnlyLettersandNum53r2With@and.com";
            Xunit.Assert.True(b.IsValidEmail(testname));

            testname = "OnlyLettersandNum53r2andS%mb@l$With@and.com";
            Xunit.Assert.False(b.IsValidEmail(testname));

            testname = "OnlyLetters_and_Underscores_and_Num53r2With@and.com";
            Xunit.Assert.True(b.IsValidEmail(testname));

            testname = "OnlyLetters-and-Hyphens-and-Num53r2With@and.com";
            Xunit.Assert.True(b.IsValidEmail(testname));
        }

        [Fact]
        public void PhoneIsValidTest(){
            IBusiness b = new Business(new RepositorySQL(new revaturedatabaseContext(_options)));

            string phone = "12251";
            Xunit.Assert.False(b.IsValidPhone(phone));

            phone = "5555555555";
            Xunit.Assert.True(b.IsValidPhone(phone));

            phone = "555-555-5555";
            Xunit.Assert.True(b.IsValidPhone(phone));

            phone = "555 555 5555";
            Xunit.Assert.True(b.IsValidPhone(phone));

            phone = "555.555.5555";
            Xunit.Assert.True(b.IsValidPhone(phone));

        }

        [Fact]
        public void AddressIsValidTest(){
            IBusiness b = new Business(new RepositorySQL(new revaturedatabaseContext(_options)));

            string address = "1 h 11100";
            Xunit.Assert.True(b.IsValidAddress(address));

            address = "12512 People are Weird St. 11101";
            Xunit.Assert.True(b.IsValidAddress(address));

            address = "10 just words st no zip";
            Xunit.Assert.False(b.IsValidAddress(address));

            address = "no number just words st 22222";
            Xunit.Assert.False(b.IsValidAddress(address));

        }

        [Fact]
        public void PasswordIsValidTest(){
            
            IBusiness b = new Business(new RepositorySQL(new revaturedatabaseContext(_options)));

            string password = "JustWorDS";
            Xunit.Assert.False(b.IsValidPassword(password));

            password = "woRDsand123145";
            Xunit.Assert.False(b.IsValidPassword(password));

            password = "153523!@";
            Xunit.Assert.False(b.IsValidPassword(password));

            password = "PAssword1!";
            Xunit.Assert.True(b.IsValidPassword(password));
        }

        [Fact]
        public void PriceIsValidTest(){

            IBusiness b = new Business(new RepositorySQL(new revaturedatabaseContext(_options)));

            decimal price = 0.00m;
            Xunit.Assert.True(b.IsValidPrice(price));

            price = 1.00m;
            Xunit.Assert.True(b.IsValidPrice(price));

            price = 1.0001m;
            Xunit.Assert.False(b.IsValidPrice(price));
        }

        [Fact]
        public void QuantityIsValidTest(){

            IBusiness b = new Business(new RepositorySQL(new revaturedatabaseContext(_options)));

            int quantity = 999;
            Xunit.Assert.True(b.IsValidQuantity(quantity));

            quantity = 1;
            Xunit.Assert.True(b.IsValidQuantity(quantity));

            quantity = -1;
            Xunit.Assert.False(b.IsValidQuantity(quantity));
        }
    }
}
    