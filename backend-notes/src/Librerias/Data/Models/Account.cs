﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace backend_notes.Models;

public partial class Account
{
    public int Id { get; set; }

    public string User { get; set; }

    public string Password { get; set; }

    public virtual ICollection<Note> Notes { get; set; } = new List<Note>();
}