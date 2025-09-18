using System;
using System.Globalization;
using Avalonia.Data.Converters;
using Hermes.Domain.Models;

namespace Hermes.UI.Helpers;

public class EnumToStringConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is Enum enumValue)
        {
            return enumValue.ToString();
        }

        return string.Empty;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is string stringValue && targetType.IsEnum)
        {
            return Enum.Parse(targetType, stringValue);
        }

        return Avalonia.Data.BindingOperations.DoNothing;
    }
}