﻿using System;
using System.Collections.Generic;

#nullable disable

namespace Display_Dashboard.Models
{
    public partial class TTranslation
    {
        public int FTranslationid { get; set; }
        public int FLabelid { get; set; }
        public string FLanguage { get; set; }
        public string FTranslation { get; set; }
    }
}
