using System.Threading.Tasks;
using Mearcury.Models.TokenAuth;
using Mearcury.Web.Controllers;
using Shouldly;
using Xunit;

namespace Mearcury.Web.Tests.Controllers
{
    public class HomeController_Tests: MearcuryWebTestBase
    {
        [Fact]
        public async Task Index_Test()
        {
            await AuthenticateAsync(null, new AuthenticateModel
            {
                UserNameOrEmailAddress = "admin",
                Password = "123qwe"
            });

            //Act
            var response = await GetResponseAsStringAsync(
                GetUrl<HomeController>(nameof(HomeController.Index))
            );

            //Assert
            response.ShouldNotBeNullOrEmpty();
        }
    }
}