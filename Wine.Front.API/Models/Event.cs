﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace Wine.Front.API.Models;

public partial class Event
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Information { get; set; }

    public decimal Discount { get; set; }

    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }
}