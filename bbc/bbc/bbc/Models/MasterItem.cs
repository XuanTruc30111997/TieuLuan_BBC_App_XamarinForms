using System;
using System.Collections.Generic;
using System.Text;

namespace bbc.Models
{
    public class MasterItem
    {
        public string Title { get; set; }
        public string SourceImage { get; set; }

        public MasterItem(string title,string sourceImage)
        {
            this.Title = title;
            this.SourceImage = sourceImage;
        }
    }
}
