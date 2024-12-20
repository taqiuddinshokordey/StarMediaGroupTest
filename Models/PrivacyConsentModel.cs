namespace StarMediaGroupTest.Models;
public class PrivacyConsent
{
    public Guid Id { get; set; }
    public bool Accepted { get; set; }
    public DateTime Timestamp { get; set; }
    public int Version { get; set; }
}
