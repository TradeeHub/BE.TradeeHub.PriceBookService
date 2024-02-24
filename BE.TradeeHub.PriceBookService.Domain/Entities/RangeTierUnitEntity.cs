using BE.TradeeHub.PriceBookService.Domain.Interfaces.Requests;
using MongoDB.Driver.Core.Misc;

namespace BE.TradeeHub.PriceBookService.Domain.Entities;

public class RangeTierUnitEntity
{
    /// <summary>
    /// The quantity you will be using for the service of a certain range
    /// </summary>
    public decimal Quantity { get; set; }

    /// <summary>
    /// The range that will determine the above quantity
    /// Example: If service install thermostat 1 in the context of labour for
    /// example time based maybe it takes me 2h to install 1 thermostat but I can do 2 in 3h
    /// so the quantity is 3 in the context of hours and only charge for 3 hours while before I was charging for 4
    /// </summary>
    public Range<decimal> Range { get; set; }
    
    public RangeTierUnitEntity()
    {
    }
    
    public RangeTierUnitEntity (decimal quantity, Range<decimal> range)
    {
        Quantity = quantity;
        Range = range;
    }
}