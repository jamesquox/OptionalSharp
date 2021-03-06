using System;

namespace OptionalSharp {

	public partial struct Optional<T> {
		/// <summary>
		/// Determines if this instance is equal to another Optional, where the inner type of the other Optional is not known.
		/// </summary>
		/// <param name="other">The other Optional.</param>
		/// <returns></returns>
		public bool Equals(IAnyOptional other) {
			if (other == null) return false;
			return (!HasValue && !other.HasValue) || (HasValue && other.HasValue && Equals(Value, other.Value));
		}

		/// <summary>
		/// Checks if this instance is equal to some <see cref="Optional{T2}"/>, with a potentially different inner type.
		/// </summary>
		/// <typeparam name="T2">The inner type of the other Optional.</typeparam>
		/// <param name="other">The other Optional.</param>
		/// <returns></returns>
		public bool Equals<T2>(Optional<T2> other) {
			return !HasValue ? !other.HasValue : HasValue && other.HasValue && Equals(Value, other.Value);
		}

		/// <summary>
		///     Determines equality between this instance and another object, such as another Optional or a concrete value.
		/// </summary>
		/// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
		/// <returns>
		///     <c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.
		/// </returns>
		public override bool Equals(object obj) {
			switch (obj) {
				case Optional<T> opt:
					return Equals(opt);
				case IAnyOptional opt:
					return Equals(opt);
				default:
					return false;
			}
		}

		/// <summary>
		/// Determines if this instance is equal to the given non-optional value.
		/// </summary>
		/// <param name="other">The concrete value.</param>
		/// <returns>
		/// true if the current object is equal to the <paramref name="other" /> parameter; otherwise, false.
		/// </returns>
		public bool ValueEquals(T other) {
			return HasValue && _eq.Equals(Value, other);
		}

		/// <summary>
		///		Determines whether this instance is equal to another <see cref="Optional{T}"/>.
		/// </summary>
		/// <param name="other">An object to compare with this object.</param>
		public bool Equals(Optional<T> other) {
			return !other.HasValue ? !HasValue : ValueEquals(other.Value);
		}

		/// <summary>
		///     Returns a hash code for this instance.
		/// </summary>
		public override int GetHashCode() {
			return !HasValue ? OptionalShared.NoneHashCode : OptionalShared.SomeHashCodeXor ^ _eq.GetHashCode(Value);
		}
	}
}