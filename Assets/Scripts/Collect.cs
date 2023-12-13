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

    private Vector3 _centerPoint = new Vector3(0.5f, 0.5f, 0);

    private void Awake()
    {
        _cam = Camera.main;
        _interactableLayer = LayerMask.GetMask("Interactable");
    }

    private void Start()
    {
        StartCoroutine(CollectableRay());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && _hit.collider != null)
        {
            _hit.collider.GetComponent<IInteractable>()?.Interact();
            _pressButtonText.gameObject.SetActive(false);
        }
    }

    private IEnumerator CollectableRay()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);

        while (true)
        {
            yield return wait;
            CollectableCheck();
        }
    }

    private void CollectableCheck()
    {
        Ray ray = _cam.ViewportPointToRay(_centerPoint);
        if (Physics.Raycast(ray, out _hit, _hitRange, _interactableLayer))
        {
            if (_pressButtonText.gameObject.activeSelf == false && GameManager.Instance.InNote == false)
            {
                _pressButtonText.gameObject.SetActive(true);
            }
        }
        if (_hit.collider == null && _pressButtonText.gameObject.activeSelf)
        {
            _pressButtonText.gameObject.SetActive(false);
        }

        if (_pressButtonText.gameObject.activeSelf && GameManager.Instance.InNote)
        {
            _pressButtonText.gameObject.SetActive(false);
        }
    }
}
