﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Boxty.Web.ViewModels
{
    public class AddCommentViewModel
    {
        [Required]
        public int ItemIndex { get; set; }

        [Required]
        public string Comment { get; set; }
    }
}
