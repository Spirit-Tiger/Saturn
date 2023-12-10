using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoUI : MonoBehaviour
{
    [SerializeField]
    private Transform _canvas;

    [SerializeField]
    private GameObject _bulletUI;

    [SerializeField]
    private Transform _startPoint;

    private Stack<GameObject> _bulletsStack = new Stack<GameObject>();
    private void OnEnable()
    {
        Shooting.OnAmmoChanged += UpdateAmmoUI;
    }

    private void OnDisable()
    {
        Shooting.OnAmmoChanged -= UpdateAmmoUI;
    }

    private void UpdateAmmoUI(int ammo)
    {
        if(_bulletsStack.Count - ammo == 1)
        {
            GameObject bulletUI =_bulletsStack.Pop();
            Destroy(bulletUI);
        }
        if(_bulletsStack.Count < ammo)
        {
            int diff = ammo - _bulletsStack.Count;
            for (int i = 0; i < diff; i++)
            {
                GameObject bulletUI = null;
                if (_bulletsStack.Count == 0)
                {
                    bulletUI = Instantiate(_bulletUI);
                    bulletUI.transform.SetParent(_canvas);
                    bulletUI.transform.position = _startPoint.position;
                    _bulletsStack.Push(bulletUI);
                }else if(_bulletsStack.Count > 0)
                {
                    bulletUI = Instantiate(_bulletUI);
                    bulletUI.transform.SetParent(_canvas);
                    Transform prevElementTransform = _bulletsStack.Peek().transform;
                    bulletUI.transform.position = prevElementTransform.position + new Vector3(prevElementTransform.position.y, 0, 0);
                    _bulletsStack.Push(bulletUI);
                }
             
            }
        }
    }
}
