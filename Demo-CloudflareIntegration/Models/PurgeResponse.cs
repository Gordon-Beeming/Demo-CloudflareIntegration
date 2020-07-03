namespace Demo_CloudflareIntegration.Models
{
  public class PurgeResponse
  {
    public PurgeResponse_Result result { get; set; }
    public bool success { get; set; }
    public object[] errors { get; set; }
    public object[] messages { get; set; }
  }

  public class PurgeResponse_Result
  {
    public string id { get; set; }
  }
}
