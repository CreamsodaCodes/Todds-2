﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
public class Movement : MonoBehaviourPunCallbacks
{
    [Tooltip("The local player instance. Use this to know if the local player is represented in the Scene")]
    public static GameObject LocalPlayerInstance;

    private Vector2 direction;

    [Tooltip("The Player's UI GameObject Prefab")]
    [SerializeField]
    public GameObject PlayerUiPrefab;

    private void Awake() 
    {
        // #Important
        // used in GameManager.cs: we keep track of the localPlayer instance to prevent instantiation when levels are synchronized
        if (photonView.IsMine)
        {
            Movement.LocalPlayerInstance = this.gameObject;
        }
        // #Critical
        // we flag as don't destroy on load so that instance survives level synchronization, thus giving a seamless experience when levels load.
        DontDestroyOnLoad(this.gameObject);
    }

    private void Start() 
    {
        if (PlayerUiPrefab != null)
        {
            GameObject _uiGo =  Instantiate(PlayerUiPrefab);
            _uiGo.SendMessage ("SetTarget", this, SendMessageOptions.RequireReceiver);
        }
        else
        {
            Debug.LogWarning("<Color=Red><a>Missing</a></Color> PlayerUiPrefab reference on player Prefab.", this);
        }
    }
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

    //Das ist Improviesirt vlht kann das auch weg soll das UI nihct zerstören wenn eine neue Map geladen wird 
    public void CalledOnLevelWasLoaded()
    {
        GameObject _uiGo = Instantiate(this.PlayerUiPrefab);
        _uiGo.SendMessage("SetTarget", this, SendMessageOptions.RequireReceiver);
    }
    
}

