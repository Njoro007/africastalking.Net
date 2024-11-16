using AfricasTalkingCS;
using Newtonsoft.Json;
using System.Text;

namespace AfricasTalkingCS.Test
{
    class AirtimeUsers
    {
        [JsonProperty("phoneNumber")]
        public string PhoneNumber { get; set; }

        [JsonProperty("amount")]
        public string Amount { get; set; }
    }

    public class AirtimeServiceTest
    {
        private static string apikey = "atsk_c3a2c9cc2d7e9e46b0d71024";
        private static string username = "SENDER_ID";
        private static AfricasTalkingGateway _atGWInstance = new AfricasTalkingGateway(username, apikey);


        [Fact]
        public void DoSendAirtimeToOneUser()
        {
            var airtimeUser = new AirtimeUsers();
            airtimeUser.PhoneNumber = "+254712321233";
            airtimeUser.Amount = "KES 100";
            var airtimeRec = JsonConvert.SerializeObject(airtimeUser);
            var gatewayResponse = _atGWInstance.SendAirtime(airtimeRec);
            var success = gatewayResponse["errorMessage"] == "None" || gatewayResponse["errorMessage"] == "A duplicate request was received within the last 5 minutes";
            Assert.True(success);
        }


        [Fact]
        public void DoSendToManyUsers()
        {
            var airtimeUser1 = new AirtimeUsers();
            airtimeUser1.PhoneNumber = "+254720000001";
            airtimeUser1.Amount = "KES 100";
            string airtime1Recipient = JsonConvert.SerializeObject(airtimeUser1);
            var airtimeUser2 = new AirtimeUsers();
            airtimeUser2.PhoneNumber = "+254720000002";
            airtimeUser2.Amount = "KES 100";
            string airtime2Recipient = JsonConvert.SerializeObject(airtimeUser2);
            StringBuilder airtimeStringBuilderInstance = new StringBuilder(airtime1Recipient, 100);
            // Hack
            airtimeStringBuilderInstance.Append($",{airtime2Recipient}");
            var gatewayResponse = _atGWInstance.SendAirtime(airtimeStringBuilderInstance);
            var success = gatewayResponse["errorMessage"] == "None" || gatewayResponse["errorMessage"] == "A duplicate request was received within the last 5 minutes";
            Assert.True(success);
        }
    }
}