﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

namespace WebApp.Models
{
    public partial class Country
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int StateId { get; set; }

        public virtual State State { get; set; }
    }
}