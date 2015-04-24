﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web.Mvc;

namespace AOCMDB.Models
{
    public class Application
    {
        /////////////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////////////
        //AOCMDB Details
        /////////////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////////////

        /// <summary>
        /// Internal ApplicationID to AOCMDB
        /// </summary>
        [Key]
        [Column(Order = 1)]
        [Required]
        public long ApplicationId { get; set; }

        /// <summary>
        /// Internal Revision Number of  the Application information
        /// </summary>
        [Key]
        [Column(Order = 2)]
        [Required]
        public long DatabaseRevision { get; set; }

        [Required]
        public string CreatedByUser { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }

        /////////////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////////////
        //User Details
        /////////////////////////////////////////////////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////////////////////////////////////////////////


        /// <summary>
        /// The Human Readable Application Name
        /// </summary>
        [Required]
        [Display(Name = "Application Name", Description = "Business name of the Application")]
        public string ApplicationName { get; set; }

        /// <summary>
        /// The business defined ApplicationID
        /// </summary>
        [Display(Name = "Global Application ID", Description = "The business defined ApplicationID")]
        public int GlobalApplicationID { get; set; }

        /// <summary>
        /// This would typically point to the application's about page, or a development team page for the application
        /// </summary>
        [Display(Name = "Site URL", Description = "This would typically point to the application's about page, or a development team page for the application")]
        public string SiteURL { get; set; }

        /// <summary>
        /// This field describes the network resources used by the application. Will be populated using a tinymce editor
        /// </summary>
        [AllowHtml]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Network Diagram or Inventory", Description = "This field describes the network resources used by the application")]
        public string NetworkDiagramOrInventory { get; set; }

        /// <summary>
        /// List of administrative tasks associated with the Application. Will be populated using a tinymce editor
        /// </summary>
        [AllowHtml]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Administrative Procedures", Description = "List of administrative tasks associated with the Application")]
        public string AdministrativeProcedures { get; set; }

        /// <summary>
        /// This is a list of general Contact Information. Will be populated using a tinymce editor
        /// </summary>
        [AllowHtml]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Contact Information", Description = "This is a list of general Contact Information")]
        public string ContactInformation { get; set; }

        /// <summary>
        /// This is a list of general Client Interface Information and Validation steps for Client Interface(s). Will be populated using a tinymce editor
        /// </summary>
        [AllowHtml]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Client Configuration and Validation", Description = "This is a list of general Client Interface Information and Validation steps for Client Interface(s)")]
        public string ClientConfigurationAndValidation { get; set; }

        /// <summary>
        /// This is a list of general Server Interface Information and validation steps for Server Interface(s). Will be populated using a tinymce editor
        /// </summary>
        [AllowHtml]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Server Configuration and Validation", Description = "This is a list of general Server Interface Information and validation steps for Server Interface(s)")]
        public string ServerConfigurationandValidation { get; set; }

        /// <summary>
        /// This is a list of validation steps for Server Interface(s). Will be populated using a tinymce editor
        /// </summary>
        [AllowHtml]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Recovery Procedures", Description = "This is a list of validation steps for Server Interface(s)")]
        public string RecoveryProcedures { get; set; }

        /// <summary>
        /// This variable stores a list of all known upstream dependencies.
        /// </summary>
        [Display(Name = "Upstream Application Dependencies", Description = "This is a list of all Upstream Application Dependencies")]
        public virtual ICollection<UpstreamApplicationDependency> UpstreamApplicationDependency { get; set; }


        /// <summary>
        /// This variable queries the database for any application that lists this as an upstream dependecy
        /// This is not set, it changes dynamically based on what applications refer to this as an upstream application
        /// </summary>
        [NotMapped]
        [Display(Name = "Downstream Application Dependencies", Description = "This is a list of all Downstream Application Dependencies")]
        public ICollection<Application> DownstreamApplicationDependency
        {
            get
            {
                throw new NotImplementedException();
                using (AOCMDBContext _dbContext = new AOCMDBContext())
                {
                    //Find all of the latest copies of applications
                    List<Application> LatestApplicationVersions = _dbContext.GetLatestApplicationVersions()//Latest Application Revisions
                        .ToList();
                        //Select all the latest application revisions which contain an upstream reference to this application                   
                    return LatestApplicationVersions;
                }
            }
        }


        public Application GenerateNewRevision()
        {
            throw new NotImplementedException();
            return new Application()
            {
                ApplicationId = this.ApplicationId,
                DatabaseRevision = this.DatabaseRevision+1,
                CreatedByUser = this.CreatedByUser,
                CreatedAt = DateTime.Now,
                ApplicationName = ApplicationName,
                GlobalApplicationID = this.GlobalApplicationID,
                SiteURL = this.SiteURL,
                NetworkDiagramOrInventory = this.NetworkDiagramOrInventory,
                AdministrativeProcedures = this.AdministrativeProcedures,
                ContactInformation = this.ContactInformation,
                ClientConfigurationAndValidation = this.ClientConfigurationAndValidation,
                ServerConfigurationandValidation = this.ServerConfigurationandValidation,
                RecoveryProcedures = this.RecoveryProcedures
            };
        }
        

    }
}