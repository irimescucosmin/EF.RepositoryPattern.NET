using System;
using EF.RepositoryPattern.NET.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace EF.RepositoryPattern.NET.Entities;

public class CustomersEntity : IBaseEntity
{
    [PersonalData]
    public virtual Guid Id { get; set; } = Guid.NewGuid();

    /// <summary>
    /// Gets or sets the first name for this user.
    /// </summary>
    [ProtectedPersonalData]
    public virtual string? FirstName { get; set; }

    /// <summary>
    /// Gets or sets the last name for this user.
    /// </summary>
    [ProtectedPersonalData]
    public virtual string? LastName { get; set; }

    /// <summary>
    /// Gets or sets the email address for this user.
    /// </summary>
    [ProtectedPersonalData]
    public virtual string? Email { get; set; }
}