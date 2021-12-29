using Microsoft.AspNetCore.Mvc;
using SupabaseExampleXA.Models;
using static Supabase.Client;

namespace KubakLandingApi.Controllers;

[ApiController]
[Route("[controller]")]
public class ViewCounterController : ControllerBase
{
    private readonly ILogger<ViewCounterController> _logger;
    int? count;

    public ViewCounterController(ILogger<ViewCounterController> logger)
    {
        _logger = logger;
        _ = UpdateCounter();
    }

    private async Task UpdateCounter()
    {
        var data = await Instance.From<Counter>().Get();
        if (data.Models.Count > 0)
        {
            count = data.Models.FirstOrDefault(new Counter { Id = 1, Count = 1, UpdatedAt = DateTime.Now }).Count;
        }
    }

    [HttpGet(Name = "GetViewCount")]
    public async Task<int> Get()
    {
        return await AddOneAndReturnNewCount();
    }

    async Task<int> AddOneAndReturnNewCount()
    {
        if (count == null)
        {
            await UpdateCounter();
        }
        if (count is int valueOfCount)
        {
            var k = await Instance.From<Counter>().Update(new Counter { Id = 1, Count = valueOfCount + 1, UpdatedAt = DateTime.Now });
            return k.Models.FirstOrDefault(new Counter { Id = 1, Count = 1, UpdatedAt = DateTime.Now }).Count;
        }
        else
        {
            var k = await Instance.From<Counter>().Insert(new Counter { Id = 1, Count = 1, UpdatedAt = DateTime.Now });
            return 1;
        }
    }
}
