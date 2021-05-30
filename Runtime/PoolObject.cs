using System;
using UnityEngine;

namespace RealmGames.PoolSystem
{
    public class PoolObject : MonoBehaviour
    {
        public PoolBucket bucket;
        public bool allocated;

        public void Despawn()
        {
            if (bucket.setAsParent)
                transform.SetParent(bucket.transform);

            allocated = false;

            gameObject.SetActive(false);
        }
    }
}