using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private RubberController _rubberController;

    [SerializeField] 
    private TrajectoryController _trajectoryController;

    [SerializeField]
    private SkullController _skullController;

    [SerializeField] 
    private ZombiesController _zombiesController;

    [SerializeField]
    private WinScreen _winScreen;
    
   
// Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (_skullController.GetSkull() != null)
            {
                _rubberController.DrawLines(GetMousePosition()); 
              _skullController.SetPosition(GetMousePosition());
                var pointInstantiateTrajectory = _skullController.GetSkull().transform.position;
                _trajectoryController.DrawTrajectory(pointInstantiateTrajectory);
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
           _rubberController.ResetLines();
          // _trajectoryController.ClearTrajectory();
           _skullController.LetSkullGo();
        }

        if (_zombiesController.HasAllEnemysDied())
        {
          _winScreen.ShowScreen();  
        }
    }
    private Vector2 GetMousePosition()
    {
        Vector3 mousePoint = Camera.main.ScreenToWorldPoint(Input.mousePosition); //заклампить!!!
       
     
        return  mousePoint;
    }
   
}
