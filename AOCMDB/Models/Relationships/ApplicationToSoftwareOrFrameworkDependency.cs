using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web.Mvc;
using AOCMDB.Models.Nodes;

namespace AOCMDB.Models.Relationships
{
    public class ApplicationToSoftwareOrFrameworkDependency
    {
        /////////////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////////////
        //AOCMDB Details
        /////////////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Internal ApplicationID to AOCMDB
        /// </summary>
        [Key]
        [Column(Order = 1)]
        [Required]
        [Display(Name = "Downstream Application ID", Description = "Internal ID of the Downstream Application")]
        public long DownstreamApplicationId { get; set; }

        /// <summary>
        /// Internal Revision Number of the Application information
        /// </summary>
        [Key]
        [Column(Order = 2)]
        [Required]
        [Display(Name = "Downstream Application Revision", Description = "Revision number of the Downstream Application")]
        public long DownstreamDatabaseRevision { get; set; }

        /// <summary>
        /// The upstream application ID
        /// </summary>
        [Key]
        [Column(Order = 3)]
        [Required]
        [Display(Name = "Upstream ApplicationToSoftwareOrFrameworkDependency ID", Description = "Internal ID of the External Logical Storage")]
        public long UpstreamApplicationToSoftwareOrFrameworkDependencyID { get; set; }

        public ApplicationToSoftwareOrFrameworkDependency GetUpstreamApplicationToSoftwareOrFrameworkDependency()
        {
            using (AOCMDBContext _dbContext = new AOCMDBContext())
            {
                return _dbContext.ApplicationToSoftwareOrFrameworkDependencys.Find(UpstreamApplicationToSoftwareOrFrameworkDependencyID);
            }
        }
    }
}
