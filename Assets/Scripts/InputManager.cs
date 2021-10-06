using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] private GameObject _highlighter;

    private GameObject _highlighed;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (_highlighed != null)
                {
                    Destroy(_highlighed.gameObject);
                }
                _highlighed = GameObject.Instantiate(_highlighter, hit.transform.position, Quaternion.identity);
            }
        }    
    }
    
    
}
