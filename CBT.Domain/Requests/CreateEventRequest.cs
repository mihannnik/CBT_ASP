using CBT.Domain.Models;
using Newtonsoft.Json;
using System.Text.Json;

namespace CBT.Domain.Requests
{
    public class CreateEventRequest
    {
        [JsonProperty]
        public required string Title { get; set; }
        [JsonProperty]
        public required string Description { get; set; }
        [JsonProperty]
        public required DateTime Date { get; set; }
        [JsonProperty]
        public int JoinLimit { get; set; } = -1;
    }
}
