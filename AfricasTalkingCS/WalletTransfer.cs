namespace AfricasTalkingCS
{   
    using System.Collections.Generic;

    using Newtonsoft.Json;

    public partial class WalletTransfer 
    {
        [JsonProperty("username")]
        public string Username {get; set;}

        [JsonProperty("productName")]
        public string ProductName {get; set;}

        [JsonProperty("targetProductCode")]
        public int TargetProductCode {get; set;}

        [JsonProperty("currencyCode")]
        public string CurrencyCode {get; set;}

        [JsonProperty("amount")]
        public decimal Amount {get; set;}

        [JsonProperty("metadata")]
        public Dictionary <string, string> Metadata {get; set;}
    
    }

    public partial class WalletTransfer 
    {
        public static WalletTransfer WalletTransferJson(string json) => JsonConvert.DeserializeObject<WalletTransfer>(json, ObjConverter.Settings); 
    }
}