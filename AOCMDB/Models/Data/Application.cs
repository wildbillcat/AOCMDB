using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web.Mvc;

namespace AOCMDB.Models.Data
{
    public class Application : Dependency
    {

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
        /// This field give a general description of the application. Will be populated using a tinymce editor
        /// </summary>
        [AllowHtml]
        [DataType(DataType.MultilineText)]
        [Display(Name = "Network Diagram or Inventory", Description = "This field describes the network resources used by the application")]
        public string Description { get; set; }

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

        [Display(Name = "Category")]
        public string Category { get; set; }

        [Display(Name = "Status")]
        public string Status { get; set; }

        [Display(Name = "Criticality")]
        public string Criticality { get; set; }

        [Display(Name = "Application Owner", Description = "This is the technical owner of an a list of validation steps for Server Interface(s)")]
        public string ApplicationOwner { get; set; }

        [Display(Name = "Business Owner", Description = "This is a list of validation steps for Server Interface(s)")]
        public string BusinessOwner { get; set; }

        [Display(Name = "Comments", Description = "This is a list of validation steps for Server Interface(s)")]
        public string Comments { get; set; }
    }
}