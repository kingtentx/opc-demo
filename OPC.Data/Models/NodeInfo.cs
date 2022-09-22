using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OPC.Data
{
    public class NodeInfo
    {
        [Key]
        public long Id { get; set; }
        [Required, StringLength(200)]
        public string NodeId { get; set; }
        [StringLength(200)]
        public string NodeName { get; set; }
        [StringLength(200)]
        public string NodeClass { get; set; }
        [StringLength(20)]
        public string DataType { get; set; }
        [StringLength(500), AllowNull]
        public string? DataModel { get; set; }

        public DateTime CreateTime { get; set; }

        public DateTime? UpdateTime { get; set; }
    }
}
