using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Collect : MonoBehaviour
{
    private Camera _cam;

    [SerializeField]
    private TextMeshProUGUI _pressButtonText;

    [SerializeField]
    private float _hitRange;
    private LayerMask _interactableLayer;
    private RaycastHit _hit;

    private GameObject _hoveredItem;

    private void Awake()
    {
        _cam = Camera.main;
        _interactableLayer = LayerMask.GetMask("Interactable");
    }
    private void Update()
    {
        Ray ray = _cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        if (Physics.Raycast(ray, out _hit, _hitRange, _interactableLayer))
        {
            _pressButtonText.gameObject.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E))
            {
                _hit.collider.GetComponent<IInteractable>()?.Interact();
                _pressButtonText.gameObject.SetActive(false);
            }
        }
        else if(_pressButtonText.gameObject.activeSelf)
        {
            _pressButtonText.gameObject.SetActive(false);
        }
    }
}
