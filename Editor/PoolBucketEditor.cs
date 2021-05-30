using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace RealmGames.PoolSystem
{
    [CustomEditor(typeof(PoolBucket))]
    public class PoolBucketEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            // Show default inspector property editor
            DrawDefaultInspector();

            PoolBucket poolBucket = (PoolBucket)target;
            EditorGUILayout.Separator();
            EditorGUILayout.IntField("Size", poolBucket.Size);
            EditorGUILayout.IntField("Allocated", poolBucket.Allocated);
        }
    }
}