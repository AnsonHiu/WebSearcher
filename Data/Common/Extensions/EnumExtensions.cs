using System.ComponentModel;
using System.Reflection;

namespace Data.Common.Extensions;

public static class EnumExtensions
{
    /// <summary>
    /// Reevaluate code for performance issues
    /// </summary>
    /// <returns>Description of Enum value, null if no description is found</returns>
    public static string? GetDescription(this Enum value) =>
        (value.GetType()
            .GetTypeInfo()
            .GetMember(value.ToString())
            .First(member => member.MemberType == MemberTypes.Field)
            .GetCustomAttributes(typeof(DescriptionAttribute), false)
            .SingleOrDefault() as DescriptionAttribute)?
        .Description;
}
