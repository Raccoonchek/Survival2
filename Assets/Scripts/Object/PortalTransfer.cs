using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalTransfer : MonoBehaviour
{
    [SerializeField] private string _nameScene;

    [SerializeField] private bool isVinerPanelOpen;
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<PlayerHP>() != null)
        {
            if(!isVinerPanelOpen)
            {
              SceneManager.LoadScene(_nameScene);

            }
            else
            {
                VinerManager.Instance.GenerateCard(_nameScene);
            }
        }
    }
}
