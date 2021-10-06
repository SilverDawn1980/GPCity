using UnityEngine;

namespace DefaultNamespace
{
    public class HighLightMode : MonoBehaviour, State
    {
        public void doAction()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                InputManager.getInstance().HighLighted = 
                    GameObject.Instantiate(InputManager.getInstance().HighlightedPrefab,
                        hit.transform.position, Quaternion.identity);
            }
        }
    }
}