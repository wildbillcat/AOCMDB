using AOCMDB.Models.Relationships;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web.Mvc;

namespace AOCMDB.Models.Nodes
{
    /// <summary>
    /// This would be a Network Share, NFS, Iscsi Lun, etc.
    /// </summary>
    public class ExternalLogicalStorageNode
    {
        /// <summary>
        /// Internal ApplicationID to AOCMDB
        /// </summary>
        [Key]
        [Required]
        public long ExternalLogicalStorageId { get; set; }

        /// <summary>
        /// The Name of the logical storage component
        /// </summary>
        [Required]
        [Index(IsUnique = true)]
        [StringLength(450)]
        [Display(Name = "External Logical Storage Name", Description = "This is the name of the logical storage, such as the windows UNC path, or iscsi lun name")]
        public string ExternalLogicalStorageName { get; set; }

        /// <summary>
        /// The Human Readable Name/nickname
        /// </summary>
        [Display(Name = "Friendly Name", Description = "The friendly name of the External Logical Storage")]
        public string FriendlyName { get; set; }

        /// <summary>
        /// This field gives a detailed description
        /// </summary>
        [AllowHtml]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Details", Description = "This field gives a detailed description of the External Logical Storage")]
        public string Details { get; set; }


        public ICollection<ServerOrApplianceNode> GetUpstreamServerOrApplianceDependencies()
        {
            using (AOCMDBContext _dbContext = new AOCMDBContext())
            {
                List<ServerOrApplianceNode> ExternalLogicalStorages = new List<ServerOrApplianceNode>();
                foreach (ExternalLogicalStorageDependencyToServerOrAppliance DataDep in _dbContext.ExternalLogicalStorageDependencyToServerOrAppliances.Where(P => P.DownstreamExternalLogicalStorageId == ExternalLogicalStorageId).ToList())
                {
                    ExternalLogicalStorages.Add(DataDep.GetUpstreamUpstreamServerOrAppliance());
                }
                return ExternalLogicalStorages;
            }
        }

        public ICollection<ApplicationNode> GetDownstreamApplicationDependencies()
        {
            using (AOCMDBContext _dbContext = new AOCMDBContext())
            {
                //Find all of the latest copies of applications
                List<ApplicationNode> LatestApplicationVersions = _dbContext.GetLatestApplicationVersions()//Latest Application Revisions
                    .Join(_dbContext.ApplicationToExternalLogicalStorageDependencys.Where(P => P.UpstreamExternalLogicalStorageNodeNodeID == this.ExternalLogicalStorageId),
                        p => p.ApplicationId,
                        e => e.DownstreamApplicationId,
                        (p, e) => p)
                    .ToList();
                //Select all the latest application revisions which contain an upstream reference to this application                   
                return LatestApplicationVersions;
            }
        }
    }
}