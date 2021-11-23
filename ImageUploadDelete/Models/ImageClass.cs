using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Web;


namespace ImageUploadDelete.Models
{
    public class ImageClass
    {
        public FileInfo[] fileImage { get; set; }

        public string ErrorMessage { get; set; }
        public decimal filesize { get; set; }

        
    }
}
