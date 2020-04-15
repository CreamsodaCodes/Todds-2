using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
public class Movement : MonoBehaviourPunCallbacks
{
    private Vector2 direction;

    private void Update()
    {
        if (photonView.IsMine == false && PhotonNetwork.IsConnected == true)
        {
            return;
        }
        GetInput();
        MoveIt();
    }

    private void GetInput()
    {
        direction = Vector2.zero;
                if(Input.GetKeyDown(KeyCode.W)){
            direction += Vector2.up;
        }
        if(Input.GetKeyDown(KeyCode.A)){
            direction += Vector2.left;
        }
        if(Input.GetKeyDown(KeyCode.S)){
            direction += Vector2.down;
        }
        if(Input.GetKeyDown(KeyCode.D)){
            direction += Vector2.right;
            
        }
    }
    private void MoveIt()
    {
        transform.Translate(direction * 1);
    }
}

