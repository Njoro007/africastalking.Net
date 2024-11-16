using AfricasTalkingCS;
using Newtonsoft.Json;
using System.Text;


namespace AfricasTalkingCS.Test
{

    public class USSDServiceTest
    {
        private static string apikey = "6c36e56b86c24c2ff66adaff340d60793dff71ac304bc551f7056ca76dd8032a";
        private static string username = "sandbox";
        private readonly AfricasTalkingGateway _atGWInstance = new AfricasTalkingGateway(username, apikey);

        [Fact]
        public void DoSendUssdPush()
        {
            var phoneNumber = "+254720000002";
            var menu = "CON What is your purpose?\n";
            var checkoutToken = _atGWInstance.CreateCheckoutToken(phoneNumber);
            string tkn = checkoutToken["token"];
            var gatewayResponse = _atGWInstance.InitiateUssdPushRequest(phoneNumber, menu, tkn);
            var success = gatewayResponse["status"] == "Queued" && gatewayResponse["errorMessage"] == "None";
            Assert.True(success, "Should successfully initiate a USSS Push request");
        }
    }
}