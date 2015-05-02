using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web.Mvc;

namespace AOCMDB.Models.Nodes
{
    public class SoftwareOrFrameworkNode
    {
        /// <summary>
        /// Internal ApplicationID to AOCMDB
        /// </summary>
        [Key]
        [Required]
        public long SoftwareOrFrameworkId { get; set; }

        /// <summary>
        /// The name/verion of the software. Generally just Major version should be noted, 
        /// but if difference between minor versions is found to be important, then they should each
        /// be noted as a separate software or Framework.
        /// </summary>
        [Required]
        [Index(IsUnique = true)]
        [StringLength(450)] 
        [Display(Name = "Software Or Framework Name", Description = "This is the name of the Software Or Framework, such as the .Net 2.x, or Apache Web Server 1.x")]
        public string SoftwareOrFrameworkName { get; set; }

        /// <summary>
        /// The friendly name of the software
        /// </summary>
        [Display(Name = "Friendly Name", Description = "The friendly name of the Software")]
        public string FriendlyName { get; set; }

        /// <summary>
        /// This field gives a detailed description
        /// </summary>
        [AllowHtml]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Details", Description = "This field gives a detailed description of the Software Or Framework")]
        public string Details { get; set; }
    }
}