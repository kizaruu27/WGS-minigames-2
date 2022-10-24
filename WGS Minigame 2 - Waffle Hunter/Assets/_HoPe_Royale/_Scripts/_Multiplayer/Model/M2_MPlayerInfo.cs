using Newtonsoft.Json;
using System;

namespace RoyaleMinigames.Models.Http.PlayerInfo
{
    // [Preserve(AllMembers = true)]
    [Serializable]
    public class M2_MPlayerInfo
    {
#nullable enable
        public bool? status { get; set; }
        public string? message { get; set; }
        public M2_Data? data { get; set; }

        [JsonConstructor]
        public M2_MPlayerInfo() { }
    }

    // [Preserve(AllMembers = true)]
    [Serializable]
    public class M2_Data
    {
#nullable enable

        public string? id { get; set; }
        public string? full_name { get; set; }
        public string? uname { get; set; }
        public string? email { get; set; }
        public int? utype { get; set; }
        public string? sol_address { get; set; }
        public string? google_id { get; set; }
        public DateTime? created_at { get; set; }
        public DateTime? updated_at { get; set; }

        [JsonConstructor]
        public M2_Data() { }
    }
}