using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class WinScreen : MonoBehaviour
{
    [SerializeField]
    private GameObject _winScreen;
   

    public void ShowScreen()
    {
        _winScreen.SetActive(true);
    }
    // Start is called before the first frame update
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
