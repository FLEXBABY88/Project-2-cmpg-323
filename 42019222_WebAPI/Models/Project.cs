using System;
using System.Collections.Generic;

namespace _42019222_WebAPI.Models;

public partial class Project
{
    public Guid ProjectId { get; set; }

    public string? ProjectName { get; set; }

    public string? ProjectDescription { get; set; }

    public DateTime? ProjectCreationDate { get; set; }

    public string? ProjectStatus { get; set; }

    public Guid? ClientId { get; set; }
    // Navigation property to JobTelemetry
    public ICollection<JobTelemetry> JobTelemetries { get; set; }

}
