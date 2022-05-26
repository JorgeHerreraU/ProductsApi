using System.Runtime.Serialization;

namespace ProductsApi.Enums;

public enum Priority
{
    [EnumMember(Value = "low")]
    Low,
    [EnumMember(Value = "medium")]
    Medium,
    [EnumMember(Value = "high")]
    High
}