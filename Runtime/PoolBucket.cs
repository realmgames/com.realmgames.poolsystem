using System;
using System.Collections.Generic;
using UnityEngine;

namespace RealmGames.PoolSystem
{
    public class PoolBucket : MonoBehaviour
    {
        public GameObject prefab;
        public bool setAsParent = true;
        public int initialCapacity = 0;

        private List<PoolObject> m_pool = new List<PoolObject>();

        public int Size {
            get {
                return m_pool.Count;
            }
        }

        public int Allocated
        {
            get
            {
                int count = 0;

                foreach(PoolObject poolObject in m_pool) {
                    if (poolObject.allocated)
                        count++;
                }

                return count;
            }
        }

        private void Awake()
        {
            Generate(initialCapacity);
        }

        public void Free()
        {
            foreach (PoolObject item in m_pool)
            {
                Destroy(item.gameObject);
            }
            m_pool.Clear();
        }

        public void Generate(int count)
        {
            for (int i = 0; i < count; i++)
                m_pool.Add( GenerateNew() );
        }

        public PoolObject GenerateNew()
        {
            PoolObject poolObj = Clone(prefab);
            poolObj.name = prefab.name + m_pool.Count;
            poolObj.bucket = this;
            return poolObj;
        }

        public PoolObject Spawn()
        {
            PoolObject poolObj = null;

            foreach (PoolObject obj in m_pool)
            {
                if (!obj.allocated)
                {
                    poolObj = obj;
                    break;
                }
            }

            if (poolObj == null)
            {
                poolObj = GenerateNew();
            }
            else {
                m_pool.Remove(poolObj);
            }

            poolObj.allocated = true;
            poolObj.gameObject.SetActive(true);
            m_pool.Add(poolObj);

            return poolObj;
        }

        public PoolObject Get(GameObject obj)
        {
            foreach (PoolObject item in m_pool)
            {
                if (item.gameObject == obj)
                    return item;
            }
            return null;
        }

        private PoolObject Clone(GameObject p)
        {
            GameObject obj = Instantiate(p);

            PoolObject poolObject = obj.GetComponent<PoolObject>();

            if (poolObject == null)
                poolObject = obj.AddComponent<PoolObject>();

            if (setAsParent)
                obj.transform.SetParent(transform);

            return poolObject;
        }
    }
}