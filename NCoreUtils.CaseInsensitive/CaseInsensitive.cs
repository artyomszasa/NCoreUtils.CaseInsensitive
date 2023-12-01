using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;

namespace NCoreUtils;

/// <summary>
/// Represents case-insensitive string.
/// </summary>
/// <remarks>
/// Constructs new case-insensitive string.
/// </remarks>
/// <param name="value">Case-sensitive string value.</param>
[method: MethodImpl(MethodImplOptions.AggressiveInlining)]
[method: DebuggerStepThrough]
public readonly struct CaseInsensitive(string value) : IComparable, IConvertible, IEquatable<CaseInsensitive>, IComparable<CaseInsensitive>, IEnumerable<char>
{
    private static readonly StringComparer _cmp = StringComparer.OrdinalIgnoreCase;

    private static readonly Dictionary<Type, Func<CaseInsensitive, IFormatProvider?, object>> _toType = [];

    /// <summary>
    /// Empty case-insensitive string.
    /// </summary>
    public static readonly CaseInsensitive Empty = Create(string.Empty);

    static CaseInsensitive()
    {
        _toType.Add(typeof(bool), (ci, provider) => ((IConvertible)ci).ToBoolean(provider));
        _toType.Add(typeof(sbyte), (ci, provider) => ((IConvertible)ci).ToSByte(provider));
        _toType.Add(typeof(byte), (ci, provider) => ((IConvertible)ci).ToByte(provider));
        _toType.Add(typeof(short), (ci, provider) => ((IConvertible)ci).ToInt16(provider));
        _toType.Add(typeof(ushort), (ci, provider) => ((IConvertible)ci).ToUInt16(provider));
        _toType.Add(typeof(int), (ci, provider) => ((IConvertible)ci).ToInt32(provider));
        _toType.Add(typeof(uint), (ci, provider) => ((IConvertible)ci).ToUInt32(provider));
        _toType.Add(typeof(long), (ci, provider) => ((IConvertible)ci).ToInt64(provider));
        _toType.Add(typeof(ulong), (ci, provider) => ((IConvertible)ci).ToUInt64(provider));
        _toType.Add(typeof(float), (ci, provider) => ((IConvertible)ci).ToSingle(provider));
        _toType.Add(typeof(double), (ci, provider) => ((IConvertible)ci).ToDouble(provider));
        _toType.Add(typeof(decimal), (ci, provider) => ((IConvertible)ci).ToDecimal(provider));
        _toType.Add(typeof(DateTime), (ci, provider) => ((IConvertible)ci).ToDateTime(provider));
        _toType.Add(typeof(string), (ci, provider) => ((IConvertible)ci).ToString(provider));
    }

    /// <summary>
    /// Preferred way to create case-insensitive strings.
    /// </summary>
    /// <param name="value">Underlying case-sensitive value.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [DebuggerStepThrough]
    public static CaseInsensitive Create(string value) => new(value ?? string.Empty);

    /// <summary>
    /// Performes implicit conversion.
    /// </summary>
    /// <param name="value">Case-sensitive string.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [DebuggerStepThrough]
    public static implicit operator CaseInsensitive(string value) => new(value);

    /// <summary>
    /// Determines whether the first instance of <see cref="CaseInsensitive"/> is
    /// equal to the second instance of <see cref="CaseInsensitive"/>. Two
    /// instances are considered equal if underlying values of the instances are equal with
    /// no respect for case-sensitivity or culture.
    /// </summary>
    /// <param name="a">
    /// Instance of <see cref="CaseInsensitive"/> to be compared against.
    /// </param>
    /// <param name="b">
    /// Instance of <see cref="CaseInsensitive"/> to be compared against the first one.
    /// </param>
    /// <returns>
    /// <c>true</c> if the instances are equal; <c>false</c> otherwise.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [DebuggerStepThrough]
    public static bool operator ==(CaseInsensitive a, CaseInsensitive b) => a.Equals(b);

    /// <summary>
    /// Determines whether the first instance of <see cref="CaseInsensitive"/> is
    /// inequal to the second instance of <see cref="CaseInsensitive"/>. Two
    /// instances are considered equal if underlying values of the instances are equal with
    /// no respect for case-sensitivity or culture.
    /// </summary>
    /// <param name="a">
    /// Instance of <see cref="CaseInsensitive"/> to be compared against.
    /// </param>
    /// <param name="b">
    /// Instance of <see cref="CaseInsensitive"/> to be compared against the first one.
    /// </param>
    /// <returns>
    /// <c>false</c> if the instances are equal; <c>true</c> otherwise.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [DebuggerStepThrough]
    public static bool operator !=(CaseInsensitive a, CaseInsensitive b) => !a.Equals(b);

    /// <summary>
    /// Determines whether the first instance of <see cref="CaseInsensitive"/> is
    /// less than the second instance of <see cref="CaseInsensitive"/>.
    /// </summary>
    /// <param name="a">
    /// Instance of <see cref="CaseInsensitive"/> to be compared against.
    /// </param>
    /// <param name="b">
    /// Instance of <see cref="CaseInsensitive"/> to be compared against the first one.
    /// </param>
    /// <returns>
    /// <c>1</c> if the specfied instances are greater than the actual one; <c>-1</c> if less; <c>0</c> otherwise.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [DebuggerStepThrough]
    public static bool operator <(CaseInsensitive a, CaseInsensitive b) => 0 < a.CompareTo(b);

    /// <summary>
    /// Determines whether the first instance of <see cref="CaseInsensitive"/> is
    /// greater than the second instance of <see cref="CaseInsensitive"/>.
    /// </summary>
    /// <param name="a">
    /// Instance of <see cref="CaseInsensitive"/> to be compared against.
    /// </param>
    /// <param name="b">
    /// Instance of <see cref="CaseInsensitive"/> to be compared against the first one.
    /// </param>
    /// <returns>
    /// <c>1</c> if the specfied instances are greater than the actual one; <c>-1</c> if less; <c>0</c> otherwise.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [DebuggerStepThrough]
    public static bool operator >(CaseInsensitive a, CaseInsensitive b) => 0 > a.CompareTo(b);

    /// <summary>
    /// Determines whether the first instance of <see cref="CaseInsensitive"/> is
    /// less or equal to the second instance of <see cref="CaseInsensitive"/>.
    /// </summary>
    /// <param name="a">
    /// Instance of <see cref="CaseInsensitive"/> to be compared against.
    /// </param>
    /// <param name="b">
    /// Instance of <see cref="CaseInsensitive"/> to be compared against the first one.
    /// </param>
    /// <returns>
    /// <c>1</c> if the specfied instances are greater than the actual one; <c>-1</c> if less; <c>0</c> otherwise.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [DebuggerStepThrough]
    public static bool operator <=(CaseInsensitive a, CaseInsensitive b) => 0 <= a.CompareTo(b);

    /// <summary>
    /// Determines whether the first instance of <see cref="CaseInsensitive"/> is
    /// greater or equal to the second instance of <see cref="CaseInsensitive"/>.
    /// </summary>
    /// <param name="a">
    /// Instance of <see cref="CaseInsensitive"/> to be compared against.
    /// </param>
    /// <param name="b">
    /// Instance of <see cref="CaseInsensitive"/> to be compared against the first one.
    /// </param>
    /// <returns>
    /// <c>1</c> if the specfied instances are greater than the actual one; <c>-1</c> if less; <c>0</c> otherwise.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [DebuggerStepThrough]
    public static bool operator >=(CaseInsensitive a, CaseInsensitive b) => 0 >= a.CompareTo(b);

    /// <summary>
    /// Concatenates the members of a constructed <see cref="IEnumerable{T}" />
    /// collection of type <see cref="string" />, using the specified separator between each member.
    /// </summary>
    /// <param name="separator">
    /// The string to use as a separator. <paramref name="separator" /> is included in the returned string only if
    /// <paramref name="values" /> has more than one element.
    /// </param>
    /// <param name="values">A collection that contains the strings to concatenate.</param>
    /// <returns>
    /// A string that consists of the members of <paramref name="values" /> delimited by the
    /// <paramref name="separator" /> string. If <paramref name="values" /> has no members, the method returns
    /// <see cref="M:CaseInsensitive.Empty" />.
    /// </returns>
    public static CaseInsensitive Join(CaseInsensitive separator, IEnumerable<CaseInsensitive> values)
        => Create(string.Join(separator.Value, values.Select(v => v.Value)));

    /// Read-only case-sensitive value;
    private readonly string _value = value;

    /// <summary>
    /// Gets underlying case-sensitive string. Ensures non-null value returned.
    /// </summary>
    public string Value
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [DebuggerStepThrough]
        get => _value ?? string.Empty;
    }

    /// <summary>
    /// Gets the number of characters in the current instance.
    /// </summary>
    public int Length
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        [DebuggerStepThrough]
        get => Value.Length;
    }

    /// <summary>
    /// Gets the <see cref="System.Char"/> object at a specified position in the current
    /// <see cref="CaseInsensitive"/> object.
    /// <para>
    /// Note: Original value is returned. No case conversion is performed.
    /// </para>
    /// </summary>
    [IndexerName("Chars")]
    public char this[int index]
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => Value[index];
    }

    IEnumerator<char> IEnumerable<char>.GetEnumerator() => GetEnumerator();

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    /// <summary>
    /// Returns a value indicating whether a specified case-insensitive substring occurs within this case-insensitive
    /// string.
    /// </summary>
    public bool Contains(CaseInsensitive value) => -1 != Value.IndexOf(value.Value, StringComparison.OrdinalIgnoreCase);

    /// <summary>
    /// Determines whether the actual instance of <see cref="CaseInsensitive"/> is
    /// equal to the specified instance of <see cref="CaseInsensitive"/>. Two
    /// instances are considered equal if underlying values of the instances are equal with
    /// no respect for case-sensitivity or culture.
    /// </summary>
    /// <param name="other">
    /// Instance of <see cref="CaseInsensitive"/> to be compared against the actual one.
    /// </param>
    /// <returns>
    /// <c>true</c> if the instances are equal; <c>false</c> otherwise.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [DebuggerStepThrough]
    public bool Equals(CaseInsensitive other) => _cmp.Equals(Value, other.Value);

    /// <summary>
    /// Determines whether the actual instance of <see cref="CaseInsensitive"/> is
    /// equal to the specified object. Two instances are considered equal if underlying values of
    /// the instances are equal with no respect for case-sensitivity or culture. If the specified
    /// object is not an instance of <see cref="CaseInsensitive"/> the result of
    /// the comparison is always <c>false</c>.
    /// </summary>
    /// <param name="obj">
    /// Object to be compared against the actual one.
    /// </param>
    /// <returns>
    /// <c>true</c> if the instances are equal; <c>false</c> otherwise.
    /// </returns>
    [DebuggerStepThrough]
    public override bool Equals(object? obj) => obj is CaseInsensitive other && Equals(other);

    /// <summary>
    /// Provides hasing function for the actual instance of <see cref="CaseInsensitive"/>.
    /// Hash is computed  with no respect for case-sensitivity or culture.
    /// </summary>
    [DebuggerStepThrough]
    public override int GetHashCode() => _cmp.GetHashCode(Value);

    /// <summary>
    /// Compares the actual instance of <see cref="CaseInsensitive"/> to
    /// the specified instance of <see cref="CaseInsensitive"/>. Compares the underlying
    /// values of the instances with no respect for case-sensitivity or culture.
    /// </summary>
    /// <param name="other">
    /// Instance of <see cref="CaseInsensitive"/> to be compared against the actual one.
    /// </param>
    /// <returns>
    /// <c>1</c> if the specfied instances are greater than the actual one; <c>-1</c> if less; <c>0</c> otherwise.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [DebuggerStepThrough]
    public int CompareTo(CaseInsensitive other) => _cmp.Compare(Value, other.Value);

    /// <summary>
    /// Compares the actual instance of <see cref="CaseInsensitive"/> to
    /// the specified object. Compares the underlying values of the instances with no respect
    /// for case-sensitivity or culture. Throws if the specified object is not an instance of
    /// <see cref="CaseInsensitive"/>.
    /// </summary>
    /// <param name="obj">
    /// Object to be compared against the actual one.
    /// </param>
    /// <returns>
    /// <c>1</c> if the specfied instances are greater than the actual one; <c>-1</c> if less; <c>0</c> otherwise.
    /// </returns>
    /// <exception cref="InvalidOperationException">
    /// Thrown if the specified object is not an instance of <see cref="CaseInsensitive"/>.
    /// </exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [DebuggerStepThrough]
    public int CompareTo(object? obj)
    {
        if (obj is CaseInsensitive other)
        {
            return CompareTo(other);
        }
        throw new InvalidOperationException("Uncomparable.");
    }

    /// <summary>
    /// Concatenates the members of a constructed <see cref="IEnumerable{T}"/> collection
    /// of type <see cref="CaseInsensitive"/>.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [DebuggerStepThrough]
    public static CaseInsensitive Concat(IEnumerable<CaseInsensitive> values) => Create(string.Concat(values.Select(v => v.Value)));

    /// <summary>
    /// Determines whether the end of this string instance matches the specified string.
    /// </summary>
    /// <param name="value">The string to compare.</param>
    /// <returns>
    /// <c>true</c> if <paramref name="value">value</paramref> matches the end of this string; otherwise,
    /// <c>false</c>.
    /// </returns>
    public bool EndsWith(CaseInsensitive value) => Value.EndsWith(value.Value, StringComparison.OrdinalIgnoreCase);

    /// <summary>
    /// Retrieves an object that can iterate through the individual characters in this string.
    /// </summary>
    /// <returns>An enumerator object.</returns>
    public CharEnumerator GetEnumerator() => Value.GetEnumerator();

    /// <summary>
    /// Reports the zero-based index of the first occurrence of the specified Unicode character in this string.
    /// Comparison performed in case-insensitive manner.
    /// </summary>
    /// <param name="value">A Unicode character to seek.</param>
    /// <returns>The zero-based index position of value if that character is found, or  <c>-1</c> if it is not.</returns>
    public int IndexOf(char value)
    {
        // Idea: https://github.com/dotnet/coreclr/blob/master/src/mscorlib/src/System/String.Comparison.cs
        var i = 0;
        var uvalue = (uint)(value - 'a') <= 'z' - 'a' ? (uint)value - 0x20 : value;
        foreach (var ch in Value)
        {
            var uch = (uint)(ch - 'a') <= 'z' - 'a' ? (uint)ch - 0x20 : ch;
            if (uch == uvalue)
            {
                return i;
            }
            ++i;
        }
        return -1;
    }

    /// <summary>
    /// Determines whether underlying value of the instance is empty.
    /// </summary>
    public bool IsEmpty() => string.IsNullOrEmpty(_value);

    /// <summary>
    /// Determines whether underlying value of the instance is empty or contains only whitespaces.
    /// </summary>
    public bool IsEmptyOrWhiteSpace() => string.IsNullOrWhiteSpace(_value);

    /// <summary>
    /// Determines whether the beginning of this string instance matches the specified string.
    /// </summary>
    /// <param name="value">The string to compare.</param>
    /// <returns>
    /// <c>true</c> if <paramref name="value">value</paramref> matches the beginning of this string; otherwise,
    /// <c>false</c>.
    /// </returns>
    public bool StartsWith(CaseInsensitive value) => Value.StartsWith(value.Value, StringComparison.OrdinalIgnoreCase);

    /// <summary>
    /// Retrieves a substring from this instance. The substring starts at a specified character position and continues
    /// to the end of the string.
    /// </summary>
    /// <param name="startIndex">The zero-based starting character position of a substring in this instance.</param>
    /// <returns>
    /// A string that is equivalent to the substring that begins at <paramref name="startIndex" /> in this instance,
    /// or <c>Empty</c> if <paramref name="startIndex" /> is equal to the length of this instance.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [DebuggerStepThrough]
    public CaseInsensitive Substring(int startIndex)
