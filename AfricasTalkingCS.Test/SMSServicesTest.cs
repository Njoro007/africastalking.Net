using AfricasTalkingCS;
using Newtonsoft.Json;

namespace AfricasTalkingCS.Test
{
    public class SMSServicesTest
    {
        private static string apikey = "atsk_c0c065cc2d7e9e46b0d71024";
        private static string username = "SENDER_ID";
        private static AfricasTalkingGateway _atGWInstance = new AfricasTalkingGateway(username, apikey);


        //Using Legacy
        [Fact]
        public void DoSendMessageToSingleValidNumber_Legacy()
        {
            var phoneNumber = "+254720000000";
            var message = "Hello Legacy Endpoint";

            var gatewayResponse = _atGWInstance.SendMessage(phoneNumber, message, IsLegacy: true, username);

            var success = gatewayResponse["SMSMessageData"]["Recipients"][0]["status"] == "Success";
            Assert.True(success, "Should successfully send message to a valid phone number");
        }

        //New EndPoint
        [Fact]
        public void DoSendMessageToSingleValidNumber()
        {
            var phoneNumber = "+254720000000";
            var message = "Hello New Endpoint";

            var gatewayResponse = _atGWInstance.SendMessage(phoneNumber, message, IsLegacy: false, username);

            var success = gatewayResponse["SMSMessageData"]["Recipients"][0]["status"] == "Success";
            Assert.True(success, "Should successfully send message to a valid phone number");
        }


        //Using Legacy
        [Fact]
        public void DoSendMessageToManyValidNumbers_Legacy()
        {
            var phoneNumerList = "+254720000000,+254720000000";
            var message = "Good Evening Legacy Endpoint";

            var gatewayResponse = _atGWInstance.SendMessage(phoneNumerList, message, IsLegacy:true, username);

            // Avoid loops
            var recipient1Success = gatewayResponse["SMSMessageData"]["Recipients"][0]["status"] == "Success";
            var recipient2Success = gatewayResponse["SMSMessageData"]["Recipients"][1]["status"] == "Success";

            var success = recipient1Success || recipient2Success;

            Assert.True(success, "Atleast one recipient is Success");
        }

        //New EndPoint
        [Fact]
        public void DoSendMessageToManyValidNumbers()
        {
            var phoneNumerList = "+254720000000,+254720000000";
            var message = "Good Evening New Endpoint";

            var gatewayResponse = _atGWInstance.SendMessage(phoneNumerList, message, IsLegacy: false, username);

            // Avoid loops
            var recipient1Success = gatewayResponse["SMSMessageData"]["Recipients"][0]["status"] == "Success";
            var recipient2Success = gatewayResponse["SMSMessageData"]["Recipients"][1]["status"] == "Success";

            var success = recipient1Success || recipient2Success;

            Assert.True(success, "Atleast one recipient is Success");
        }

        [Fact]
        public void GetAirtimeBalance()
        {
            var gw = new AfricasTalkingGateway(username, apikey);

            dynamic response = gw.GetUserData();
            var success = response != null;

            Assert.True(success);
        }


        [Fact]
        public void DoSendMessageViaSenderId()
        {
            var phoneNumber = "+254720000000";
            var message = "Hello Mr. Wick";

            var gatewayResponse = _atGWInstance.SendMessage(phoneNumber, message, IsLegacy: false, username);
            var success = gatewayResponse["SMSMessageData"]["Recipients"][0]["status"] == "Success";
            
            Assert.True(success, "Should send message to avalid phoneNumber via SenderID");
        }


        [Fact]
        public void DoSendMessageViaShortCode()
        {
            var phoneNumber = "+254720000000";
            var message = "Hello Neo";
            var shortCode = "44000";

            var gatewayResponse = _atGWInstance.SendMessage(phoneNumber, message, IsLegacy: false, shortCode);
            var success = gatewayResponse["SMSMessageData"]["Recipients"][0]["status"] == "Success";
            
            Assert.True(success, "Should send message to avalid phoneNumber via Shortcode");
        }

        [Fact]
        public void DoCreateCheckoutToken()
        {
            var phoneNumber = "+254720000000";
            var gatewayResponse = _atGWInstance.CreateCheckoutToken(phoneNumber);
            var success = gatewayResponse["token"];

            Assert.False(string.IsNullOrEmpty(Convert.ToString(success)));
        }

        [Fact]
        public void DoCreateSubscription()
        {
            var phoneNumber = "+254720000000";
            var shortCode = "44000";
            var keyword = "Coolguy";

            var gatewayResponse = _atGWInstance.CreateSubscription(phoneNumber, shortCode, keyword);
            var success = gatewayResponse["status"] == "Success";

            Assert.True(success);
        }

        [Fact]
        public void DoDeleteSubscription()
        {
            var phoneNumber = "+254720000000";
            var shortCode = "44000";
            var keyword = "Coolestguy";

            var subscribeUser = _atGWInstance.CreateSubscription(phoneNumber, shortCode, keyword);
            var deleteUserSub = _atGWInstance.DeleteSubscription(phoneNumber, shortCode, keyword);

            // Should be mocked
            var success = deleteUserSub["description"] == "Succeeded";

            Assert.True(success, "Should successfully delete a subscription");
        }
    }
}