using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq.Expressions;
using System.Reflection;

namespace BlazorPWA.Components
{

    public static class TypeUtils
    {
        private static readonly object[] EmptyObjects = new object[] { };

        public delegate object GetDelegate(object source);
        public delegate void SetDelegate(object source, object value);
        public delegate object ConstructorDelegate(params object[] args);

        public delegate TValue ThreadSafeDictionaryValueFactory<TKey, TValue>(TKey key);

        public static Type GetTypeInfo(this Type type) => type;

        public static Attribute GetAttribute(this Type type, MemberInfo info)
        {
            if (info == null || type == null || !Attribute.IsDefined(info, type))
                return null;
            return Attribute.GetCustomAttribute(info, type);
        }

        public static Type GetGenericListElementType(this Type type)
        {
            IEnumerable<Type> interfaces;
            interfaces = type.GetInterfaces();
            foreach (var implementedInterface in interfaces)
            {
                if (IsTypeGeneric(implementedInterface) &&
                    implementedInterface.GetGenericTypeDefinition() == typeof(IList<>))
                {
                    return GetGenericTypeArguments(implementedInterface)[0];
                }
            }
            return GetGenericTypeArguments(type)[0];
        }

        public static TAttribute GetAttribute<TAttribute>(this Type objectType)
            where TAttribute : Attribute
            => (TAttribute)GetAttribute(objectType, typeof(TAttribute));

        public static Attribute GetAttribute(this Type objectType, Type attributeType)
        {
            if (objectType == null || attributeType == null || !Attribute.IsDefined(objectType, attributeType))
                return null;
            return Attribute.GetCustomAttribute(objectType, attributeType);
        }

        public static Type[] GetGenericTypeArguments(this Type type) => type.GetGenericArguments();

        public static bool IsTypeGeneric(this Type type) => GetTypeInfo(type).IsGenericType;

        public static bool IsTypeGenericCollectionInterface(Type type)
        {
            if (!IsTypeGeneric(type))
                return false;

            var genericDefinition = type.GetGenericTypeDefinition();

            return (genericDefinition == typeof(IList<>)
                || genericDefinition == typeof(ICollection<>)
                || genericDefinition == typeof(IEnumerable<>)

                    );
        }

        public static bool IsAssignableFrom(this Type type1, Type type2) => GetTypeInfo(type1).IsAssignableFrom(GetTypeInfo(type2));

        public static bool IsTypeDictionary(this Type type)
        {

            if (typeof(System.Collections.IDictionary).IsAssignableFrom(type))
                return true;

            if (!GetTypeInfo(type).IsGenericType)
                return false;

            var genericDefinition = type.GetGenericTypeDefinition();
            return genericDefinition == typeof(IDictionary<,>);
        }

        public static bool IsNullableType(this Type type) => GetTypeInfo(type).IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);

        public static TValueType? ToNullableType<TValueType>(this TValueType obj) where TValueType : struct
            => (TValueType?)ToNullableType(obj, typeof(TValueType));
        public static object ToNullableType(this object obj, Type nullableType) => obj == null ? null : Convert.ChangeType(obj, Nullable.GetUnderlyingType(nullableType), CultureInfo.InvariantCulture);

        public static bool IsValueType(this Type type) => GetTypeInfo(type).IsValueType;

        public static IEnumerable<ConstructorInfo> GetConstructors(this Type type)
        {
            const BindingFlags flags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
            return type.GetConstructors(flags);
        }

        public static ConstructorInfo GetConstructorInfo(this Type type, params Type[] argsType)
        {
            var constructorInfos = GetConstructors(type);
            int i;
            bool matches;
            foreach (var constructorInfo in constructorInfos)
            {
                var parameters = constructorInfo.GetParameters();
                if (argsType.Length != parameters.Length)
                    continue;

                i = 0;
                matches = true;
                foreach (var parameterInfo in constructorInfo.GetParameters())
                {
                    if (parameterInfo.ParameterType != argsType[i])
                    {
                        matches = false;
                        break;
                    }
                }

                if (matches)
                    return constructorInfo;
            }

            return null;
        }

