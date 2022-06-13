using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System;

namespace Game.SavingSystem
{
    public class SavingSystem : MonoBehaviour
    {
        public void Save(string fileName)
        {
            Dictionary<string, object> state = LoadFile(fileName);
            CaptureState(state);
            SaveFile(fileName, state);
        }

        public void Load(string fileName)
        {
            RestoreState(LoadFile(fileName));
        }

        private void SaveFile(string fileName, object state)
        {
            string path = GetPath(fileName);
            using(FileStream stream = File.Open(path, FileMode.Create))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, state);
            }
        }

        private Dictionary<string, object> LoadFile(string fileName)
        {
            string path = GetPath(fileName);

            if (!File.Exists(path))
            {
                return new Dictionary<string, object>();
            }

            using(FileStream stream = File.Open(path, FileMode.Open))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                return formatter.Deserialize(stream) as Dictionary<string, object>;
            }
        }

        private void CaptureState(Dictionary<string, object> state)
        {
            foreach(SaveableEntities saveable in FindObjectsOfType<SaveableEntities>())
            {
                state[saveable.GetUniqueIdentifier()] = saveable.CaptureState();
            }
        }

        private void RestoreState(Dictionary<string, object> state)
        {
            foreach(SaveableEntities saveable in FindObjectsOfType<SaveableEntities>())
            {
                string id = saveable.GetUniqueIdentifier();
                if(state.ContainsKey(id))
                {
                    saveable.RestoreState(state[id]);
                }
            }
        }

        private string GetPath(string fileName)
        {
            return Path.Combine(Application.persistentDataPath, fileName + ".sav");
        }
    }
}
