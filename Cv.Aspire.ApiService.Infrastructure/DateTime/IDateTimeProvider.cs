namespace Cv.Aspire.ApiService.Infrastructure;

public interface IDateTimeProvider
{
    public DateTime Now { get; }
    public DateTime Today { get; }
    public DateTime UtcNow { get; }
}