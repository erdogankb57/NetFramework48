﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Inta.Framework.Admin.Models
{
    public class RecordSearch
    {
        public string Name { get; internal set; }
        public bool IsActive { get; internal set; }
        public int CategoryId { get; internal set; }
    }
}