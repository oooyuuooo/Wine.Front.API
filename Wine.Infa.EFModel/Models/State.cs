﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace Wine.Infa.EFModel.Models;

public partial class State
{
    public int Id { get; set; }

    public string StateName { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}