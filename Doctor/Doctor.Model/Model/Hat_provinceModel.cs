using Newtonsoft.Json;
namespace Doctor.Model
{
    public class Hat_provinceModel
    {
        public System.Int32 Id { get; set; }
        public System.String ProvinceID { get; set; }
        public System.String Province { get; set; }
        [JsonProperty("en-US")]
        public System.String EN_US { get; set; }
    }

}
