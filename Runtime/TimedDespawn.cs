using UnityEngine;
using System.Collections;

namespace RealmGames.PoolSystem
{
    public class TimedDespawn : MonoBehaviour
    {
        public float delay;

        private float m_dt;

        void OnEnable()
        {
            m_dt = 0f;
        }

        private void Update()
        {
            m_dt += Time.deltaTime;

            if(m_dt >= delay) {
                PoolManager.Instance.Despawn(gameObject);
            }
        }
    }
}