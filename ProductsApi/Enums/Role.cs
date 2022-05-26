using System.Runtime.Serialization;

namespace ProductsApi.Enums;

public enum Role
{
    [EnumMember(Value = "admin")]
    Admin,
    [EnumMember(Value = "user")]
    User
}