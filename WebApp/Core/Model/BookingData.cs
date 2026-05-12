using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Model;
public class BookingData
{
    public string StudioId { get; set; } = string.Empty;
    public DateTime Date { get; set; } = DateTime.Today;
    public string StartTime { get; set; } = string.Empty;
    public string EndTime { get; set; } = string.Empty;
    public int Attendees { get; set; } = 1;
    public string? Notes { get; set; }
}
