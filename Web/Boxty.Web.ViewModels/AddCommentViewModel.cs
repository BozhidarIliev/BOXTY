﻿namespace Boxty.Web.ViewModels
{
    using System.ComponentModel.DataAnnotations;

    public class AddCommentViewModel
    {
        [Required]
        public int ItemIndex { get; set; }

        [Required]
        public string Comment { get; set; }
    }
}
