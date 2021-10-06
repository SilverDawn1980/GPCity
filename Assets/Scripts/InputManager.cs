using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private static GameObject _instance;

    public static InputManager getInstance()
    {
        if (_instance == null)
        {
            _instance = new GameObject("InputManager");
            _instance.AddComponent<InputManager>();
        }
        return _instance.GetComponent<InputManager>();
    }

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this.gameObject;
        }
        _currentInputState = new GameObject("HighLightMode").AddComponent<HighLightMode>();
    }

    [SerializeField] private GameObject _highlightPrefab;

    public GameObject HighlightedPrefab
    {
        get
        {
            return _highlightPrefab;
        }
    }
    
    private GameObject _highlighted;
    public GameObject HighLighted
    {
        get
        {
            return _highlighted;
        }
        set
        {
            if (_highlighted != null)
            {
                Destroy(_highlighted.gameObject);
            }
            _highlighted = value;
        }
    }
    
    private State _currentInputState;
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _currentInputState.doAction();
        }    
    }
    
    
}
