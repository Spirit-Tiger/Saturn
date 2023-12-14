using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    public float LadderSpeed;
    public GameObject HelpUI;

    private bool _playerNearLadder;
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            HelpUI.SetActive(true);
            _playerNearLadder = true;
            Debug.Log("PlayerIN");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            HelpUI.SetActive(false);
            _playerNearLadder = false;
            Debug.Log("PlayerOUT");
        }
    }

    private void Update()
    {
   
        if (_playerNearLadder && Input.GetKeyDown(KeyCode.E))
        {
            if (GameManager.Instance.IsOnLadder == false)
            {
                GameManager.Instance.LadderStartPosition = GameManager.Instance.Player.transform.position;
                GameManager.Instance.IsOnLadder = true;
                GameManager.Instance.CanAct = false;
                HelpUI.SetActive(false);
            }
            else if(GameManager.Instance.IsOnLadder == true)
            {
                GameManager.Instance.IsOnLadder = false;
                GameManager.Instance.CanAct = true;
            }
        }
    }
}
