using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RealmGames.PoolSystem
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Rigidbody2DSpawner : MonoBehaviour
    {
        public GameObject prefab;
        public float force;
        public float delay;

        private float m_dt = 0f;

        private void OnEnable()
        {
            m_dt = 0f;
        }

        void Spawn()
        {
            GameObject spawnedObject = PoolManager.Instance.Spawn(prefab);

            if (spawnedObject == null)
                return;

            spawnedObject.transform.position = transform.position;
            spawnedObject.transform.LookAt(transform.position + transform.forward, new Vector3(0, 0, -1f));

            Rigidbody2D rb = spawnedObject.GetComponent<Rigidbody2D>();

            Vector3 forward = transform.forward;

            rb.AddForce(new Vector2(forward.x, forward.y) * force, ForceMode2D.Impulse);
        }

        // Update is called once per frame
        void Update()
        {
            m_dt += Time.deltaTime;

            if (m_dt >= delay)
            {
                m_dt = 0;
                Spawn();
            }
        }
    }
}