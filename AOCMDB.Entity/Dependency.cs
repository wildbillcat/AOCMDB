using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AOCMDB.Models
{
    
    public abstract class Dependency
    {
        [Key]
        [Required]
        public long Id { get; set; }

        [Column(Order = 2)]
        [Required]
        public long DatabaseRevision { get; set; }

        [Required]
        public string CreatedByUser { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// The Human Readable Name
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// The friendly name of the DatabaseNode
        /// </summary>
        public string FriendlyName { get; set; }

        public virtual ICollection<Dependency> DownstreamDependencies { get; set; }
        
        public void AddUpstreamDependency(Dependency dependency)
        {
            dependency.DownstreamDependencies.Add(this);
        }

    }
}