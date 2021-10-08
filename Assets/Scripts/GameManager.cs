using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private GridSystem _gridSystem = new GridSystem();
    [SerializeField] private GameObject _fieldParent;
    
    #region MapPrefabs

    [SerializeField] private GameObject _grassPrefab;
    
    
    #endregion
    
    // Start is called before the first frame update
    void Start()
    {
        for (int x = 0; x < 10; x++)
        {
            for (int y = 0; y < 10; y++)
            {
                GameObject.Instantiate(_grassPrefab, new Vector3(x, 0, y),Quaternion.identity,_fieldParent.transform);
            }    
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
