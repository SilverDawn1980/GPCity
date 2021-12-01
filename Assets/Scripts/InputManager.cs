using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using DefaultNamespace;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour
{
    private const bool DEBUG = true;
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
        if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject() )
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
            var position = hit.transform.position;
            if (GameManager.getInstance().GridSystem.GameObjects
                .ContainsKey(new Vector2Int(((int)position.x), ((int)position.z))))
            {
                return;
            }
            GameObject newBuilding = GameObject.Instantiate(placeMe,
                    position, Quaternion.identity);
            GameManager.getInstance().GridSystem.GameObjects
                .Add(new Vector2Int(((int)position.x),((int)position.z)),newBuilding);
        }
    }

    private void PlaceRoad()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            var position = hit.transform.position;
            Vector2Int gridPosition = new Vector2Int((int)position.x, (int)position.z);

            PlaceNextRoad(gridPosition,true);
        }
        
        // Wieder diese Prüfung für alle angrenzenden Felder auf denen Straßen sind (max 1 mal)

    }

    private void PlaceNextRoad(Vector2Int gridPosition,bool is_recursive)
    {
        if (!is_recursive)
        {
            GameObject killME = GameManager.getInstance().GridSystem.GameObjects[gridPosition];
            GameManager.getInstance().GridSystem.GameObjects.Remove(gridPosition);
            Destroy(killME.gameObject);
        }
        StreetView nextRoad = calcRoadTileType(gridPosition);

        if (!GameManager.getInstance().GridSystem.GameObjects.ContainsKey(gridPosition))
        {
            GameObject newRoad = RoadFactory.getRoad(nextRoad);
            newRoad.transform.position = new Vector3(gridPosition.x, 0f, gridPosition.y);
            GameManager.getInstance().GridSystem.GameObjects.Add(gridPosition, newRoad);
        }

        if (is_recursive)
        {
            int recursiveCalls = CheckSourroundingsForRoads(gridPosition);
            if ((recursiveCalls - 8) >= 0)
            {
                recursiveCalls -= 8;
                if (DEBUG)
                {
                    Debug.Log("RefreshCall for "+ "(" +gridPosition.x+"/"+gridPosition.y+")");
                }
                PlaceNextRoad(gridPosition + Vector2Int.left,false);
            }
            if ((recursiveCalls - 4) >= 0)
            {
                recursiveCalls -= 4;
                if (DEBUG)
                {
                    Debug.Log("RefreshCall for "+ "(" +gridPosition.x+"/"+gridPosition.y+")");
                }
                PlaceNextRoad(gridPosition + Vector2Int.right,false);
            }
            if ((recursiveCalls - 2) >= 0)
            {
                recursiveCalls -= 2;
                if (DEBUG)
                {
                    Debug.Log("RefreshCall for "+ "(" +gridPosition.x+"/"+gridPosition.y+")");
                }
                PlaceNextRoad(gridPosition + Vector2Int.up,false);
            }
            if ((recursiveCalls - 1) >= 0)
            {
                recursiveCalls -= 1;
                if (DEBUG)
                {
                    Debug.Log("RefreshCall for "+ "(" +gridPosition.x+"/"+gridPosition.y+")");
                }
                PlaceNextRoad(gridPosition + Vector2Int.down,false);
            }
        }
        
        

    }

    private StreetView calcRoadTileType(Vector2Int key)
    {
        var connections = CheckSourroundingsForRoads(key);

        return connections switch
        {
            1 => StreetView.NS,
            2 => StreetView.NS,
            3 => StreetView.NS,
            4 => StreetView.WE,
            5 => StreetView.WN,
            6 => StreetView.SW,
            7 => StreetView.SWN,
            8 => StreetView.WE,
            9 => StreetView.NE,
            10 => StreetView.ES,
            11 => StreetView.NES,
            12 => StreetView.WE,
            13 => StreetView.WNE,
            14 => StreetView.ESW,
            15 => StreetView.CROSS,
            _ => StreetView.CROSS
        };
    }

    private static int CheckSourroundingsForRoads(Vector2Int key)
    {
        int connections = 0;
        if (CheckForRoad(key + Vector2Int.down))
        {
            connections += 1;
        }

        if (CheckForRoad(key + Vector2Int.up))
        {
            connections += 2;
        }

        if (CheckForRoad(key + Vector2Int.right))
        {
            connections += 4;
        }

        if (CheckForRoad(key + Vector2Int.left))
        {
            connections += 8;
        }

        if (DEBUG)
        {
            Debug.Log($"Checksum : {connections}");
        }

        return connections;
    }

    private static bool CheckForRoad(Vector2Int gridPosition)
    {
        return GameManager.getInstance().GridSystem.GameObjects.ContainsKey(gridPosition) 
               && GameManager.getInstance().GridSystem.GameObjects[gridPosition].CompareTag("Road");
    }

    #endregion

    #region ButtonControls

    public void setResidential()
    {
        _currentMode = new Mode(PlaceModeResidence);
        GameManager.getInstance().addResidence();
    }
    public void setCommercial()
    {
        _currentMode = new Mode(PlaceModeCommercial);
    }
    public void setIndustrial()
    {
        _currentMode = new Mode(PlaceModeIndustrial);
    }

    public void setRoad()
    {
        _currentMode = new Mode(PlaceRoad);
    }

    public void setSelectMode()
    {
        _currentMode = new Mode(HighlightMode);
    }

    #endregion
    
}
