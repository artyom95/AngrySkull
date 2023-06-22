using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField] private RubberController _rubberController;

    [SerializeField] private TrajectoryController _trajectoryController;

    [SerializeField] private SkullController _skullController;

    [SerializeField] private ZombiesController _zombiesController;

    [SerializeField] private WinScreen _winScreen;

    [SerializeField] private LayerMask _layer;
    private bool _isPlaceForClick = false;


// Update is called once per frame
    void Update()
    {
        if (CheckPlaceForClick())
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

                _isPlaceForClick = true;
            }
        }


        if (Input.GetMouseButtonUp(0) && _isPlaceForClick)
        {
            _rubberController.ResetLines();
            _trajectoryController.ClearTrajectory();
            _skullController.LetSkullGo();
            _isPlaceForClick = false;
        }

        if (_zombiesController.HasAllEnemysDied())
        {
            _winScreen.ShowScreen();
        }
    }

    private bool CheckPlaceForClick()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out var hitInfo))
        {
            if (hitInfo.collider.gameObject.CompareTag("Area"))
            {
                return true;
            }
        }

        return false;
    }

    private Vector2 GetMousePosition()
    {
        Vector3 mousePoint = Camera.main.ScreenToWorldPoint(Input.mousePosition); //заклампить!!!

        return mousePoint;
    }
}