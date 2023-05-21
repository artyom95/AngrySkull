using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class SkullController : MonoBehaviour
{
    [SerializeField] 
    private TrajectoryController _trajectoryController;
    [SerializeField] 
    private GameObject _prefabSkulls;
    [SerializeField] 
    private Rigidbody2D _rigidbody2DSkull;

    [SerializeField] 
    private Rigidbody2D _pointConnectSkull;
    
    [SerializeField]
    private Transform _centrepoint;

    private float _speed = 5.0f;

    private int _countSkull = 2;
    const float _maxDistance = 2.4f;
    private GameObject _skull;
    private int _index = 0;
    private Vector3[] _points = new Vector3[30];
    private Vector3 _nextPosition;
    
    // Start is called before the first frame update
    void Start()
    {
        _skull = gameObject;
        GetComponentRigidbody();
    }

    public void SetPosition(Vector2 mousePosition)
    {
        if (_skull!= null)
        {
            if (Vector2.Distance(mousePosition, _pointConnectSkull.position) > _maxDistance)
            {
                _rigidbody2DSkull.position = _pointConnectSkull.position +
                                             (mousePosition - _pointConnectSkull.position).normalized * _maxDistance;
            }
            else
            {
                _rigidbody2DSkull.position = mousePosition;
            }
            
        }
        _rigidbody2DSkull.isKinematic = true;
    }
  

    public void LetSkullGo()
    {
        if (_skull != null)
        {
            _rigidbody2DSkull.isKinematic = false;
            StartCoroutine(LetGo());
        }
    }
    private IEnumerator LetGo()
    {
        
        yield return new WaitForSeconds(0.1f);
        _skull.GetComponent<SpringJoint2D>().enabled = false;
        Shoot();
        _skull = null;
         
        if (_skull == null && _countSkull !=0 )
        {
            yield return new WaitForSeconds(0.2f);
            _skull = Instantiate(_prefabSkulls);
            _skull.transform.position = _centrepoint.position;
            _skull.GetComponent<SpringJoint2D>().enabled = true;
            GetComponentRigidbody();
           _countSkull--; 
        }
    }

    private void GetComponentRigidbody()
    {
        _rigidbody2DSkull = _skull.GetComponent<Rigidbody2D>();
        _rigidbody2DSkull.isKinematic = false;
    }

   

    public GameObject GetSkull()
    {
        return _skull;
    }
    private void Shoot()
    {
        _rigidbody2DSkull.AddForce(_trajectoryController.GetSpeed()*5.0f);
      // MoveToFinish();
    }
  /*  public void MoveToFinish()
    {
        _points = _trajectoryController.GetPointsToForward();
        var time = 0f;
        while (_index < _points.Length)
        {
            time += Time.deltaTime;
            var distance = Vector3.Distance(transform.position, _nextPosition);
            var travelTime = distance / _speed;
            var progress = time / travelTime;
           _rigidbody2DSkull.transform.position =  Vector3.MoveTowards(transform.position, _nextPosition, _speed*Time.deltaTime); //умножить на вектор скорости
           
            if (_rigidbody2DSkull.transform.position == _nextPosition)
            {
                _index++;
                ChangeNextPosition();
                time = 0f;
            }
            
            
        }
        
    }
    
    private void ChangeNextPosition()
    {
        if (_index < _points.Length)
        {
            _nextPosition = _points[_index];
        }

    }*/
}
