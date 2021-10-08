using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class GridSystem
{
    private Field[,] _fields;

    public GridSystem()
    {
        initGrid();
    }

    public void initGrid()
    {
        _fields = new Field[10, 10];
        for (int x = 0; x < 10; x++)
        {
            for (int y = 0; y < 10; y++)
            {
                _fields[x, y] = new Field();
                _fields[x, y].Terrain = TerrainType.GRASS;
            }
        }
    }
}
