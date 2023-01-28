using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GD.CMS.Common
{
    public class LayUITreeModel
    {
        public string ID { get; set; }

        public string PID { get; set; }

        public string Icons { get; set; }

        public string Title { get; set; }

        public string Field { get; set; }
        
        public string Href { get; set; }
        public bool Spread { get; set; }
        public bool Checked { get; set; }
        public bool Disabled { get; set; }

        public List<LayUITreeModel> Children { get; set; }
    }
}
