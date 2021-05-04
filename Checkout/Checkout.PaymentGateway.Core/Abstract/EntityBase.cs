using System;
using System.Collections.Generic;
using System.Text;

namespace Checkout.PaymentGateway.Core.Abstract
{

    internal interface IEntity
    {
        Guid Id { get; }
    }
    public abstract class EntityBase : IEntity, IEquatable<EntityBase>
    {
        public EntityBase(Guid id)
        {
            Id = id;
        }
        public Guid Id { get;}


        public bool Equals(EntityBase other)
        {
            if (ReferenceEquals(other, null))
                return false;

            return !IsTransient && !other.IsTransient && Id == other.Id;
        }

        public bool IsTransient
        {
            get
            {
                return Id == Guid.Empty;
            }
        }

        public override bool Equals(object obj)
        {
            var compareTo = obj as EntityBase;

            if (ReferenceEquals(compareTo, null))
                return false;

            if (ReferenceEquals(this, compareTo))
                return true;

            if (GetType() != obj.GetType())
                return false;

            return Equals(compareTo);

        }

        public static bool operator ==(EntityBase a, EntityBase b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
                return true;

            if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
                return false;

            return a.Equals(b);
        }

        public static bool operator !=(EntityBase a, EntityBase b)
        {
            return !(a == b);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = 13;
                hashCode = (hashCode * 397) ^ Id.GetHashCode();
                return hashCode;
            }
        }
    }
}
