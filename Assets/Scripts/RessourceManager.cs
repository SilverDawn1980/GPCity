using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class RessourceManager
{
    private int _money;
    private int _people;
    private int _work;
    private int _happyness;
    
    private static RessourceManager _instance;

    public static RessourceManager GetRessourceManager()
    {
        if (_instance == null)
        {
            _instance = new RessourceManager();
        }
        return _instance;
    }

    private RessourceManager()
    {
        
    }

}
