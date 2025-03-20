using UnityEngine;
using YG;


namespace PhysicsCharacterController
{
    public class LockCursor : MonoBehaviour
    {
        public bool lockCursor = false;
        [SerializeField] private GameObject _mobileJoustick;

        /**/


        private void Awake()
        {
            if (YG2.envir.isDesktop)
            {
                if (lockCursor) Cursor.lockState = CursorLockMode.Locked;
                _mobileJoustick.SetActive(false);
            }
            else
            {
                _mobileJoustick.SetActive(true);
                if (lockCursor) Cursor.lockState = CursorLockMode.Confined;
            }
        }
    }
}