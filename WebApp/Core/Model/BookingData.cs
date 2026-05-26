using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Model;
public class BookingData
{
    public int StudioId { get; set; }
    //[BsonDateTimeOptions(Kind = DateTimeKind.Local)]
    [BsonDateTimeOptions(Kind = DateTimeKind.Unspecified)] // tells the MongoDb driver not to covert daytime to UTC
    public DateTime Date { get; set; } = DateTime.Today;
    public string StartTime { get; set; } = string.Empty;
    public string EndTime { get; set; } = string.Empty;
    public int Attendees { get; set; } = 1;
    public string? Notes { get; set; }
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public string StudioOwner { get; set; } = "";
    public string Status { get; set; } = "";
    public string StudioName { get; set; } = "";
    public string StudioAddress { get; set; } = "";
}
