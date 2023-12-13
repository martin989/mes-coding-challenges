namespace config_api.Models.Data;

using System.ComponentModel.DataAnnotations;
using config_api.Entities;

public class UpdateRequest
{
    public string? Key { get; set; }
    public string? Value { get; set; }
}