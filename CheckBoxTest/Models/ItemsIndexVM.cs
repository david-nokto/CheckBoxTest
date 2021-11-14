using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CheckBoxTest.Models
{
    public class ItemsIndexVM
    {
        public IEnumerable<Items> AllItems { get; set; }
    }
}