﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace Wine.Front.API.Models;

public partial class Capacity
{
    public int Id { get; set; }

    public string Capacity1 { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}