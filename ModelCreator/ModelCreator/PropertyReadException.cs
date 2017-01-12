﻿using System;
using Newtonsoft.Json.Linq;

namespace Assets.Scripts.ModuleResources.Exceptions
{
    class PropertyReadException : Exception
    {
        public string PropertyName { get; set; }
        public JObject ParentObject { get; set; }
        public string FoundValue { get; set; }
        public Type RequiredType { get; set; }

        public PropertyReadException(string propertyName, JObject parentObject, string foundValue, Type requiredType)
        {
            PropertyName = propertyName;
            ParentObject = parentObject;
            FoundValue = foundValue;
            RequiredType = requiredType;
        }

        public override string ToString()
        {
            if (FoundValue == null)
            {
                return string.Format("Property missing. {0} is a required property of type {1}. Searched in: root.{2}", PropertyName, TypeToReadeable(RequiredType), ParentObject.Path);
            }
            else
            {
                return string.Format("Property of an invalid type. {0} needs to be {1} ({3} found). Searched in: {2}", PropertyName, TypeToReadeable(RequiredType), ParentObject.Path, FoundValue);
            }
        }

        private string TypeToReadeable(Type type)
        {
            if (type == typeof(JObject))
                return "JSON Object";
            else if (type == typeof(JArray))
                return "JSON Array";
            else if (type == typeof(int))
                return "integer";
            else if (type == typeof(double))
                return "double";
            else if (type == typeof(string))
                return "string";
            else if (type == typeof(int[]))
                return "Array of ints";
            else if (type == typeof(float[]))
                return "Array of floats";
            else
                return type.Name;
        }       
    }
}
