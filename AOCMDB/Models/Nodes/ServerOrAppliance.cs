using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web.Mvc;

namespace AOCMDB.Models.Nodes
{
    /// <summary>
    /// This may represent a physical or virtual server/appliance, such as a windows server, Mainframe, NAS, SAN, or vendor appliance (TPAM, Nvidia Grid).
    /// Generally this should refer to a scoped entity and not a platform, such as Azure Cloud, or VMAX, but use your best judgment. In the example of
    /// Nvidia grid, this object would represent a single physical box (ie. Appliance Box 01), not the collective which would be summarized as a technology dependency. 
    /// </summary>
    public class ServerOrAppliance
    {
        /// <summary>
        /// Internal ApplicationID to AOCMDB
        /// </summary>
        [Key]
        [Required]
        public long ServerOrApplianceId { get; set; }

        /// <summary>
        /// The Name of the logical storage component
        /// </summary>
        [Required]
        [Display(Name = "Server or Appliance Name", Description = "Database name of the Server/Appliance")]
        public string DatabaseName { get; set; }

        /// <summary>
        /// The Human Readable Name/nickname
        /// </summary>
        [Display(Name = "Friendly Name", Description = "The friendly name of the Server/Applicance")]
        public string FriendlyName { get; set; }

        /// <summary>
        /// This field gives a detailed description
        /// </summary>
        [AllowHtml]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Details", Description = "This field gives a detailed description of the Server/Appliance")]
        public string Details { get; set; }
    }
}