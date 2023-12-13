namespace config_api.Models.Data;

using System.ComponentModel.DataAnnotations;
using config_api.Entities;

public class CreateRequest
{
    [Required]
    public string? Key { get; set; }
    [Required]
    public string? Value { get; set; }
}