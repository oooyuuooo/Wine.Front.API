﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace Wine.Infa.EFModel.Models;

public partial class Product
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Year { get; set; }

    public int CatId { get; set; }

    public int OrigId { get; set; }

    public int CapId { get; set; }

    public int TastesId { get; set; }

    public decimal Price { get; set; }

    public string ImageLink { get; set; }

    public int Count { get; set; }

    public virtual Capacity Cap { get; set; }

    public virtual Category Cat { get; set; }

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual Origin Orig { get; set; }

    public virtual Taste Tastes { get; set; }
}