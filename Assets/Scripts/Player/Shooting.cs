using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.VFX;

public class Shooting : MonoBehaviour
{
    private Camera _cam;

    [SerializeField]
    private Transform _spawnPoint;

    [SerializeField]
    private VisualEffect _shootFireEffect;
    [SerializeField]
    private GameObject _shootLight;

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

    public static event Action<int> OnAmmoChanged;

    private void Awake()
    {
        _cam = Camera.main;
        _audio = GetComponent<AudioSource>();
        _timer = _shootingDelay;
    }

    private void Update()
    {
        if (GameManager.Instance.CanAct)
        {
            if (Input.GetMouseButtonDown(0) && GameManager.Instance.CanShoot)
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
    }
    private void Shoot()
    {
        if (_isReadyToShoot && _ammo > 0)
        {
            Ray ray = _cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
            RaycastHit hit;

            if (Physics.Raycast(ray,out hit, 200f))
            {
                if (hit.collider.CompareTag("Wall"))
                {
                    GameObject hole = Instantiate(GlobalReferences.Instance.BulletHolePrefab, hit.point, Quaternion.LookRotation(hit.normal));
                    hole.transform.SetParent(hit.collider.gameObject.transform);
                }
            }
            _shootFireEffect.Play();
            _shootLight.SetActive(true);
            _audio.PlayOneShot(_shootSound);
            _ammo -= 1;
            OnAmmoChanged?.Invoke(_ammo);
            _isReadyToShoot = false;
        }

        if (_isReadyToShoot && _ammo == 0)
        {
            _audio.PlayOneShot(_emptySound);
        }
    }

    public void LoadGun(int bulletsCount)
    {
        _ammo += bulletsCount;
        OnAmmoChanged?.Invoke(_ammo);
    }
}
