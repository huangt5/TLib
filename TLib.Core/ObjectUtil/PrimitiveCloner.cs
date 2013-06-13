using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace TLib.Core.ObjectUtil {
    public class PrimitiveCloner {
        private Type _targetType;
        private Type _sourceType;
        private List<string> _excludes = new List<string>();
        private bool _convertToNull = true;
        // Mapping of primitive types and it's null value representative.
        private Dictionary<Type, object> _primitive = new Dictionary<Type, object>();

        public bool ConvertToNull
        {
            get { return _convertToNull; }
            set { _convertToNull = value; }
        }

        private PrimitiveCloner()
        {
            _primitive.Add(typeof(bool?), null);
            _primitive.Add(typeof(bool), false);
            _primitive.Add(typeof(string), null);
            _primitive.Add(typeof(byte), 0);
            _primitive.Add(typeof(byte?), null);
            _primitive.Add(typeof(int), 0);
            _primitive.Add(typeof(int?), null);
            _primitive.Add(typeof(short), 0);
            _primitive.Add(typeof(short?), null);
            _primitive.Add(typeof(long), 0);
            _primitive.Add(typeof(long?), null);
            _primitive.Add(typeof(decimal), 0);
            _primitive.Add(typeof(decimal?), null);
            _primitive.Add(typeof(DateTime), DateTime.MinValue);
            _primitive.Add(typeof(DateTime?), null);
            _primitive.Add(typeof(Guid), Guid.Empty);
            _primitive.Add(typeof(Guid?), null);
        }

        public PrimitiveCloner(Type sourceType) : this()
        {
            _sourceType = sourceType;
            _targetType = sourceType;
        }

        public PrimitiveCloner(Type sourceType, Type targetType) : this()
        {
            _sourceType = sourceType;
            _targetType = targetType;
        }

        public void Clone(object from, object to) {
            if (from == null || to == null) {
                throw new Exception("Can not clone null object.");
            }

            foreach (PropertyInfo sourceProp in _sourceType.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.GetProperty | BindingFlags.SetProperty)) {
                if (IsPrimitive(sourceProp) && sourceProp.CanRead && sourceProp.CanWrite && !IsExcluded(sourceProp)) {
                    try
                    {
                        PropertyInfo targetProp = sourceProp;
                        if (_sourceType != _targetType)
                        {
                            targetProp = _targetType.GetProperty(sourceProp.Name);
                        }
                        if (targetProp != null)
                        {
                            object value = sourceProp.GetValue(from, null);

                            // Convert to null if target property is nullable.
                            if (NeedConvertoNull(targetProp, value))
                            {
                                value = null;
                            }

                            targetProp.SetValue(to, value, null);
                        }
                    } catch (Exception e) {
                        throw new Exception(string.Format("Failed to clone propery of {0}.{1}", sourceProp.DeclaringType.Name, sourceProp.Name), e);
                    }
                }
            }
        }

        private bool NeedConvertoNull(PropertyInfo targetProp, object value)
        {
            if (value == null)
            {
                return false;
            }
            if (targetProp.PropertyType.IsGenericType && targetProp.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                // Compare to see if value is same as null representative.
                return value.Equals(_primitive[value.GetType()]);
            }
            return false;
        }

        private bool IsExcluded(PropertyInfo prop)
        {
            return _excludes.Contains(prop.Name);
        }

        public void AddExclude(params string[] propertyName)
        {
            _excludes.AddRange(propertyName);
        }

        private bool IsPrimitive(PropertyInfo prop)
        {
            return _primitive.Keys.ToArray().Contains(prop.PropertyType);
        }
    }
}
