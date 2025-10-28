using System;
using System.Linq;
using System.Reflection;
using App.Core.Attributes;
using App.Utils;
using UnityEditor;
using UnityEngine;

namespace Editor.SerializableTypeModule
{
    [CustomPropertyDrawer(typeof(SerializableType))]
    public class SerializableTypeDrawer : PropertyDrawer
    {
        private TypeFilterAttribute _typeFilter;
        private string[] _typeNames, _typeFullNames;
        
        private void Initialize(SerializedProperty property)
        {
            if (_typeFullNames != null) return;
            
            _typeFilter = fieldInfo.GetCustomAttribute<TypeFilterAttribute>();
            
            if (_typeFilter == null)
            {
                var parentType = property.serializedObject.targetObject.GetType();
                var path = property.propertyPath;
                
                var parentFieldName = path.Split('.').FirstOrDefault();
                if (!string.IsNullOrEmpty(parentFieldName))
                {
                    var parentField = parentType.GetField(parentFieldName,
                        BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                    _typeFilter = parentField?.GetCustomAttribute<TypeFilterAttribute>();
                }
            }
            
            var filteredTypes = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .Where(t => _typeFilter == null ? DefaultFilter(t) : _typeFilter.Filter(t))
                .ToArray();
            
            _typeNames = filteredTypes
                .Select(t => t.ReflectedType == null ? t.Name : $"{t.ReflectedType.Name}.{t.Name}")
                .ToArray();
            
            _typeFullNames = filteredTypes
                .Select(t => t.AssemblyQualifiedName)
                .ToArray();
        }
        
        static bool DefaultFilter(Type type)
        {
            return !type.IsAbstract && !type.IsInterface && !type.IsGenericType;
        }
        
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            Initialize(property);
            
            // exception for serialized dictionary by AYellowpaper
            if (label is { text: "Key" }) label = GUIContent.none;
            
            var typeIdProperty = property.FindPropertyRelative("_assemblyQualifiedName");
            
            if (_typeFullNames == null || _typeFullNames.Length == 0)
            {
                EditorGUI.HelpBox(position, "No types found", MessageType.Warning);
                return;
            }
            
            if (string.IsNullOrEmpty(typeIdProperty.stringValue))
            {
                typeIdProperty.stringValue = _typeFullNames.First();
                property.serializedObject.ApplyModifiedProperties();
            }
            
            var currentIndex = Array.IndexOf(_typeFullNames, typeIdProperty.stringValue);
            if (currentIndex < 0) currentIndex = 0;
            
            var selectedIndex = EditorGUI.Popup(position, label.text, currentIndex, _typeNames);
            
            if (selectedIndex >= 0 && selectedIndex != currentIndex)
            {
                typeIdProperty.stringValue = _typeFullNames[selectedIndex];
                property.serializedObject.ApplyModifiedProperties();
            }
        }
    }
}
