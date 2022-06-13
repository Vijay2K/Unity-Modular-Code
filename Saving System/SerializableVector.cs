using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.SavingSystem
{
    [System.Serializable]
    public class SerializableVector
    {
        private float x;
        private float y;
        private float z;

        public SerializableVector(Vector3 vector)
        {
            x = vector.x;
            y = vector.y;
            z = vector.z;
        }

        public Vector3 ToVector()
        {
            return new Vector3(x, y, z);
        }
    }
}
