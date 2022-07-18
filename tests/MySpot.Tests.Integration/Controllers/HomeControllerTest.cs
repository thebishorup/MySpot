using Shouldly;

namespace MySpot.Tests.Integration.Controllers
{
    public class HomeControllerTest : ControllerTest
    {
        public HomeControllerTest(OptionsProvider optionsProvider) : base(optionsProvider)
        {
        }

        [Fact]
        public async Task get_base_endpoint_should_return_200_status_code_and_api_name()
        {
            var response = await Client.GetAsync("/");

            var content = await response.Content.ReadAsStringAsync();

            content.ShouldBe("MySpot API [dev]");
        }
    }
}
