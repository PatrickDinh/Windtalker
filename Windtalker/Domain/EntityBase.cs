using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Windtalker.Domain
{
    public class EntityBase
    {
        public Guid Id { get; set; }

        [Column(TypeName = "DateTime2")]
        public DateTime DateCreated { get; set; }
    }
}