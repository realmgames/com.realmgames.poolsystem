using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RealmGames.PoolSystem
{
    public class PoolManager : MonoBehaviour
    {
        static PoolManager _instance;

        public static PoolManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<PoolManager>();
                }
                return _instance;
            }
            set
            {
                _instance = value;
            }
        }

        private List<PoolBucket> m_buckets = new List<PoolBucket>();
        private Dictionary<GameObject, PoolBucket> m_bucketMap = new Dictionary<GameObject, PoolBucket>();

        private void Awake()
        {
            m_buckets.AddRange( GetComponentsInChildren<PoolBucket>(true) );
        }

        public void FreeBuckets() {
            foreach (PoolBucket bucket in m_buckets)
                bucket.Free();
        }

        public void Despawn(PoolObject obj, float t)
        {
            if (t <= 0f)
                obj.Despawn();
            else
                StartCoroutine(DelayedReturnToPool(obj, t));
        }

        public void Despawn(PoolObject obj)
        {
            obj.Despawn();
        }

        public void Despawn (GameObject obj, float t)
        {
            if (t <= 0f)
                ReturnToPoolImmediate(obj);
            else
                StartCoroutine(DelayedReturnToPool(obj,t));
        }

        public void Despawn(GameObject obj)
        {
            ReturnToPoolImmediate(obj);
        }

        public PoolBucket GetBucket(string name)
        {
            foreach(PoolBucket bucket in m_buckets) {
                if (string.Equals(bucket.name, name))
                    return bucket;
            }
            return null;
        }

        public PoolBucket GetBucket(GameObject prefab, bool generate = true)
        {
            PoolBucket bucket = null;

            if (!m_bucketMap.ContainsKey(prefab))
            {
                if (generate)
                {
                    GameObject gbucket = new GameObject(prefab.name, typeof(PoolBucket));
                    gbucket.transform.SetParent(transform);
                    bucket = gbucket.GetComponent<PoolBucket>();
                    bucket.prefab = prefab;
                    bucket.tag = prefab.tag;
                    m_buckets.Add(bucket);
                    m_bucketMap.Add(prefab, bucket);
                }
            }
            else
            {
                bucket = m_bucketMap[prefab];
            }

            return bucket;
        }

        public void Generate(GameObject prefab, int count)
        {
            PoolBucket bucket = GetBucket(prefab);

            bucket.Generate(count);
        }

        public GameObject Spawn(GameObject prefab)
        {
            PoolBucket bucket = GetBucket(prefab);

            return Spawn(bucket);
        }

        public GameObject Spawn(GameObject prefab, Vector3 position)
        {
            PoolBucket bucket = GetBucket(prefab);

            GameObject obj = Spawn(bucket);

            if (obj != null)
            {
                obj.transform.position = position;
            }

            return obj;
        }

        public GameObject Spawn(string name, Vector3 position, Quaternion rotation)
        {
            PoolBucket bucket = GetBucket(name);

            if (bucket == null)
            {
                Debug.LogError("invalid bucket name '" + name + "'");
                return null;
            }

            GameObject obj = Spawn(bucket);

            if (obj != null) {
                obj.transform.position = position;
                obj.transform.rotation = rotation;
            }

            return obj;
        }

        public GameObject Spawn(string name)
        {
            PoolBucket bucket = GetBucket(name);

            if (bucket == null) {
                Debug.LogError("invalid bucket name '" + name + "'");
                return null;
            }

            return Spawn(bucket);
        }

        public GameObject Spawn(PoolBucket bucket)
        {
            PoolObject poolObject = bucket.Spawn();

            if (poolObject == null)
            {
                Debug.LogError("unable to spawn name '" + name + "'");
                return null;
            }

            return poolObject.gameObject;
        }

        IEnumerator DelayedReturnToPool(PoolObject obj, float delay)
        {
            yield return new WaitForSeconds(delay);
            {
                obj.Despawn();
            }
        }

        void ReturnToPoolImmediate(GameObject obj)
        {
            PoolObject poolObject = obj.GetComponent<PoolObject>();

            if (poolObject == null)
                return;

            poolObject.Despawn();
        }

        IEnumerator DelayedReturnToPool(GameObject obj, float delay) {

            yield return new WaitForSeconds(delay);
            {
                ReturnToPoolImmediate(obj);
            }
        }
    }
}