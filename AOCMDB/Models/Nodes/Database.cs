using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web.Mvc;

namespace AOCMDB.Models.Nodes
{
    /// <summary>
    /// This refers to a Database hosted on a server, such as a MS SQL Server, Oracle, or even MongoDB Database.
    /// This should refer to the Database itself, not the server which hosts it.
    /// </summary>
    public class Database
    {
        /// <summary>
        /// Internal ApplicationID to AOCMDB
        /// </summary>
        [Key]
        [Required]
        public long DatabaseId { get; set; }

        /// <summary>
        /// The Human Readable Application Name
        /// </summary>
        [Required]
        [Display(Name = "Database Name", Description = "Database name of the Application")]
        public string DatabaseName { get; set; }

        /// <summary>
        /// The type of Database
        /// </summary>
        [Display(Name = "Friendly Name", Description = "The friendly name of the Database")]
        public string FriendlyName { get; set; }

        /// <summary>
        /// The type of Database
        /// </summary>
        [Display(Name = "Database Type", Description = "The type of managment software the database is hosted in. (Oracle, CassandraDB, etc.)")]
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