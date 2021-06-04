using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Quackmageddon
{
    public class Enemy : MonoBehaviour, IPooledObject
    {
        #region Static members

        public static readonly string TagName = "Duckie";

        #endregion

        #region properties & fields

        /// <summary>
        /// Property.
        /// Note: caches rigid body component in private field
        /// </summary>
        public Rigidbody RigidBody
        {
            get
            {
                if (this._rigidBody == null)
                {
                    this._rigidBody = GetComponent<Rigidbody>();
                }
                return this._rigidBody;
            }
        }

        public float upForce = 50f;
        public float sideForce = 5f;

        private Rigidbody _rigidBody = null;

        #endregion

        #region Life-cycle callbacks

        void Awake()
        {
            //Debug.Log("Awake!");

        }

        void Update()
        {

        }

        #endregion

        #region IPooledObject interface's methods implementation

        /// <summary>
        /// 
        /// </summary>
        public void OnSpawn()
        {
           /* 
           float xForce = Random.Range(-sideForce, sideForce);
           float yForce = Random.Range(upForce / 2f, upForce);
           float zForce = Random.Range(-sideForce, sideForce);


           Vector3 force = new Vector3(xForce, yForce, zForce);
            
           GetComponent<Rigidbody>().velocity = force;
           */

        }

        #endregion

        public void OnCollisionEnter(Collision collision)
        {
            Debug.Log("COLLIDED");
        }

    }
}
