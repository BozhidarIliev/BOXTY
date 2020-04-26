namespace Boxty.Web.ViewModels
{
    using System.ComponentModel.DataAnnotations;

    public class AddTableItemCommentInputModel
    {
        [Required]
        public int TableId { get; set; }

        [Required]
        public int ItemIndex { get; set; }

        [Required]
        public string Comment { get; set; }
    }
}
