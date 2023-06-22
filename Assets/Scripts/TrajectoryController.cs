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
    private Rigidbody2D _rigidbody2DSkull;

    [SerializeField]
    private GameObject _prefab;

    private GameObject[] _gameObjects = new GameObject[30];
  
    private bool _isDrawLine = false;
    private Vector3 _skullPosition;
  
    const float _startSpeedForTrajectory = 8.0f;
    private const float _startSpeedForSkull = 4.2f;
    
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
        _gameObjects[0].transform.position = new Vector3(mouseposition.x , mouseposition.y) ;
        _pointsToForward[0] = _gameObjects[0].transform.position;
        float time =0 ;
        for (int i = 1; i < _gameObjects.Length; i++) 
        {
          
           _gameObjects[i].transform.position  =_gameObjects[0].transform.position + GetGameObjectPosition(time);
           _pointsToForward[i] = _gameObjects[i].transform.position;
           time = 0.1f * i;
        }
    }
    
    private Vector3 GetGameObjectPosition(float time)
    {
       var direction = GetDirection();
       _speed = direction * _startSpeedForTrajectory ;
        float angle = Mathf.Atan2(direction.y, direction.x);
        var x = GetPositionX(time, angle); 
        var y = GetPositionY(time, angle); 
       return new Vector3(x, y, 0);
        
    }

    private Vector3 GetDirection()              // изменить код как в gamecontroller
    {
        var mousePoint = _skullPosition;
        Vector3 direction = (_centrePoint.position - mousePoint);
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
         var speed = GetDirection() * _startSpeedForSkull  ;

        return speed ;
    }

    public Vector3[] GetPointsToForward()
    {
        return _pointsToForward;
    }
  
}
