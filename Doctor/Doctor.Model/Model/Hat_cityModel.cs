using Newtonsoft.Json;
namespace Doctor.Model
{
    public class Hat_cityModel
    {
        public System.Int32 Id { get; set; }
        public System.String CityID { get; set; }
        public System.String City { get; set; }
        public System.String Father { get; set; }
        [JsonProperty("en-US")]
        public System.String EN_US { get; set; }
    }

}
