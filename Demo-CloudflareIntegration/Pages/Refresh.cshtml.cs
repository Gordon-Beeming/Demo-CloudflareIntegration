using Demo_CloudflareIntegration.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Demo_CloudflareIntegration.Pages
{
  public class RefreshModel : PageModel
  {
    private readonly ILogger<RefreshModel> _logger;
    private readonly IConfiguration configRoot;

    public RefreshModel(ILogger<RefreshModel> logger, IConfiguration configRoot)
    {
      _logger = logger;
      this.configRoot = configRoot;
    }

    public async Task<IActionResult> OnGet()
    {
      await PurgeFiles(new List<string> { "https://cloudflareintegration.dahdah.xyz/" });
      return RedirectToPage("/Index");
    }

    private async Task<PurgeResponse> PurgeFiles(List<string> files)
    {
      var client = new WebClient();
      var zoneId = configRoot.GetValue<string>("Cloudflare:ZoneId");
      var endPoint = $"https://api.cloudflare.com/client/v4/zones/{zoneId}/purge_cache";
      var request = new PurgeRequest();
      request.files = files.Select(o => new PurgeRequest_File { url = o, }).ToArray();
      var requestJson = JsonConvert.SerializeObject(request);
      client.Headers.Add("X-Auth-Email", configRoot.GetValue<string>("Cloudflare:Email"));
      client.Headers.Add("X-Auth-Key", configRoot.GetValue<string>("Cloudflare:Key"));
      client.Headers[HttpRequestHeader.ContentType] = "application/json";
      var responseJson = await client.UploadStringTaskAsync(endPoint, requestJson);
      var response = JsonConvert.DeserializeObject<PurgeResponse>(responseJson);
      return response;
    }
  }
}
