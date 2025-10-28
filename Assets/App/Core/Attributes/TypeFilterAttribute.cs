using System;
using App.Utils;
using UnityEngine;

namespace App.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = true)]
    public class TypeFilterAttribute : PropertyAttribute 
    {
        public Func<Type, bool> Filter { get; }
        
        public bool IncludeBaseType { get; }
        
        public TypeFilterAttribute(Type filterType, bool includeBaseType = false) 
        {
            IncludeBaseType = includeBaseType;
            
            Filter = type => !type.IsAbstract &&
                             !type.IsInterface &&
                             !type.IsGenericType &&
                             type.InheritsOrImplements(filterType) &&
                             (includeBaseType || type != filterType);
        }
    }
}