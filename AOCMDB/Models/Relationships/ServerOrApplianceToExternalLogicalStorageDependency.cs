using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web.Mvc;
using AOCMDB.Models.Nodes;

namespace AOCMDB.Models.Relationships
{
    public class ServerOrApplianceToExternalLogicalStorageDependency
    {
        /// <summary>
        /// Internal ApplicationID to AOCMDB
        /// </summary>
        [Key]
        [Column(Order = 1)]
        [Required]
        [Display(Name = "Downstream DownstreamServerOrAppliance ID", Description = "Internal ID of the Downstream DownstreamServerOrAppliance")]
        public long DownstreamServerOrApplianceId { get; set; }

        /// <summary>
        /// Internal ServerID to AOCMDB
        /// </summary>
        [Key]
        [Column(Order = 2)]
        [Required]
        [Display(Name = "Upstream ExternalLogicalStorage ID", Description = "Internal ID of the Upstream ExternalLogicalStorage")]
        public long UpstreamExternalLogicalStorageId { get; set; }

        public ExternalLogicalStorageNode GetUpstreamUpstreamServerOrAppliance()
        {
            using (AOCMDBContext _dbContext = new AOCMDBContext())
            {
                return _dbContext.ExternalLogicalStorages.Find(UpstreamExternalLogicalStorageId);
            }
        }
    }
}