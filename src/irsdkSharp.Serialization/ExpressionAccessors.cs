using irsdkSharp.Serialization.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace irsdkSharp.Serialization
{
    public static class ExpressionAccessors
    {
        public static Dictionary<string, ReferenceTypeMemberSetterDelegate<object, object>> ModelSetters;
        public static PropertyInfo[] CarModelProperties;
        public static PropertyInfo[] DataModelProperties;
        static ExpressionAccessors()
        {
            CarModelProperties = typeof(DataModel).GetProperties();
            DataModelProperties = typeof(CarModel).GetProperties();
            ModelSetters = new Dictionary<string, ReferenceTypeMemberSetterDelegate<object, object>>();

            foreach (var property in CarModelProperties)
            {
                ModelSetters.Add($"CarModel::{property.Name}", GetSetter(typeof(CarModel), property));
            }

            foreach (var property in DataModelProperties)
            {
                ModelSetters.Add($"DataModel::{property.Name}", GetSetter(typeof(DataModel), property));
            }
        }

        public delegate void ReferenceTypeMemberSetterDelegate<TTarget, TValue>(TTarget @this, TValue value);

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

        private static ReferenceTypeMemberSetterDelegate<object, object> GetSetter(Type model, PropertyInfo property)
        {
            return GenerateReferenceTypeMemberSetter<object>(model, property.Name);
        }
    }
}
