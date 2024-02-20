using MongoDB.Bson;
using MongoDB.Driver.Core.Misc;

namespace BE.TradeeHub.PriceBookService.Domain.Entities;

/// <summary>
/// All material required in a service with reference to the MaterialEntity to get the cost and price of the material
/// The quantity will be used to calculate the total cost and price
/// The Ratio will be used to calculate the quantity based on the ratio of the service
/// The Range will be used to calculate the quantity based on the range of the service
/// </summary>
public class ServiceMaterialEntity
{
    /// <summary>
    /// The material id to map to the MaterialEntity via graphql
    /// </summary>
    public ObjectId MaterialId { get; set; }

    /// <summary>
    /// The quantity of material required for the service
    /// </summary>
    public decimal? Quantity { get; set; }

    /// <summary>
    /// The ratio of material required for the given service
    /// Example: 3.5sqm of tiling will require 1 bag of adhesive so the ratio will be 3.5 and if the user wants 2 sqm then the quantity will be 2 * 3.5 will set quantity to 7
    /// </summary>
    public decimal? Ratio { get; set; }

    /// <summary>
    /// Good for when you want to use a dynamic service and want to map the range to the quantity for the service
    /// to be able to set the quantity based on the range of the service
    /// </summary>
    public List<RangeTierUnitEntity>? Ranges { get; set; }
}