using JekirdekCRM.ENTITIES.Enums;
using JekirdekCRM.ENTITIES.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JekirdekCRM.ENTITIES.Models
{
    public class BaseEntity : IEntity
    {
        public BaseEntity() 
        {
            CreatedAt = DateTime.UtcNow;
            Status = DataStatus.Inserted;
        }
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DataStatus Status { get; set; }
    }
}
