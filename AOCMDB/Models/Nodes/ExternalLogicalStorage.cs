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
    public class ExternalLogicalStorage
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
    }
}