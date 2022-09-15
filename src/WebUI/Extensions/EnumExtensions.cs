using System.Reflection;

namespace WBlog.WebUI.Extensions;

public static class EnumExtensions
{
    public static TAttribute? GetAttribute<TAttribute>(this Enum enumValue)
            where TAttribute : Attribute
    {
        var enumtype = enumValue.GetType();
        var enumMember = enumtype.GetMember(enumValue.ToString()).First();
        return enumMember.GetCustomAttribute<TAttribute>();
    }


}
