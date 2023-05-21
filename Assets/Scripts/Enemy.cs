using System;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : MonoBehaviour
{

    [SerializeField] 
    private ZombiesController _zombiesController;
    [SerializeField]
    private GameObject _explosionPrefab;
    [SerializeField] 
    private AudioClip _deathSound;

    private float _firstPosition;
    private float _currentPosition;
    private float _time;
    const float _speed = 3f;
    private float _currentSpeed;
    private float _rotation;
    private void Start()
    {
        _firstPosition = gameObject.GetComponent<Rigidbody2D>().velocity.y;
       
    }

    private void Update()
    {
        FindSpeedEnemy();
        _rotation = GetComponent<Rigidbody2D>().transform.rotation.z;
    
    }

   

    private void FindSpeedEnemy()
    {
        _currentPosition = gameObject.GetComponent<Rigidbody2D>().velocity.y;
        
            _time = Time.deltaTime;
            _currentSpeed = (_firstPosition - _currentPosition) * _speed;
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(GlobalConstants.SKULL_TAG))
        {
            Die();
        }

        if (collision.gameObject.CompareTag(GlobalConstants.WOOD_TAG) ||collision.gameObject.CompareTag(GlobalConstants.GROUND_TAG))
        {
            if (_currentSpeed > _speed)
            {
               // Debug.Log( "Currentspeed" + _currentSpeed);
               // Debug.Log("speed" + _speed);
                Die();
            }
            if (_rotation >= 0.1f || _rotation <= -0.1f )
            {
                Die();
            }

        }
        // TODO: Напишите логику уничтожения зомби тут
    }
    

    private void Die()
    {
        // Создаем эффект "взрыв" на месте убитого зомби.
        CreateExplosion();
        // ПРоигрываем звук смерти зомби.
        PlayDeathSound();
        // Разрушаем объект зомби.
        Destroy(gameObject);
        _zombiesController.DecreaseEnemy();
        
    }

    private void PlayDeathSound()
    {
        AudioSource.PlayClipAtPoint(_deathSound, transform.position);
    }
    
    private void CreateExplosion()
    {
        Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
    }
    
}