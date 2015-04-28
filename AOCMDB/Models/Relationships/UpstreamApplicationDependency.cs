using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web.Mvc;

namespace AOCMDB.Models
{
    public class UpstreamApplicationDependency
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
        /// Internal Revision Number of  the Application information
        /// </summary>
        [Key]
        [Column(Order = 2)]
        [Required]
        [Display(Name = "Upstream Application Revision", Description = "Revision number of the Application")]
        public long DownstreamDatabaseRevision { get; set; }

        /// <summary>
        /// The Human Readable Application Name
        /// </summary>
        [Key]
        [Column(Order = 3)]
        [Required]
        [Display(Name = "Upstream Application ID", Description = "Internal ID of the Application")]
        public long UpstreamApplicationID { get; set; }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////////////
        //User Details
        /////////////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////////////
        

        public Application GetUpstreamApplication()
        {
            using (AOCMDBContext _dbContext = new AOCMDBContext())
            {
                return _dbContext.GetLatestApplicationVersions().Where(P => P.ApplicationId == UpstreamApplicationID).FirstOrDefault();
            }            
        }
    }

    
}