using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ZombiesController : MonoBehaviour
{
    [SerializeField]
    private WinScreen _winScreen;
    private TextMeshProUGUI _enemyLabel;
   private int _countEnemys = 6;

    private void Start()
    {
        _enemyLabel = gameObject.GetComponent<TextMeshProUGUI>();
        if (_enemyLabel!=null)
        {
            _enemyLabel.text = _countEnemys.ToString();

        }
    }

    public void DecreaseEnemy()
    {
        _countEnemys =_countEnemys-1;
        if (_countEnemys < 0)
        {
            _countEnemys = 0;
        }
        _enemyLabel.text = " " + _countEnemys.ToString();
       Debug.Log(_countEnemys);
    }

    
    public bool HasAllEnemysDied()
    {
        if (_countEnemys <= 0)
        {
            return true;
        }

        return false;
    }
}
