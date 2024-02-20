using MongoDB.Bson;

namespace BE.TradeeHub.PriceBookService.Domain.Entities;

/// <summary>
/// This is used to define the labour rate for a service and the quantity of labour required
/// It will join to the LabourRateEntity to get the cost and price of the labour and the quantity will be used to calculate the total cost and price
/// Example: Quantity is 15 and the labour rate price is £20 per sqm then the total price will be 15 * 20 = £300
/// If we want to give a better price for a larger quantity then we can use the Range property to define the range of the quantity and the ratio to apply to the price
/// The quantity will be used to calculate the total cost and price
/// The Ratio will be used to calculate the quantity based on the ratio of the service
/// The Range will be used to calculate the quantity based on the range of the service
/// </summary>
public class ServiceLabourEntity
{
    /// <summary>
    /// Labour rate id to map to the LabourRateEntity via graphql 
    /// </summary>
    public ObjectId LabourRateId { get; set; }
    
    /// <summary>
    /// The quantity of labour required for a certain service if it's a fixed service and not dynamic 
    /// </summary>
    public decimal? Quantity { get; set; }
    
    /// <summary>
    /// Good for when you want to use a dynamic Service and want to map the ratio to the quantity for the service
    /// Example: if the service is install a thermostat and the quantity is hours and it take 2h to install 1 which means if I have to do 3 then it will take 6h
    /// </summary>
    public decimal? Ratio { get; set; }
    
    /// <summary>
    /// Good for when you want to use a dynamic service and want to map the range to the quantity for the service
    /// to be able to set the quantity based on the range of the service
    /// Example: If service is to tile an area of 1-30sqm the the quantity of hours can be 15 while if they do an area of 31-60sqm then the quantity of hours can be 25 less than the 30sqm
    ///  </summary>
    public List<RangeTierUnitEntity>? Ranges { get; set; }
}