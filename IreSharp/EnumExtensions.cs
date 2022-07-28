﻿using System.Reflection;

namespace IreSharp;

internal static class EnumExtensions {

    public static T GetAttribute<T>(this Enum value) where T : Attribute {
        Type type = value.GetType();
        MemberInfo[] memberInfo = type.GetMember(value.ToString());
        object[] attributes = memberInfo[0].GetCustomAttributes(typeof(T), false);

        if (attributes.Length == 0)
            throw new InvalidOperationException();

        return (T)attributes[0];
    }

}
