﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using System;
using System.Collections.Generic;

namespace WebApp.Models
{
    public partial class State
    {
        public State()
        {
            Country = new HashSet<Country>();
        }

        public int Id { get; set; }
        public string Uf { get; set; }

        public virtual ICollection<Country> Country { get; set; }
    }
}