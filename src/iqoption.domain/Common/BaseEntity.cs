using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace iqoption.domain.Common
{
    public abstract class BaseEntity
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("isDeleted")]
        public bool IsDeleted { get; set; }

        [JsonProperty("createdAt")]
        public DateTime CreatedAt { get; set; }

        public BaseEntity()
        {
            Id = Guid.NewGuid().ToString();
            CreatedAt = DateTime.UtcNow;
        }
    }
}