        public static IEnumerable<PropertyInfo> GetProperties(this Type type) => type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);

        public static IEnumerable<FieldInfo> GetFields(this Type type) => type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static);

        public static MethodInfo GetGetterMethodInfo(this PropertyInfo propertyInfo) => propertyInfo.GetGetMethod(true);

        public static MethodInfo GetSetterMethodInfo(this PropertyInfo propertyInfo) => propertyInfo.GetSetMethod(true);

        public static ConstructorDelegate GetConstructor(this ConstructorInfo constructorInfo) => GetConstructorByExpression(constructorInfo);

        public static ConstructorDelegate GetConstructor(this Type type, params Type[] argsType) => GetConstructorByExpression(type, argsType);

        private static ConstructorDelegate GetConstructorByExpression(this ConstructorInfo constructorInfo)
        {
            var paramsInfo = constructorInfo.GetParameters();
            var param = Expression.Parameter(typeof(object[]), "args");
            var argsExp = new Expression[paramsInfo.Length];
            for (var i = 0; i < paramsInfo.Length; i++)
            {
                Expression index = Expression.Constant(i);
                var paramType = paramsInfo[i].ParameterType;
                Expression paramAccessorExp = Expression.ArrayIndex(param, index);
                Expression paramCastExp = Expression.Convert(paramAccessorExp, paramType);
                argsExp[i] = paramCastExp;
            }
            var newExp = Expression.New(constructorInfo, argsExp);
            var lambda = Expression.Lambda<Func<object[], object>>(newExp, param);
            var compiledLambda = lambda.Compile();
            return delegate (object[] args) { return compiledLambda(args); };
        }

        private static ConstructorDelegate GetConstructorByExpression(this Type type, params Type[] argsType)
        {
            var constructorInfo = GetConstructorInfo(type, argsType);
            return constructorInfo == null ? null : GetConstructorByExpression(constructorInfo);
        }

        public static GetDelegate GetGetMethod(this PropertyInfo propertyInfo) => GetGetMethodByExpression(propertyInfo);

        public static GetDelegate GetGetMethod(this FieldInfo fieldInfo) => GetGetMethodByExpression(fieldInfo);

        private static GetDelegate GetGetMethodByExpression(this PropertyInfo propertyInfo)
        {
            var getMethodInfo = GetGetterMethodInfo(propertyInfo);
            var instance = Expression.Parameter(typeof(object), "instance");
            var instanceCast = (!IsValueType(propertyInfo.DeclaringType)) ? Expression.TypeAs(instance, propertyInfo.DeclaringType) : Expression.Convert(instance, propertyInfo.DeclaringType);
            var compiled = Expression.Lambda<Func<object, object>>(Expression.TypeAs(Expression.Call(instanceCast, getMethodInfo), typeof(object)), instance).Compile();
            return delegate (object source) { return compiled(source); };
        }

        private static GetDelegate GetGetMethodByExpression(this FieldInfo fieldInfo)
        {
            var instance = Expression.Parameter(typeof(object), "instance");
            var member = Expression.Field(Expression.Convert(instance, fieldInfo.DeclaringType), fieldInfo);
            var compiled = Expression.Lambda<GetDelegate>(Expression.Convert(member, typeof(object)), instance).Compile();
            return delegate (object source) { return compiled(source); };
        }

        public static SetDelegate GetSetMethod(this PropertyInfo propertyInfo) => GetSetMethodByExpression(propertyInfo);

        public static SetDelegate GetSetMethod(this FieldInfo fieldInfo) => GetSetMethodByExpression(fieldInfo);

        private static SetDelegate GetSetMethodByExpression(this PropertyInfo propertyInfo)
        {
            var setMethodInfo = GetSetterMethodInfo(propertyInfo);
            var instance = Expression.Parameter(typeof(object), "instance");
            var value = Expression.Parameter(typeof(object), "value");
            var instanceCast = (!IsValueType(propertyInfo.DeclaringType)) ? Expression.TypeAs(instance, propertyInfo.DeclaringType) : Expression.Convert(instance, propertyInfo.DeclaringType);
            var valueCast = (!IsValueType(propertyInfo.PropertyType)) ? Expression.TypeAs(value, propertyInfo.PropertyType) : Expression.Convert(value, propertyInfo.PropertyType);
            var compiled = Expression.Lambda<Action<object, object>>(Expression.Call(instanceCast, setMethodInfo, valueCast), new ParameterExpression[] { instance, value }).Compile();
            return delegate (object source, object val) { compiled(source, val); };
        }

        private static SetDelegate GetSetMethodByExpression(FieldInfo fieldInfo)
        {
            var instance = Expression.Parameter(typeof(object), "instance");
            var value = Expression.Parameter(typeof(object), "value");
            var compiled = Expression.Lambda<Action<object, object>>(
                Assign(Expression.Field(Expression.Convert(instance, fieldInfo.DeclaringType), fieldInfo), Expression.Convert(value, fieldInfo.FieldType)), instance, value).Compile();
            return delegate (object source, object val) { compiled(source, val); };
        }

        private static BinaryExpression Assign(Expression left, Expression right)
        {
            var assign = typeof(Assigner<>).MakeGenericType(left.Type).GetMethod("Assign");
            var assignExpr = Expression.Add(left, right, assign);
            return assignExpr;
        }

        private static class Assigner<T>
        {
            public static T Assign(ref T left, T right) => (left = right);
        }

        public sealed class ThreadSafeDictionary<TKey, TValue> : IDictionary<TKey, TValue>
        {
            private readonly object _lock = new object();
            private readonly ThreadSafeDictionaryValueFactory<TKey, TValue> _valueFactory;
            private Dictionary<TKey, TValue> _dictionary;

            public ThreadSafeDictionary(ThreadSafeDictionaryValueFactory<TKey, TValue> valueFactory)
            {
                _valueFactory = valueFactory;
            }

            private TValue Get(TKey key)
            {
                if (_dictionary == null)
                    return AddValue(key);
                if (!_dictionary.TryGetValue(key, out var value))
                    return AddValue(key);
                return value;
            }

            private TValue AddValue(TKey key)
            {
                var value = _valueFactory(key);
                lock (_lock)
                {
                    if (_dictionary == null)
                    {
                        _dictionary = new Dictionary<TKey, TValue>
                        {
                            [key] = value
                        };
                    }
                    else
                    {
                        if (_dictionary.TryGetValue(key, out var val))
                            return val;
                        var dict = new Dictionary<TKey, TValue>(_dictionary)
                        {
                            [key] = value
                        };
                        _dictionary = dict;
                    }
                }
                return value;
            }

            public void Add(TKey key, TValue value) => throw new NotImplementedException();

            public bool ContainsKey(TKey key) => _dictionary.ContainsKey(key);

            public ICollection<TKey> Keys => _dictionary.Keys;

            public bool Remove(TKey key) => throw new NotImplementedException();

            public bool TryGetValue(TKey key, out TValue value)
            {
                value = this[key];
                return true;
            }

            public ICollection<TValue> Values => _dictionary.Values;

            public TValue this[TKey key]
            {
                get => Get(key);
                set => throw new NotImplementedException();
            }

            public void Add(KeyValuePair<TKey, TValue> item) => throw new NotImplementedException();

            public void Clear() => throw new NotImplementedException();

            public bool Contains(KeyValuePair<TKey, TValue> item) => throw new NotImplementedException();

            public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex) => throw new NotImplementedException();

            public int Count => _dictionary.Count;

            public bool IsReadOnly => throw new NotImplementedException();

            public bool Remove(KeyValuePair<TKey, TValue> item) => throw new NotImplementedException();

            public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator() => _dictionary.GetEnumerator();

            System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => _dictionary.GetEnumerator();
        }
    }
}
