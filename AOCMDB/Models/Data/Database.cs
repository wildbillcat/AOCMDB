using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web.Mvc;

namespace AOCMDB.Models.Data
{
    /// <summary>
    /// This refers to a DatabaseNode hosted on a server, such as a MS SQL Server, Oracle, or even MongoDB DatabaseNode.
    /// This should refer to the DatabaseNode itself, not the server which hosts it.
    /// </summary>
    public class Database : Dependency
    {        
        /// <summary>
        /// The type of DatabaseNode
        /// </summary>
        [Display(Name = "DatabaseNode Type", Description = "The type of managment software the database is hosted in. (Oracle, CassandraDB, etc.)")]
        public string DatabaseType { get; set; }
        //Allow for Config file to be set with Parameters

    }
}