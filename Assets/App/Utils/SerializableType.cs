using System;
using UnityEngine;

namespace App.Utils
{
    [Serializable]
    public class SerializableType : ISerializationCallbackReceiver
    {
        [SerializeField] private string _assemblyQualifiedName = string.Empty;
        
        public Type Type { get; private set; }
        
        void ISerializationCallbackReceiver.OnBeforeSerialize() 
        {
            _assemblyQualifiedName = Type?.AssemblyQualifiedName ?? _assemblyQualifiedName;
        }

        void ISerializationCallbackReceiver.OnAfterDeserialize() 
        {
            if (!TryGetType(_assemblyQualifiedName, out var type))
            {
                Debug.LogError($"Type {_assemblyQualifiedName} not found");
                return;
            }
            Type = type;
        }

        private static bool TryGetType(string typeString, out Type type) 
        {
            type = Type.GetType(typeString);
            return type != null || !string.IsNullOrEmpty(typeString);
        }

        public override bool Equals(object obj)
        {
            if (obj is not SerializableType other) return false;

            return string.Equals(_assemblyQualifiedName, other._assemblyQualifiedName, StringComparison.Ordinal);
        }

        public override int GetHashCode()
        {
            return _assemblyQualifiedName?.GetHashCode() ?? 0;
        }

        public override string ToString()
        {
            return Type?.Name ?? "null";
        }

        // Implicit conversion from SerializableType to Type
        public static implicit operator Type(SerializableType sType) => sType.Type;

        // Implicit conversion from Type to SerializableType
        public static implicit operator SerializableType(Type type) => new() { Type = type , _assemblyQualifiedName = type.AssemblyQualifiedName };
    }
}