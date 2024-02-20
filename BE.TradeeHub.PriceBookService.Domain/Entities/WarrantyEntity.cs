﻿using HotChocolate.Types.Relay;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace BE.TradeeHub.PriceBookService.Domain.Entities;

/// <summary>
/// Warranty entity that could be applied to a service
/// </summary>
public class WarrantyEntity : AuditableEntity
{
    [ID] [BsonId] public ObjectId Id { get; set; }

    /// <summary>
    /// The services that the warranty can be applied to if empty then the warranty can be applied to all services
    /// </summary>
    public List<ObjectId>? Services { get; set; }

    /// <summary>
    /// The name of the warranty
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Warranty Type
    /// </summary>
    public string? WarrantyType { get; set; }

    /// <summary>
    /// Warranty Description
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Warranty Terms
    /// </summary>
    public required string Terms { get; set; }

    /// <summary>
    /// Warranty Duration
    /// </summary>
    public required WarrantyDurationEntity WarrantyDuration { get; set; }

    /// <summary>
    /// Warranty price
    /// </summary>
    public decimal? Price { get; set; }
}