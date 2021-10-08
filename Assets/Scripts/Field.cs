using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class Field
{
    private GameObject _buildGameObject;
    private TerrainType _terrain;

    public TerrainType Terrain
    {
        get
        {
            return _terrain;
        }
        set
        {
            _terrain = value;
        }
    }
    
    public GameObject BuildGameObject
    {
        get
        {
            return _buildGameObject;
        }
    }

    public bool IsBuild { get; }

    public bool buildHere(GameObject building)
    {
        if (!IsBuild)
        {
            _buildGameObject = building;
            return true;
        }

        return false;
    }
}
