using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RealmGames.PoolSystem
{
    public class SpawnOnCollision : MonoBehaviour
    {
        public GameObject prefab;
        public bool setRelativePosition;

        private void OnCollisionEnter()
        {
            GameObject spawned = PoolManager.Instance.Spawn(prefab);

            if (setRelativePosition)
                spawned.transform.position = transform.position;
        }

        private void OnCollisionEnter2D()
        {
            GameObject spawned = PoolManager.Instance.Spawn(prefab);

            if (setRelativePosition)
                spawned.transform.position = transform.position;
        }
    }
}