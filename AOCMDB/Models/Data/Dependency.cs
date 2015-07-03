using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;

namespace AOCMDB.Models.Data
{
    /// <summary>
    /// This generic class will allow dependency relationships to be made accross all objects in the database, 
    /// which will allow for dependency visualization with the dependency wheel: http://www.redotheweb.com/DependencyWheel/
    /// Article on Inheritance:http://www.asp.net/mvc/overview/getting-started/getting-started-with-ef-using-mvc/implementing-inheritance-with-the-entity-framework-in-an-asp-net-mvc-application
    /// </summary>
    [TrackChanges]
    public abstract class Dependency
    {
        [Key]
        [Required]
        public long Id { get; set; }
       
        /// <summary>
        /// The Human Readable Name
        /// </summary>
        [Required]
        [Index(IsUnique = true)]
        [MaxLength(450)]
        public string Name { get; set; }

        /// <summary>
        /// The friendly name of the DatabaseNode
        /// </summary>
        public string FriendlyName { get; set; }

        /// <summary>
        /// This field gives a detailed description
        /// </summary>
        [AllowHtml]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Details", Description = "This field gives a detailed description of the External Logical Storage")]
        public string Details { get; set; }

        public virtual ICollection<Dependency> DownstreamDependencies { get; set; }
        
        public void AddUpstreamDependency(Dependency dependency)
        {
            dependency.DownstreamDependencies.Add(this);
        }

    }
}