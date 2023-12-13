namespace confi_api.Controllers;

using System.Text.Json;
using System.Text.Json.Nodes;
using config_api.Models.Data;
using config_api.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileSystemGlobbing.Internal.Patterns;

[ApiController]
[Route("[controller]")]
public class DataController : ControllerBase
{
    private IDataService _dataService;

    public DataController(IDataService dataService)
    {
        _dataService = dataService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var data = await _dataService.GetAll();
        return Ok(data);
    }

    [HttpGet("GetByKey/{key}")]
    public async Task<IActionResult> GetByKey(string key)
    {
        var data = await _dataService.GetByKey(key);
        return Ok(data);
    }

    [HttpPost("Create/{key}")]
    public async Task<IActionResult> Create(string key, [FromBody] JsonObject content)
    {
        await _dataService.Create(key, content);
        return Ok(new { message = "Data created" });
    }

    [HttpPost("Update/{key}")]
    public async Task<IActionResult> Update(string key, [FromBody] JsonObject content)
    {
        await _dataService.Update(key, content);
        return Ok(new { message = "Data updated" });
    }

    [HttpDelete("Delete/{key}")]
    public async Task<IActionResult> Delete(string key)
    {
        await _dataService.Delete(key);
        return Ok(new { message = "Data deleted" });
    }

    [HttpPost("MatchFilter/{name}/{value}")]
    public async Task<IActionResult> MatchFilter(string name, string value)
    {
        var data = await _dataService.MatchFilter(name, value);
        return Ok(data);
    }
}