using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpOverWall : MonoBehaviour
{
    public Transform NewPosition;
    private bool _isPlaced;
    public void SetPlayerPosition()
    {
        GameManager.Instance.Player.transform.position = NewPosition.position; 
    }

    private void Update()
    {
        if (!_isPlaced)
        {
            GameManager.Instance.Player.transform.position = NewPosition.position;
            if(GameManager.Instance.Player.transform.position == NewPosition.position)
            {
                GameManager.Instance.Player.transform.GetChild(0).GetComponent<Animator>().Play("Idle");
                _isPlaced = true;
            }
        }
    }
}
