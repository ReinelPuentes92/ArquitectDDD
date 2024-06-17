using Iktan.Ecommerce.App.Interface;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.Client;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace Iktan.Ecommerce.App.Test
{
    [TestClass]
    public class UserApplicationTest
    {
        private static WebApplicationFactory<Program> _factory = null;
        private static IServiceScopeFactory _scopeFactory = null;

        [ClassInitialize]
        public static void ClassInitialize(TestContext _)
        {
            _factory = new CustomWebApplicationFactory();
            _scopeFactory = _factory.Services.GetRequiredService<IServiceScopeFactory>();
        }

        [TestMethod]
        public void Authenticate_WhenNoParametersAreSent_ValidationErrorMessageIsReturned()
        {
            using var scope = _scopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetService<IUserApplication>();

            //Arrange
            var userName = string.Empty;
            var password = string.Empty;
            var expect = "Errores de Validación.";

            //Act
            var result = context.Authenticate(userName, password);
            var actual = result.Result.Message;

            //Assert
            Assert.AreEqual(expect, actual);
        }

        [TestMethod]
        public void Authenticate_WhenParametersAreSent_ReturnsSuccessfulMessage()
        {
            using var scope = _scopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetService<IUserApplication>();

            //Arrange
            var userName = "rpuentesm";
            var passwrod = "Reinel1992$$";
            var expect = "Autenticacion Exitosa !!!";

            //Act
            var result = context.Authenticate(userName, passwrod);
            var actual = result.Result.Message;

            //Assert
            Assert.AreEqual(expect, actual);
        }

        [TestMethod]
        public void Authenticate_WhenIncorrectParametersAreSent_ReturnsMessageUserDoesNotExist()
        {
            using var scope = _scopeFactory.CreateScope();
            var context = scope.ServiceProvider.GetService<IUserApplication>();

            //Arrange
            var userName = "rpuentesm";
            var passwrod = "123456";
            var expect = "Usuario no existe !!!";

            //Act
            var result = context.Authenticate(userName, passwrod);
            var actual = result.Result.Message;

            //Assert
            Assert.AreEqual(expect, actual);
        }
    }
}