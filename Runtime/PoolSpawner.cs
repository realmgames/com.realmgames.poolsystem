using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RealmGames.PoolSystem
{
    [Serializable]
    public class SpawnDirective {
        public Vector3 position;
    }

    public class PoolSpawner : MonoBehaviour
    {
        public GameObject prefab;

        [Header("offset to apply to spawn position")]
        public Vector3 spawnRangeMin, spawnRangeMax;

        [Header("delay between spawns")]
        public float spawnDelay;
        public float burstTime;
        public bool setAsParent;

        public SpawnDirective[] spawnOnEnable;

        private float m_dt;

        public void Spawn(Vector3 pos)
        {
            GameObject poolObject = PoolManager.Instance.Spawn(prefab);

            if (setAsParent)
                poolObject.transform.SetParent(transform);
            
            poolObject.transform.position = pos;
        }

        public void Spawn() {

            Vector3 pos = transform.position;

            float x = UnityEngine.Random.Range(spawnRangeMin.x, spawnRangeMax.x);
            float y = UnityEngine.Random.Range(spawnRangeMin.y, spawnRangeMax.y);
            float z = UnityEngine.Random.Range(spawnRangeMin.z, spawnRangeMax.z);

            pos += new Vector3(x,y,z);

            Spawn(pos);
        }

        private void OnEnable()
        {
            m_dt = 0;

            if(spawnOnEnable != null)
                foreach(SpawnDirective spawn in spawnOnEnable) {
                    Spawn(spawn.position);
                }
        }

        // Update is called once per frame
        void Update()
        {
            float dt = Time.deltaTime;

                m_dt += dt;

                if (m_dt >= spawnDelay)
                {
                    m_dt = 0;
                    Spawn();
                }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = new Color(1, 0, 0, 1F);
            Gizmos.DrawCube(transform.position, new Vector3(0.5f, 0.5f, 0.5f));
        }

        void OnDrawGizmosSelected()
        {
            Gizmos.color = new Color(0, 1f, 0, 1F);
            Gizmos.DrawCube(transform.position, new Vector3(0.5f, 0.5f, 0.5f));
        }
    }
}