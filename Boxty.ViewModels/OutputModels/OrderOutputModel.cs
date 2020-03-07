using Boxty.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Boxty.ViewModels.OutputModels
{
    public class OrderOutputModel
    {
        public int Id { get; set; }
        public DateTime SentTime { get; set; }
        public int ProductId{ get; set; }
    }
}
