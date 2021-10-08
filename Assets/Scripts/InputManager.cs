using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class InputManager : MonoBehaviour
{

    private static GameObject _instance;
    private delegate void Mode();
    private Mode _currentMode;
    private GameObject _highlighted;
    
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

        _currentMode += new Mode(HighlightMode);
    }

    [SerializeField] private GameObject _highlightPrefab;

    public GameObject HighlightedPrefab
    {
        get
        {
            return _highlightPrefab;
        }
    }
    
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

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _currentMode?.Invoke();
        }    
    }

    private void HighlightMode()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            HighLighted = 
                GameObject.Instantiate(HighlightedPrefab,
                    hit.transform.position, Quaternion.identity);
        }
    }

    #region BuildingPlacement

    [SerializeField] private GameObject _residencePrefab;
    [SerializeField] private GameObject _commercialPrefab;
    [SerializeField] private GameObject _industrialPrefab;

    private void PlaceModeResidence()
    {
        PlaceBuilding(_residencePrefab);
    }

    private void PlaceModeCommercial()
    {
        PlaceBuilding(_commercialPrefab);
    }

    private void PlaceModeIndustrial()
    {
        PlaceBuilding(_industrialPrefab);
    }
    
    private void PlaceBuilding(GameObject placeMe)
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            HighLighted = 
                GameObject.Instantiate(placeMe,
                    hit.transform.position, Quaternion.identity);
        }
    }


    #endregion

    #region ButtonControls

    public void setResidential()
    {
        _currentMode = new Mode(PlaceModeResidence);
    }
    public void setCommercial()
    {
        _currentMode = new Mode(PlaceModeCommercial);
    }
    public void setIndustrial()
    {
        _currentMode = new Mode(PlaceModeIndustrial);
    }

    public void setSelectMode()
    {
        _currentMode = new Mode(HighlightMode);
    }

    #endregion
    
}
