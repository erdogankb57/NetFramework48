﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inta.Framework.Entity
{
    [Table("GeneralSettings")]
    public class GeneralSettings
    {
        public GeneralSettings()
        {

        }

        public int Id { get; set; }
        public int? LanguageId { get; set; }
        public int? SystemUserId { get; set; }
        public string EmailIpAdress { get; set; }
        public string EmailAdress { get; set; }
        public int EmailPort { get; set; }
        public string EmailPassword { get; set; }
        public string DomainName { get; set; }
        public string CdnUrl { get; set; }
        public string ImageUploadPath { get; set; }
        public string FileUploadPath { get; set; }
        public string DeveloperName { get; set; }
        public string DeveloperEmail { get; set; }
        public int CategoryImageSmallWidth { get; set; }
        public int CategoryImageSmallHeight { get; set; }
        public int CategoryImageBigWidth { get; set; }
        public int CategoryImageBigHeight { get; set; }
        public int ContentImageSmallWidth { get; set; }
        public int ContentImageSmallHeight { get; set; }
        public int ContentImageBigWidth { get; set; }
        public int ContentImageBigHeight { get; set; }

    }
}