//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TranTienPhat_2122110302.DB
{
    using System;
    using System.Web;
    using System.Collections.Generic;
    
    public partial class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public Nullable<double> Price { get; set; }
        public Nullable<int> CategoryId { get; set; }
        public Nullable<int> TypeId { get; set; }
        public Nullable<int> BrandId { get; set; }
        public HttpPostedFileBase ImageUpload { get; set; }
    }
}
