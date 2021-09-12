using System;
using System.Linq.Expressions;

namespace irsdkSharp.Serialization
{
    public static class ExpressionAccessors
    {
        public static Func<TTarget, TReturn> GenerateMemberGetter<TTarget, TReturn>(string memberName)
        {
            if (string.IsNullOrEmpty(memberName)) throw new ArgumentException(nameof(memberName));

            var param = Expression.Parameter(typeof(TTarget), "this");
            var member = Expression.PropertyOrField(param, memberName);
            var lambda = Expression.Lambda<Func<TTarget, TReturn>>(member, param);

            return lambda.Compile();
        }

        public static Func<object, TReturn> GenerateMemberGetter<TReturn>(Type type, string memberName)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));
            if (string.IsNullOrEmpty(memberName)) throw new ArgumentException(nameof(memberName));

            var param = Expression.Parameter(typeof(object), "this");
            var castParam = Expression.Convert(param, type);
            var member = Expression.PropertyOrField(castParam, memberName);
            var lambda = Expression.Lambda<Func<object, TReturn>>(member, param);

            return lambda.Compile();
        }

        public delegate void ValueTypeMemberSetterDelegate<TTarget, TValue>(ref TTarget @this, TValue value);
        public delegate void ReferenceTypeMemberSetterDelegate<TTarget, TValue>(TTarget @this, TValue value);

        public static ValueTypeMemberSetterDelegate<TTarget, TValue> GenerateValueTypeMemberSetter<TTarget, TValue>(string memberName)
            where TTarget : struct
        {
            if (string.IsNullOrEmpty(memberName)) throw new ArgumentException(nameof(memberName));

            var thisRef = typeof(TTarget).MakeByRefType();

            var paramThis = Expression.Parameter(thisRef, "this");
            var paramValue = Expression.Parameter(typeof(TValue), "value");
            var member = Expression.PropertyOrField(paramThis, memberName);
            var assign = Expression.Assign(member, paramValue);
            var lambda = Expression.Lambda<ValueTypeMemberSetterDelegate<TTarget, TValue>>(assign, paramThis, paramValue);

            return lambda.Compile();
        }

        public static ReferenceTypeMemberSetterDelegate<TTarget, TValue> GenerateReferenceTypeMemberSetter<TTarget, TValue>(string memberName)
            where TTarget : class
        {
            if (string.IsNullOrEmpty(memberName)) throw new ArgumentException(nameof(memberName));

            var paramThis = Expression.Parameter(typeof(TTarget), "this");
            var paramValue = Expression.Parameter(typeof(TValue), "value");
            var member = Expression.PropertyOrField(paramThis, memberName);
            var assign = Expression.Assign(member, paramValue);
            var lambda = Expression.Lambda<ReferenceTypeMemberSetterDelegate<TTarget, TValue>>(assign, paramThis, paramValue);

            return lambda.Compile();
        }

        public static ReferenceTypeMemberSetterDelegate<object, TValue> GenerateReferenceTypeMemberSetter<TValue>(Type type, string memberName)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));
            if (string.IsNullOrEmpty(memberName)) throw new ArgumentException(nameof(memberName));
            if (type.IsValueType) throw new ArgumentException("Type must be a reference type", nameof(type));

            var paramThis = Expression.Parameter(typeof(object), "this");
            var paramValue = Expression.Parameter(typeof(TValue), "value");
            var castThis = Expression.Convert(paramThis, type);
            var member = Expression.PropertyOrField(castThis, memberName);
            var assign = Expression.Assign(member, paramValue);
            var lambda = Expression.Lambda<ReferenceTypeMemberSetterDelegate<object, TValue>>(assign, paramThis, paramValue);

            return lambda.Compile();
        }
    }
}
