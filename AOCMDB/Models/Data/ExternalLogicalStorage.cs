using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web.Mvc;

namespace AOCMDB.Models.Data
{
    /// <summary>
    /// This would be a Network Share, NFS, Iscsi Lun, etc.
    /// </summary>
    public class ExternalLogicalStorage : Dependency
    {
        /// <summary>
        /// This denotes the storage types
        /// </summary>
        public string StorageType { get; set; }

    }
}