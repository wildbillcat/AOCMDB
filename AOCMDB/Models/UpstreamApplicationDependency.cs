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
        public long UpstreamApplicationDependencyId { get; set; }

        /// <summary>
        /// Internal Revision Number of  the Application information
        /// </summary>
        [Key]
        [Column(Order = 2)]
        [Required]
        public long DatabaseRevision { get; set; }

        [Required]
        public string CreatedByUser { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////////////
        //User Details
        /////////////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////////////
        
        /// <summary>
        /// The Human Readable Application Name
        /// </summary>
        [Required]
        [Display(Name = "Upstream Application ID", Description = "Internal ID of the Application")]
        public long UpstreamApplicationID { get; set; }


        public Application GetUpstreamApplication()
        {
            using (AOCMDBContext _dbContext = new AOCMDBContext())
            {
                return _dbContext.GetLatestApplicationVersions().Where(P => P.ApplicationId == UpstreamApplicationID).FirstOrDefault();
            }            
        }
    }

    
}