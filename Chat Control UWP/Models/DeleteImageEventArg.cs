using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat_Control_UWP.Models
{
    public class DeleteImageEventArg: EventArgs
    {
        public string ImageId { get; set; }
    }
}
