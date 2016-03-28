using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace MeCommerce.ViewModels
{
    public class ProductViewModel
    {
        public string Name { get; set; }

        [DisplayName("SKU")]
        public string Sku { get; set; }

        [DisplayName("Short Description")]
        public string ShortDescription { get; set; }

        [DisplayName("Long Description")]
        public string LongDescription { get; set; }

        public int Price { get; set; }
    }
}