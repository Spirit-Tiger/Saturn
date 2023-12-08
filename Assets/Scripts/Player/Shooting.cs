using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    private Camera _cam;

    [SerializeField]
    private GameObject _bullet;
    [SerializeField]
    private Transform _spawnPoint;
    [SerializeField]
    private float _bulletSpeed;
    [SerializeField]
    private float _bulletLifeTime;

/*    [SerializeField]
    private GameObject _particles;*/

    [SerializeField]
    private float _particlesLifeTime;

    [SerializeField]
    private int _ammo = 5;

    private AudioSource _audio;
    [SerializeField]
    private AudioClip _shootSound;
    [SerializeField]
    private AudioClip _emptySound;

    [SerializeField]
    private float _shootingDelay;
    private float _timer;
    private bool _isReadyToShoot = true;

    private void Awake()
    {
        _cam = Camera.main;
        _audio = GetComponent<AudioSource>();
        _timer = _shootingDelay;
    }
    private void Shoot()
    {
        if (_isReadyToShoot && _ammo > 0)
        {
            RaycastHit hit;

            if(Physics.Raycast(_cam.transform.position, transform.forward, out hit, 200f))
            {
                if (hit.collider.CompareTag("Wall"))
                {
                    GameObject hole = Instantiate(GlobalReferences.Instance.BulletHolePrefab, hit.point, Quaternion.LookRotation(hit.normal));
                    hole.transform.SetParent(hit.collider.gameObject.transform);
                }
            }

            _audio.PlayOneShot(_shootSound);
    /*        GameObject particles = Instantiate(_particles, _spawnPoint.position, Quaternion.identity);
            particles.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
            StartCoroutine(DestoyParticles(particles, _particlesLifeTime));*/

            _ammo -= 1;
            _isReadyToShoot = false;
        }

        if(_isReadyToShoot && _ammo == 0)
        {
            _audio.PlayOneShot(_emptySound);
        }
    }

/*    private IEnumerator DestoyParticles(GameObject particles, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(particles);
    }*/


    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Shoot();
        }

        if (!_isReadyToShoot && _timer > 0)
        {
            _timer -= Time.deltaTime;
        }
        else
        {
            _timer = _shootingDelay;
            _isReadyToShoot = true;
        }
    }

    public void LoadGun(int bulletsCount)
    {
        _ammo += bulletsCount;
    }
}
