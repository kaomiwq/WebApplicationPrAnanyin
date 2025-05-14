using System;
using System.Collections.Generic;

namespace WebApplicationPrAnanyin.Models;

public partial class Reservation
{
    public int Id { get; set; }

    public int TableId { get; set; }

    public int UserId { get; set; }

    public DateTime DateTime { get; set; }

    public string Status { get; set; } = null!;

    public virtual Table Table { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
