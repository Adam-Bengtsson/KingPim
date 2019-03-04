using System;
using System.Collections.Generic;
using System.Text;

namespace KingPim.Models
{
    public class Media
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string FilePath { get; set; }
        public string AltText { get; set; }
        public bool MainImage { get; set; }
        public int ProductId { get; set; }
        public virtual Product Product { get; set; }
        public int MediaTypeId { get; set; }
        public virtual MediaType MediaType{ get; set; }
    }
}