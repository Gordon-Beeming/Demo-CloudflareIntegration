namespace Demo_CloudflareIntegration.Models
{
  public class PurgeRequest
  {
    public PurgeRequest_File[] files { get; set; }
  }

  public class PurgeRequest_File
  {
    public string url { get; set; }
  }
}
