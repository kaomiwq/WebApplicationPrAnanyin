using System;
using System.Collections.Generic;

namespace WebApplicationPrAnanyin.Models;

public partial class Table
{
    public int Id { get; set; }

    public string Number { get; set; } = null!;

    public string Status { get; set; } = null!;

    public int Capacity { get; set; }

    public virtual ICollection<Reservation> Reservations { get; set; } = new List<Reservation>();
}
