﻿namespace FluentCMS.Entities;

public class Role : AuditEntity
{
    public required string Name { get; set; }
    public string? Description { get; set; }
    public required bool AutoAssigned { get; set; }

    public Guid SiteId { get; set; }
}
