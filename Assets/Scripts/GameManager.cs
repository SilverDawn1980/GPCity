using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    private const Boolean DEBUG = true;
    private static GameManager _instance;

    private GameManager()
    {

    }

    public static GameManager getInstance()
    {
        if (_instance == null)
        {
            _instance = Instantiate(new GameObject("GameManager").AddComponent<GameManager>());
            DontDestroyOnLoad(_instance);
        }

        return _instance;
    }

    private int _money;

    public int Money
    {
        get { return _money; }
        set
        {
            _money = value;
            _moneyDisplay.text = _money.ToString() + "$";
        }
    }


    private GridSystem _gridSystem = new GridSystem();

    public GridSystem GridSystem
    {
        get => _gridSystem;
        set => _gridSystem = value;
    }

    private Text _moneyDisplay;

    #region MapPrefabs

    [SerializeField] private GameObject _grassPrefab;

    public GameObject GrassPrefab
    {
        get => _grassPrefab;
    }

    public GameObject RoadStraightPrefab { get; set; }
    public GameObject RoadCornerPrefab { get; set; }
    public GameObject RoadJunctionPrefab { get; set; }
    public GameObject RoadCrossPrefab { get; set; }

    #endregion

    private float _timePerTick = 1;
    private float _timeUntilTick;

    private delegate void TickFunct();

    private TickFunct _stuffForTick;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (_timeUntilTick <= 0)
        {
            _stuffForTick?.Invoke();
            _timeUntilTick = _timePerTick;
        }
        else
        {
            _timeUntilTick -= Time.deltaTime;
        }
    }

    public void addResidence()
    {
        _stuffForTick += new TickFunct(ResidenceTick);
    }

    private void ResidenceTick()
    {
        Money += 5;
    }

    #region Init

    public void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }

        _moneyDisplay = GameObject.Find("MoneyDisplay").GetComponent<Text>();
        _grassPrefab = Resources.Load("Prefabs/Grass", typeof(GameObject)) as GameObject;
        RoadStraightPrefab = Resources.Load("Prefabs/RoadStraight", typeof(GameObject)) as GameObject;
        RoadJunctionPrefab = Resources.Load("Prefabs/RoadJunction", typeof(GameObject)) as GameObject;
        RoadCornerPrefab = Resources.Load("Prefabs/RoadCorner", typeof(GameObject)) as GameObject;
        RoadCrossPrefab = Resources.Load("Prefabs/RoadCross", typeof(GameObject)) as GameObject;
        if (DEBUG)
        {
            CheckForNullPointerLoad(_grassPrefab);
            CheckForNullPointerLoad(RoadStraightPrefab);
            CheckForNullPointerLoad(RoadJunctionPrefab);
            CheckForNullPointerLoad(RoadCornerPrefab);
            CheckForNullPointerLoad(RoadCrossPrefab);
        }
    }

    private void CheckForNullPointerLoad(GameObject target)
    {
        Debug.Log("Object GameManager ID :" + this.GetHashCode());
        if (target == null)
        {
            Debug.Log("Error loading :" + target.name);
        }
    }

    #endregion
}