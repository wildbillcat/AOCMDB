using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AOCMDB.Models.Data
{
    public enum RepositoryType
    {
        TeamFoundationServer,
        Git
    }     
    public class SourceCodeRepository
    {
        [Key]
        [Required]
        public long Id { get; set; }

        [Required]
        public string RepositoryName { get; set; }

        [Required]
        public RepositoryType Type { get; set; }

        [Required]
        public string RepositoryUrl { get; set; }

        /// <summary>
        /// This will be set my a separate service that scans source code. 
        /// Should not be user editable.
        /// </summary>
        public string CodeFlower { get; set; }

        public DateTime CodeFlowerTimeStamp { get; set; }

    }
}