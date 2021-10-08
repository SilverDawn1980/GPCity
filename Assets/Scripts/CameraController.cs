using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float _scrollSpeed = 4f;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.S))
        {
            transform.position += (new Vector3(_scrollSpeed * Time.deltaTime, 0f, _scrollSpeed * Time.deltaTime));
        }
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += (new Vector3(-_scrollSpeed * Time.deltaTime, 0f, -_scrollSpeed * Time.deltaTime));
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += (new Vector3(_scrollSpeed * Time.deltaTime, 0f, -_scrollSpeed * Time.deltaTime));
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += (new Vector3(-_scrollSpeed * Time.deltaTime, 0f, _scrollSpeed * Time.deltaTime));
        }
    }
}
