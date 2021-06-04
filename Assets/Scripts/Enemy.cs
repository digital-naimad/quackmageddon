using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Quackmageddon
{
    public class Enemy : MonoBehaviour, IPooledObject
    {
        public float upForce = 50f;
        public float sideForce = 5f;

        /**
         * */
        public void OnSpawn()
        {
            float xForce = Random.Range(-sideForce, sideForce);
            float yForce = Random.Range(upForce / 2f, upForce);
            float zForce = Random.Range(-sideForce, sideForce);

            Vector3 force = new Vector3(xForce, yForce, zForce);
            GetComponent<Rigidbody>().velocity = force;
        }

        
        void Update()
        {

        }


    }
}
