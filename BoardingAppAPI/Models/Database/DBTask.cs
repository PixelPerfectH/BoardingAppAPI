﻿using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace BoardingAppAPI.Models.Database
{
    public class DBTask
    {
        [Key]
        [JsonIgnore]
        public int Id { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public int Level { get; set; }

        public bool IsActive { get; set; }
    }
}
