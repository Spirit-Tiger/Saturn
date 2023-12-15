using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{
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
       
        if (_playerNearLadder  && Input.GetKeyDown(KeyCode.E))
        {
            if (GameManager.Instance.IsOnLadder == false)
            {
                GameManager.Instance.CurrentLadder = transform.gameObject;
                if (transform.position.y < GameManager.Instance.Player.transform.position.y)
                {
                    GameManager.Instance.IsEnteringLadderDown = true;
                    HelpUI.SetActive(false);
                    GameManager.Instance.CanAct = false; 
                    GameManager.Instance.IsOnLadder = true;
                    GameManager.Instance.CurrentLadder = transform.gameObject;
                }
                if (!GameManager.Instance.IsEnteringLadderDown && Vector3.Angle(transform.forward, GameManager.Instance.CameraPoint.transform.forward) < 35)
                {
                    GameManager.Instance.LadderStartPosition = GameManager.Instance.Player.transform.position;
                    HelpUI.SetActive(false);
                    GameManager.Instance.CanAct = false;
                    GameManager.Instance.IsOnLadder = true;
                    GameManager.Instance.CurrentLadder = transform.gameObject;
                }
               
            }
            else if(GameManager.Instance.IsOnLadder == true && transform.position.y > GameManager.Instance.Player.transform.position.y)
            {
                GameManager.Instance.IsOnLadder = false;
                GameManager.Instance.CanAct = true;
            }
        }
    }
}
