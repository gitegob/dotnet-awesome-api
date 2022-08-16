using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace Dotnet_API.Enums;

public enum EProductType
{
    PUBLISH,
    DRAFT
}
public enum EProductStatus
{
    SIMPLE,
    VARIABLE
}