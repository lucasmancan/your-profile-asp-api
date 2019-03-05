using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace aspApi.Models
{
    public class AppImage
    {
        public string fileName { get; set; }
        public byte[] image { get; set; }
        public Storage storage { get; set; }
    }

   public enum Storage {
        ProfileStorage,CoverStorage
    }
}
