using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class RubberController : MonoBehaviour
{
    [SerializeField] 
    private Transform _frontpoint;
    [SerializeField] 
    private Transform _backpoint;
    [SerializeField]
    private Transform _centrepoint;
   
    
    [SerializeField]
    private LineRenderer _firstLineRenderer;
    [SerializeField]
    private LineRenderer _secondLineRenderer;

    private Vector3 _finishPoint;
    const float _maxLenght = 2.2f;

    // Start is called before the first frame update
    void Start()
    {
         _firstLineRenderer.positionCount = 2;
        _secondLineRenderer.positionCount = 2;
       
    }

   

    public void DrawLines(Vector2 mousePosition)
    {
        GetMousePosition(mousePosition);
        _firstLineRenderer.SetPosition(0,new Vector3(_frontpoint.position.x, _frontpoint.position.y, _frontpoint.position.z));
        _firstLineRenderer.SetPosition(1,new Vector3(_finishPoint.x, _finishPoint.y, _finishPoint.z));
        _secondLineRenderer.SetPosition(1,new Vector3(_finishPoint.x, _finishPoint.y, _finishPoint.z));
      
        _secondLineRenderer.SetPosition(0,new Vector3(_backpoint.position.x, _backpoint.position.y, _backpoint.position.z));

    }

    public void ResetLines()
    {
        _firstLineRenderer.SetPosition(1,new Vector3(_centrepoint.position.x, _centrepoint.position.y, _centrepoint.position.z));
        _secondLineRenderer.SetPosition(1,new Vector3(_centrepoint.position.x, _centrepoint.position.y, _centrepoint.position.z));

    }
    private void GetMousePosition(Vector2 mousePosition)
    {
        _finishPoint =mousePosition;
        if (Vector3.Distance(_finishPoint,_centrepoint.position) >  _maxLenght)
        {
            _finishPoint = _centrepoint.position + (_finishPoint - _centrepoint.position).normalized * _maxLenght;
        }
    }
}
