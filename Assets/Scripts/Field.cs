using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class Field
{
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

}
