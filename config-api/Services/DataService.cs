namespace config_api.Services;

using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using AutoMapper;
using BCrypt.Net;
using config_api.Entities;
using config_api.Helpers;
using config_api.Models.Data;
using config_api.Repositories;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.VisualBasic;

public interface IDataService
{
    Task<List<JsonObject>> GetAll();
    Task<JsonObject> GetByKey(string key);
    Task Create(string key, JsonObject content);
    Task Update(string key, JsonObject content);
    Task Delete(string key);
    Task<List<object>> MatchFilter(string name, string value);
}

public class DataService : IDataService
{
    private IDataRepository _dataRepository;
    private readonly IMapper _mapper;

    public DataService(
        IDataRepository dataRepository,
        IMapper mapper)
    {
        _dataRepository = dataRepository;
        _mapper = mapper;
    }

    public async Task<List<JsonObject>> GetAll()
    {
        var data = await _dataRepository.GetAll();

        List<JsonObject> jsonObj = new List<JsonObject>();
        foreach (Data item in data)
        {
            JsonObject? obj = JsonSerializer.Deserialize<JsonObject>(item.Value);
            obj.Add("Key", item.Key);
            jsonObj.Add(obj);
        }
        return jsonObj;
    }


    public async Task<JsonObject> GetByKey(string key)
    {
        var data = await _dataRepository.GetByKey(key);
        if (data == null)
            throw new KeyNotFoundException("Data not found");
        JsonObject? obj = JsonSerializer.Deserialize<JsonObject>(data.Value);
        obj.Add("Key", data.Key);
        return obj;
    }

    public async Task Create(string key, JsonObject content)
    {
        if (string.IsNullOrEmpty(key))
            throw new Exception("Key can't be empty");
        CreateRequest createReq = new CreateRequest { Value = JsonSerializer.Serialize(content) };
        var data1 = _mapper.Map<Data>(createReq);
        data1.Key = key;
        await _dataRepository.Create(data1);
    }

    public async Task Update(string key, JsonObject content)
    {
        var data = await _dataRepository.GetByKey(key);
        UpdateRequest updateRequest = new UpdateRequest { Value = JsonSerializer.Serialize(content) };

        if (data == null)
            throw new KeyNotFoundException("Data not found");
        _mapper.Map(updateRequest, data);
        await _dataRepository.Update(data);
    }

    public async Task Delete(string key)
    {
        await _dataRepository.Delete(key);
    }
    public async Task<List<Object>> MatchFilter(string name, string value)
    {
        if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(value))
            throw new Exception("Name or value can't be empty");
        var data = await _dataRepository.MatchFilter(name, value);

        List<object> jsonObj = new List<object>();
        foreach (Data item in data)
        {
            object? obj = JsonSerializer.Deserialize<Object>(item.Value);
            jsonObj.Add(obj);
        }
        return jsonObj;
    }
}