using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Field
{
    private GameObject _buildGameObject;

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
