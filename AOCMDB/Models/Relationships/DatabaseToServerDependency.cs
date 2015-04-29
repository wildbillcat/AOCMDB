using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web.Mvc;
using AOCMDB.Models.Nodes;

namespace AOCMDB.Models.Relationships
{
    public class DatabaseToServerOrApplianceDependency
    {
        /// <summary>
        /// Internal ApplicationID to AOCMDB
        /// </summary>
        [Key]
        [Column(Order = 1)]
        [Required]
        [Display(Name = "Downstream Database ID", Description = "Internal ID of the Downstream Database")]
        public long DownstreamDatabaseId { get; set; }

        /// <summary>
        /// Internal ServerID to AOCMDB
        /// </summary>
        [Key]
        [Column(Order = 2)]
        [Required]
        [Display(Name = "Upstream Server ID", Description = "Internal ID of the Upstream Server")]
        public long UpstreamServerOrApplianceId { get; set; }

        public ServerOrApplianceNode GetUpstreamUpstreamServerOrAppliance()
        {
            using (AOCMDBContext _dbContext = new AOCMDBContext())
            {
                return _dbContext.ServerOrAppliances.Find(UpstreamServerOrApplianceId);
            }
        }
    }
}