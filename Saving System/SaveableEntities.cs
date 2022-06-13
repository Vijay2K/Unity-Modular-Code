using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Game.SavingSystem
{
    [ExecuteInEditMode]
    public class SaveableEntities : MonoBehaviour
    {
        [SerializeField] private string uniqueIdentifier = "";

        public string GetUniqueIdentifier() => uniqueIdentifier;

#if UNITY_EDITOR
        private void Update()
        {
            if (Application.IsPlaying(this.gameObject)) return;
            if (string.IsNullOrEmpty(gameObject.scene.path)) return;

            SerializedObject serializedObject = new SerializedObject(this);
            SerializedProperty serializedProperty = serializedObject.FindProperty("uniqueIdentifier");
            
            if(string.IsNullOrEmpty(serializedProperty.stringValue))
            {
                serializedProperty.stringValue = System.Guid.NewGuid().ToString();
                serializedObject.ApplyModifiedProperties();
            }
        }
#endif

        public object CaptureState()
        {
            Debug.Log("Capturing : " + GetUniqueIdentifier());
            Dictionary<string, object> state = new Dictionary<string, object>();
            ISaveable[] saveables = GetComponents<ISaveable>();
            foreach(ISaveable saveable in saveables)
            {
                state[saveable.GetType().ToString()] = saveable.CaptureState();
            }

            return state;
        }

        public void RestoreState(object state)
        {
            Debug.Log("Restoring : " + GetUniqueIdentifier());
            Dictionary<string, object> stateDict = state as Dictionary<string, object>;
            ISaveable[] saveables = GetComponents<ISaveable>();
            foreach(ISaveable saveable in saveables)
            {
                string id = saveable.GetType().ToString();

                if(stateDict.ContainsKey(id))
                {
                    saveable.RestoreState(stateDict[id]);
                }
            }
        }
    }

}