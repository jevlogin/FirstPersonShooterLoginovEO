using System;
using System.ComponentModel;
using System.Numerics;

namespace JevLogin
{
    [Serializable]
    public struct SerializableGameObject
    {
        public string Name;
        public SerializableVector3 Position;
        public SerializableQuaternion Rotation;
        public SerializableVector3 Scale;
        public Component[] Components;
    }

    

    [Serializable]
    public struct SerializableVector3
    {
        public float X;
        public float Y;
        public float Z;
        public SerializableVector3(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }
        public static implicit operator Vector3(SerializableVector3 value)
        {
            return new Vector3(value.X, value.Y, value.Z);
        }
        public static implicit operator SerializableVector3(Vector3 value)
        {
            return new SerializableVector3(value.X, value.Y, value.Z);
        }

    }

    [Serializable]
    public struct SerializableQuaternion
    {

    }
}
