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

        public string CodeFlower { get; set; }

    }
}