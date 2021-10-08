using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private int _money;

    public int Money
    {
        get
        {
            return _money;
        }
        set
        {
            _money = value;
            _moneyDisplay.text = _money.ToString() + "$";
        }
    }
    
    
    
    private GridSystem _gridSystem = new GridSystem();
    [SerializeField] private GameObject _fieldParent;
    [SerializeField] private Text _moneyDisplay;
    
    #region MapPrefabs

    [SerializeField] private GameObject _grassPrefab;
    
    
    #endregion

    private float _timePerTick = 1;
    private float _timeUntilTick;
    private delegate void TickFunct();

    private TickFunct _stuffForTick;
    
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

        this.Money = 500;
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
}
