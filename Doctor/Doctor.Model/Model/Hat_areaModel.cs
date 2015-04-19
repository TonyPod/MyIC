
using Newtonsoft.Json;
namespace Doctor.Model
{
    public class Hat_areaModel
    {
        public System.Int32 Id { get; set; }
        public System.String AreaID { get; set; }
        public System.String Area { get; set; }
        public System.String Father { get; set; }
        [JsonProperty("en-US")]
        public System.String EN_US { get; set; }
    }
}
