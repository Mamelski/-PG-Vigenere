using System;

namespace Vigenere.Core
{
    public struct muint : IComparable<muint>, IEquatable<muint>
    {
        private const uint DefaultModulo = 26;

        public static uint Modulo = muint.DefaultModulo;

        private uint _value;

        public muint(uint value)
        {
            this._value = value % Modulo;
        }

        public int CompareTo(muint other)
        {
            return this._value.CompareTo(other._value);
        }

        public override bool Equals(object other)
        {
            return other is muint ? this.Equals((muint)other) : false;
        }

        public bool Equals(muint other)
        {
            return this._value.Equals(other._value);
        }

        public override int GetHashCode()
        {
            return this._value.GetHashCode();
        }

        public static implicit operator muint(int value)
        {
            if (value >= 0)
                return new muint((uint)value);
            else
            {
                int mod = value % (int)Modulo;
                return new muint((uint)(mod + Modulo));
            }
        }

        public static implicit operator muint(uint value)
        {
            return new muint(value);
        }

        public static implicit operator muint(char value)
        {
            return new muint((uint)value);
        }

        public static explicit operator int(muint value)
        {
            return (int)value._value;
        }

        public static explicit operator uint(muint value)
        {
            return value._value;
        }

        public static explicit operator char(muint value)
        {
            return (char)value._value;
        }

        public static muint operator+(muint first, muint second)
        {
            return (first._value + second._value) % muint.Modulo;
        }

        public static muint operator-(muint first, muint second)
        {
            return (first._value - second._value) % muint.Modulo;
        }

        public static muint operator*(muint first, muint second)
        {
            return (first._value * second._value) % muint.Modulo;
        }

        public static muint operator/(muint first, muint second)
        {
            return (first._value / second._value) % muint.Modulo;
        }

        public static bool operator<(muint first, muint second)
        {
            return first.CompareTo(second) < 0;
        }

        public static bool operator<=(muint first, muint second)
        {
            return first.CompareTo(second) <= 0;
        }

        public static bool operator>(muint first, muint second)
        {
            return first.CompareTo(second) > 0;
        }

        public static bool operator>=(muint first, muint second)
        {
            return first.CompareTo(second) >= 0;
        }

        public static bool operator==(muint first, muint second)
        {
            return first.CompareTo(second) == 0;
        }

        public static bool operator!=(muint first, muint second)
        {
            return first.CompareTo(second) != 0;
        }

        public override string ToString()
        {
            return this._value.ToString();
        }
    }
}