#if NETFRAMEWORK
        => Create(Value.Substring(startIndex));
#else
        => Create(Value[startIndex..]);
#endif

    /// <summary>
    /// Retrieves a substring from this instance. The substring starts at a specified character position and has a
    /// specified length.
    /// </summary>
    /// <param name="startIndex">The zero-based starting character position of a substring in this instance.</param>
    /// <param name="length">The number of characters in the substring.</param>
    /// <returns>
    /// A string that is equivalent to the substring of length length that begins at <paramref name="startIndex" /> in
    /// this instance, or <c>Empty</c> if <paramref name="startIndex" /> is equal to the length of this instance and
    /// length is zero.
    /// </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [DebuggerStepThrough]
    public CaseInsensitive Substring(int startIndex, int length) => Create(Value.Substring(startIndex, length));

    /// <summary>
    /// Returns the lowercased underlying string.
    /// </summary>
    /// <param name="cultureInfo">Culture info to use.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [DebuggerStepThrough]
    public string ToLowerString(CultureInfo? cultureInfo) => (cultureInfo ?? CultureInfo.InvariantCulture).TextInfo.ToLower(Value);

    /// <summary>
    /// Returns the uppercased underlying string.
    /// </summary>
    /// <param name="cultureInfo">Culture info to use.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [DebuggerStepThrough]
    public string ToUpperString(CultureInfo cultureInfo) => (cultureInfo ?? CultureInfo.InvariantCulture).TextInfo.ToUpper(Value);

    /// <summary>
    /// Returns the lowercased underlying string (invariant culture).
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [DebuggerStepThrough]
    public string ToLowerString() => ToLowerString(CultureInfo.InvariantCulture);

    /// <summary>
    /// Returns the uppercased underlying string (invariant culture).
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    [DebuggerStepThrough]
    public string ToUpperString() => ToUpperString(CultureInfo.InvariantCulture);

    /// <summary>
    /// Customized ToString method.
    /// </summary>
    [DebuggerStepThrough]
    public override string ToString() => ToLowerString();

    #region IConvertible

    /// <summary>
    /// Returns object type code.
    /// </summary>
    /// <returns>Object type code.</returns>
    TypeCode IConvertible.GetTypeCode() => TypeCode.Object;

    /// <summary>
    /// Converts actual instance to boolean if possible.
    /// </summary>
    /// <param name="provider">Format provider to use.</param>
    /// <returns>Converted boolean value.</returns>
    bool IConvertible.ToBoolean(IFormatProvider? provider)
    {
        if (!bool.TryParse(Value, out bool result))
        {
            throw new InvalidCastException($"\"{this}\" cannot be converted to bool.");
        }
        return result;
    }

    /// <summary>
    /// Converts actual instance to char if possible.
    /// </summary>
    /// <param name="provider">Format provider to use.</param>
    /// <returns>Converted char value.</returns>
    char IConvertible.ToChar(IFormatProvider? provider)
    {
        if (1 == Value.Length)
        {
            return char.ToLowerInvariant(Value[0]);
        }
        throw new InvalidCastException($"\"{this}\" cannot be converted to char");
    }

    sbyte IConvertible.ToSByte(IFormatProvider? provider)
    {
        if (!sbyte.TryParse(Value, NumberStyles.Integer, provider, out sbyte result))
        {
            throw new InvalidCastException($"\"{this}\" cannot be converted to sbyte");
        }
        return result;
    }

    byte IConvertible.ToByte(IFormatProvider? provider)
    {
        if (!byte.TryParse(Value, NumberStyles.Integer, provider, out byte result))
        {
            throw new InvalidCastException($"\"{this}\" cannot be converted to byte");
        }
        return result;
    }

    short IConvertible.ToInt16(IFormatProvider? provider)
    {
        if (!short.TryParse(Value, NumberStyles.Integer, provider, out short result))
        {
            throw new InvalidCastException($"\"{this}\" cannot be converted to short");
        }
        return result;
    }

    ushort IConvertible.ToUInt16(IFormatProvider? provider)
    {
        if (!ushort.TryParse(Value, NumberStyles.Integer, provider, out ushort result))
        {
            throw new InvalidCastException($"\"{this}\" cannot be converted to ushort");
        }
        return result;
    }

    int IConvertible.ToInt32(IFormatProvider? provider)
    {
        if (!int.TryParse(Value, NumberStyles.Integer, provider, out int result))
        {
            throw new InvalidCastException($"\"{this}\" cannot be converted to int");
        }
        return result;
    }

    uint IConvertible.ToUInt32(IFormatProvider? provider)
    {
        if (!uint.TryParse(Value, NumberStyles.Integer, provider, out uint result))
        {
            throw new InvalidCastException($"\"{this}\" cannot be converted to uint");
        }
        return result;
    }

    long IConvertible.ToInt64(IFormatProvider? provider)
    {
        if (!long.TryParse(Value, NumberStyles.Integer, provider, out long result))
        {
            throw new InvalidCastException($"\"{this}\" cannot be converted to long");
        }
        return result;
    }

    ulong IConvertible.ToUInt64(IFormatProvider? provider)
    {
        if (!ulong.TryParse(Value, NumberStyles.Integer, provider, out ulong result))
        {
            throw new InvalidCastException($"\"{this}\" cannot be converted to ulong");
        }
        return result;
    }

    float IConvertible.ToSingle(IFormatProvider? provider)
    {
        if (!float.TryParse(Value, NumberStyles.Float, provider, out float result))
        {
            throw new InvalidCastException($"\"{this}\" cannot be converted to float");
        }
        return result;
    }

    double IConvertible.ToDouble(IFormatProvider? provider)
    {
        if (!double.TryParse(Value, NumberStyles.Float, provider, out double result))
        {
            throw new InvalidCastException($"\"{this}\" cannot be converted to double");
        }
        return result;
    }

    decimal IConvertible.ToDecimal(IFormatProvider? provider)
    {
        if (!decimal.TryParse(Value, NumberStyles.Float, provider, out decimal result))
        {
            throw new InvalidCastException($"\"{this}\" cannot be converted to decimal");
        }
        return result;
    }

    DateTime IConvertible.ToDateTime(IFormatProvider? provider)
    {
        if (!DateTime.TryParse(Value, provider, DateTimeStyles.AllowWhiteSpaces, out DateTime result))
        {
            throw new InvalidCastException($"\"{this}\" cannot be converted to DateTime");
        }
        return result;
    }

    string IConvertible.ToString(IFormatProvider? provider) => ToLowerString();

    object IConvertible.ToType(Type conversionType, IFormatProvider? provider)
    {
        if (_toType.TryGetValue(conversionType, out Func<CaseInsensitive, IFormatProvider?, object>? converter))
        {
            return converter(this, provider);
        }
        throw new InvalidCastException($"\"{this}\" cannot be converted to {conversionType}");
    }

    #endregion
}