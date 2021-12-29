using Microsoft.AspNetCore.Mvc;
using SupabaseExampleXA.Models;
using static Supabase.Client;

namespace KubakLandingApi.Controllers;

[ApiController]
[Route("[controller]")]
public class ViewCounterController : ControllerBase
{
    private readonly ILogger<ViewCounterController> _logger;
    Counter? counter;

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
            counter = data.Models.FirstOrDefault(new Counter { Id = 1, Count = 1, UpdatedAt = DateTime.Now });
        }
    }

    [HttpGet(Name = "GetViewCount")]
    public async Task<Counter> Get()
    {
        return await AddOneAndReturnNewCount();
    }

    async Task<Counter> AddOneAndReturnNewCount()
    {
        if (counter == null)
        {
            await UpdateCounter();
        }
        if (counter is Counter valueOfCount)
        {
            var k = await Instance.From<Counter>().Update(new Counter { Id = 1, Count = valueOfCount.Count + 1, UpdatedAt = DateTime.Now });
            return k.Models.FirstOrDefault(new Counter { Id = 1, Count = 1, UpdatedAt = DateTime.Now });
        }
        else
        {
            var k = await Instance.From<Counter>().Insert(new Counter { Id = 1, Count = 1, UpdatedAt = DateTime.Now });
            return new Counter { Id = 1, Count = 1, UpdatedAt = DateTime.Now };
        }
    }
}
