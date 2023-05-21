using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.Serialization;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class TrajectoryController : MonoBehaviour
{
     [SerializeField]
    private Transform _centrePoint;

    

    [SerializeField]
    private GameObject _prefab;

    private GameObject[] _gameObjects = new GameObject[30];
  
    private bool _isDrawLine = false;
    private Vector3 _skullPosition;
  
    const float _startSpeed = 8.0f;
    private Vector3 _speed;
    private float _gravity = 9.81f;
   
    private Vector3[] _pointsToForward = new Vector3[30] ;
   
    // Start is called before the first frame update
   private void Instantiate()
    {
        for (int i = 0; i < _gameObjects.Length; i++)
        {
            _gameObjects[i] = Instantiate(_prefab);
        }
        _isDrawLine = true;
    }

    public void DrawTrajectory(Vector2 mouseposition)
    {
        _skullPosition = mouseposition;
        if (!_isDrawLine)
        {
            Instantiate( );
        }
        _gameObjects[0].transform.position = new Vector3(mouseposition.x + 0.1f, mouseposition.y+0.1f) ;
        _pointsToForward[0] = _gameObjects[0].transform.position;
        for (int i = 1; i < _gameObjects.Length; i++) 
        {
           float time = 0.1f * i;
           _gameObjects[i].transform.position  =_gameObjects[0].transform.position + GetGameObjectPosition(time);
           _pointsToForward[i] = _gameObjects[i].transform.position;
        }
    }
    
    private Vector3 GetGameObjectPosition(float time)
    {
       var direction = GetDirection();
       _speed = direction * _startSpeed;
        float angle = Mathf.Atan2(direction.y, direction.x);
        var x = GetPositionX(time, angle); 
        var y = GetPositionY(time, angle); 
       return new Vector3(x, y, 0);
        
    }

    private Vector3 GetDirection()              // изменить код как в gamecontroller
    {
        var mousePoint = _skullPosition;
        Vector3 direction = _centrePoint.position - mousePoint;
        return direction;
    }
    private float GetPositionX( float time, float angle )
    {
        var X = (float)Math.Sqrt( _speed.x*_speed.x + _speed.y*_speed.y)* time *(float)Math.Cos(angle);
        return X;
    }
    private float GetPositionY( float time,  float angle )
    {
        var Y =  (float)Math.Sqrt( _speed.x*_speed.x + _speed.y*_speed.y) * time *(float)Math.Sin(angle) - _gravity*time*time/2f;
        return Y  ;
    }
    
    public void ClearTrajectory ()
    {
        foreach (var gameObject in _gameObjects)
        {
            Destroy(gameObject);
        }

        _isDrawLine = false;
    }

    public Vector3 GetSpeed()
    {
        return _speed ;
    }

    public Vector3[] GetPointsToForward()
    {
        return _pointsToForward;
    }
   /* private void FillArrayPoint()
    {
        _points =  new Vector3[30];
        var mousePoint = GetMousePosition();
        //  _lineRenderer.positionCount = _points.Length;
        Vector3 direction = _centrePoint.position - mousePoint;
        float angle = Mathf.Atan2(direction.y, direction.x);
        _points[0] = new Vector3(_centrePoint.position.x, _centrePoint.position.y, 0);
        for (int i = 1; i < _points.Length; i++)
        {
            float time = 0.1f * i;
            var x = GetPositionX(time, angle);
            var y = GetPositionY(time, angle);
            _points[i] = new Vector3(x,y,0f);
            if (_points[i].y < -2.0f)
            {
                _lineRenderer.positionCount = i + 1;
                break;
            }
        }
        //  _lineRenderer.SetPositions(_points);
    }*/
}
