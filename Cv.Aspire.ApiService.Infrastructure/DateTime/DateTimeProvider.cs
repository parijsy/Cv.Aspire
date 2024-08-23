namespace Cv.Aspire.ApiService.Infrastructure;

/// <summary>
/// Wrapper klasse voor de Now, Today, en UtcNow getters van DateTime.
/// Bij het gebruik van deze wrapper kan er getests worden hoe de applicatie reageerd als er een specfieke datum terug komt via o.a. DateTime.Now
/// </summary>
public class DateTimeProvider : IDateTimeProvider
{
    public DateTime Now => DateTime.Now;
    public DateTime Today => DateTime.Today;
    public DateTime UtcNow => DateTime.UtcNow;
}