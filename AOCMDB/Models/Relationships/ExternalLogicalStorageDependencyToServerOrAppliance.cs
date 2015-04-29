using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web.Mvc;
using AOCMDB.Models.Nodes;

namespace AOCMDB.Models.Relationships
{
    public class ExternalLogicalStorageDependencyToServerOrAppliance
    {
        /// <summary>
        /// Internal ApplicationID to AOCMDB
        /// </summary>
        [Key]
        [Column(Order = 1)]
        [Required]
        [Display(Name = "Downstream DownstreamExternalLogicalStorage ID", Description = "Internal ID of the Downstream DownstreamExternalLogicalStorage")]
        public long DownstreamExternalLogicalStorageId { get; set; }

        /// <summary>
        /// Internal ServerID to AOCMDB
        /// </summary>
        [Key]
        [Column(Order = 2)]
        [Required]
        [Display(Name = "Upstream ServerOrAppliance ID", Description = "Internal ID of the Upstream ServerOrAppliance")]
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