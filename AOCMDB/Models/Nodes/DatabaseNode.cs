using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web.Mvc;

namespace AOCMDB.Models.Nodes
{
    /// <summary>
    /// This refers to a DatabaseNode hosted on a server, such as a MS SQL Server, Oracle, or even MongoDB DatabaseNode.
    /// This should refer to the DatabaseNode itself, not the server which hosts it.
    /// </summary>
    public class DatabaseNode
    {
        /// <summary>
        /// Internal ApplicationID to AOCMDB
        /// </summary>
        [Key]
        [Required]
        public long DatabaseId { get; set; }

        /// <summary>
        /// The Database Name
        /// </summary>
        [Required]
        [Index(IsUnique = true)]
        [Display(Name = "DatabaseNode Name", Description = "DatabaseNode name of the Application")]
        public string DatabaseName { get; set; }

        /// <summary>
        /// The friendly name of the DatabaseNode
        /// </summary>
        [Display(Name = "Friendly Name", Description = "The friendly name of the DatabaseNode")]
        public string FriendlyName { get; set; }

        /// <summary>
        /// The type of DatabaseNode
        /// </summary>
        [Display(Name = "DatabaseNode Type", Description = "The type of managment software the database is hosted in. (Oracle, CassandraDB, etc.)")]
        public string DatabaseType { get; set; }
        //Allow for Config file to be set with Parameters

        /// <summary>
        /// This field gives a detailed description
        /// </summary>
        [AllowHtml]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Details", Description = "This field gives a detailed description of the database")]
        public string Details { get; set; }
    }
}