using Checkout.PaymentGateway.Common.Validation;
using System;

namespace Checkout.PaymentGateway.Core.Values
{
    public class Enumeration
    {
        public static Result<U> Create<U, V>(string raw, Func<V, U> creator, AppError error) where U : Enumeration<V>
                                                                                               where V : struct, Enum
        {
            if (!Enum.TryParse(raw, out V value))
            {
                return Result<U>.Fail<U>(error);
            }
            return Result<U>.Ok<U>(creator(value));
        }
    }
    public class Enumeration<T>: Enumeration, IEquatable<Enumeration<T>> where T: Enum 
    {
        public readonly T InnerEnum;
        public readonly string Label;
        public Enumeration(T inner, string label)
        {
            InnerEnum = inner;
            Label = label;
        }

        public Enumeration(T inner)
        {
            InnerEnum = inner;
            Label = inner.ToString();
        }

        public bool Equals(Enumeration<T> other)
        {
            return InnerEnum.Equals(other.InnerEnum)
                && Label == other.Label;
        }

        public override bool Equals(object obj)
        {
            var otherValue = obj as Enumeration<T>;

            if (ReferenceEquals(otherValue, null))
                return false;

            var typeMatches = GetType().Equals(obj.GetType());
            var valueMatches = InnerEnum.Equals(otherValue.InnerEnum)
                && Label == otherValue.Label;

            return typeMatches && valueMatches;
        }

        public override int GetHashCode() => InnerEnum.GetHashCode() + 13 * Label.GetHashCode();

        public static bool operator ==(Enumeration<T> obj1, Enumeration<T> obj2)
        {
            if (ReferenceEquals(obj1, null))
                return false;

            //Invoke the Equals() version implemented above
            return obj1.Equals(obj2);
        }

        //The == and != should be overloaded in pair
        public static bool operator !=(Enumeration<T> obj1, Enumeration<T> obj2)
        {
            return !(obj1 == obj2);
        }
    }
}
