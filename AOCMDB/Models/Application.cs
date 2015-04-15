using System;
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
        public int ApplicationId { get; set; }

        /// <summary>
        /// Internal Revision Number of  the Application information
        /// </summary>
        [Key]
        [Column(Order = 2)]
        [Required]
        public int DatabaseRevision { get; set; }

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
        public string ApplicationName { get; set; }

        /// <summary>
        /// The business defined ApplicationID
        /// </summary>
        public int GlobalApplicationID { get; set; }

        /// <summary>
        /// This would typically point to the application's about page, or a development team page for the application
        /// </summary>
        public string SiteURL { get; set; }

        /// <summary>
        /// This field describes the network resources used by the application. Will be populated using a tinymce editor
        /// </summary>
        [AllowHtml]
        public string NetworkDiagramOrInventory { get; set; }

        /// <summary>
        /// List of administrative tasks associated with the Application. Will be populated using a tinymce editor
        /// </summary>
        [AllowHtml]
        public string AdministrativeProcedures { get; set; }

        /// <summary>
        /// This is a list of general Contact Information. Will be populated using a tinymce editor
        /// </summary>
        [AllowHtml]
        public string ContactInformation { get; set; }

        /// <summary>
        /// This is a list of general Client Interface Information and Validation steps for Client Interface(s). Will be populated using a tinymce editor
        /// </summary>
        [AllowHtml]
        public string ClientConfigurationAndValidation { get; set; }

        /// <summary>
        /// This is a list of general Server Interface Information and validation steps for Server Interface(s). Will be populated using a tinymce editor
        /// </summary>
        [AllowHtml]
        public string ServerConfigurationandValidation { get; set; }

        /// <summary>
        /// This is a list of validation steps for Server Interface(s). Will be populated using a tinymce editor
        /// </summary>
        [AllowHtml]
        public string RecoveryProcedures { get; set; }

        /// <summary>
        /// Application Dependencies are tracked using just the Generic ApplicationID to prevent complications due to versioning. 
        /// This collection is referenced by the UpstreamApplicationDependency field to  create the Application collection returned.
        /// </summary>
        private ICollection<int> _UpstreamApplicationDependency;

        public ICollection<int> GetUpstreamApplicationDependency()
        {
            return _UpstreamApplicationDependency;
        }

        /// <summary>
        /// This variable stores a list of all known upstream dependencies
        /// </summary>
        [NotMapped]
        public virtual ICollection<Application> UpstreamApplicationDependency
        {
            get
            {
                using (AOCMDBContext _dbContext = new AOCMDBContext())
                {
                    //Find all of the latest copies of applications
                    List<Application> LatestApplicationVersions = _dbContext.Applications.GroupBy(p => p.ApplicationId)
                        .Select(group => group.Where(x => x.DatabaseRevision == group.Max(y => y.DatabaseRevision)).FirstOrDefault())//Latest Application Revisions
                        .Where(MostRecentApplicationRevison => _UpstreamApplicationDependency.Contains(MostRecentApplicationRevison.ApplicationId))
                        .ToList();//Select all the latest revisions of upstream applications
                    return LatestApplicationVersions;
                }
            }
            set
            {
                List<int> UpstreamApplicationIDs = new List<int>();
                foreach(Application UpstreamApplication in value)
                {
                    UpstreamApplicationIDs.Add(UpstreamApplication.ApplicationId);
                }
                _UpstreamApplicationDependency = UpstreamApplicationIDs;
            }
        }


        /// <summary>
        /// This variable queries the database for any application that lists this as an upstream dependecy
        /// This is not set, it changes dynamically based on what applications refer to this as an upstream application
        /// </summary>
        [NotMapped]
        public virtual ICollection<Application> DownstreamApplicationDependency
        {
            get
            {
                using (AOCMDBContext _dbContext = new AOCMDBContext())
                {
                    //Find all of the latest copies of applications
                    List<Application> LatestApplicationVersions = _dbContext.Applications.GroupBy(p => p.ApplicationId)
                        .Select(group => group.Where(x => x.DatabaseRevision == group.Max(y => y.DatabaseRevision)).FirstOrDefault())//Latest Application Revisions
                        .Where(MostRecentApplicationRevison => MostRecentApplicationRevison.UpstreamApplicationDependency.Contains(this))
                        .ToList();//Select all the latest application revisions which contain an upstream reference to this application                   
                    return LatestApplicationVersions;
                }
            }
        }


        public Application GenerateNewRevision()
        {
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
                RecoveryProcedures = this.RecoveryProcedures,
                _UpstreamApplicationDependency = this.GetUpstreamApplicationDependency()
            };
        }
        

    }
}