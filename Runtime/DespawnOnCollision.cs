using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RealmGames.PoolSystem
{
    public class DespawnOnCollision : MonoBehaviour
    {
        public void Despawn() {
            PoolManager.Instance.Despawn(gameObject);
        }

        public void OnCollisionEnter()
        {
            Despawn();
        }

        public void OnCollisionEnter2D()
        {
            Despawn();
        }
    }
}