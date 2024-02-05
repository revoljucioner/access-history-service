﻿using AccessManager.Models.Database;
using Newtonsoft.Json;

namespace AccessManager.Models.DataModels
{
    public class DepartmentInfo
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        public static explicit operator DepartmentInfo(DepartmentDbModel db) => new DepartmentInfo
        {
            Id = db.Id,
            Name = db.Name
        };
    }
}
