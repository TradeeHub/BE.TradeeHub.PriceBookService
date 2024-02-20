using BE.TradeeHub.PriceBookService.Domain.Enums;
using MongoDB.Driver.Core.Misc;

namespace BE.TradeeHub.PriceBookService.Domain.Entities;

/// <summary>
/// Used to define the duration of a service
/// </summary>
public class ServiceDurationEntity
{
    /// <summary>
    /// The type of duration for the service
    /// Example: Minutes, Hours, Days, Weeks, Months
    /// </summary>
    public DurationType Type { get; set; }
    
    /// <summary>
    /// The exact duration of the service if there is no  DurationRangeTo
    /// If there is a DurationRangeTo then this will be the minimum duration
    /// </summary>
    public decimal DurationRangeFrom { get; set; }
    
    /// <summary>
    /// Duration of the service if there is a range
    /// </summary>
    public decimal? DurationRangeTo { get; set; }  
    
    /// <summary>
    /// Based on the service unit/quantity for example 1h for every sqm  1/1 ration or 2h for ever sqm 2/1 ratio
    /// </summary>
    public decimal? Ratio { get; set; }
    
    /// <summary>
    /// The duration range for to install a certain quantity of a service like I can do 1 install for a range of 0-2h but I could take 3 for a range of 3-4 units etc 
    /// </summary>
    public List<RangeTierUnitEntity>? Ranges { get; set; }
}