using UnityEngine;

namespace Quackmageddon
{
    /// <summary>
    /// Simply UI helper class making GameObject looking at given camera
    /// </summary>
    public class BillboardView : MonoBehaviour
    {
        /// <summary>
        /// Note: it has to be set when GameObject is instantieted
        /// </summary>
        public Camera cameraToLookAt = null;

        private void LateUpdate()
        {
            if (cameraToLookAt != null)
            {
                transform.LookAt(transform.position + cameraToLookAt.transform.forward);
            }
        }
    }
}
