//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TheEndOfRestaurrant.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Item
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public string Pic { get; set; }
        public string Description { get; set; }
        public int Categories { get; set; }
        public int ID { get; set; }
    
        public virtual Category Category { get; set; }
    }
}