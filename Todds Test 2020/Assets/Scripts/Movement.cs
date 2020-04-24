using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
public class Movement : MonoBehaviourPunCallbacks
{
    [Tooltip("The local player instance. Use this to know if the local player is represented in the Scene")]
    public static GameObject LocalPlayerInstance;
    private int ViewId;
    private Vector2 direction;

    private CharacterList _characterList;


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
        GameObject FirstSpawn = this.gameObject;
        CharacterList Script = FirstSpawn.GetComponent<CharacterList>();
        _characterList = Script;
        ViewId = photonView.ViewID;
        Script.CharaList.Add(ViewId);
        if (PlayerUiPrefab != null)
        {
            GameObject _uiGo =  Instantiate(PlayerUiPrefab);
            _uiGo.SendMessage ("SetTarget", this, SendMessageOptions.RequireReceiver);
        }
        else
        {
            Debug.LogWarning("<Color=Red><a>Missing</a></Color> PlayerUiPrefab reference on player Prefab.", this);
        }

        if (photonView.IsMine)
        {
            var GameObjectRenderer = FirstSpawn.GetComponent<Renderer>();
            GameObjectRenderer.material.SetColor("_Color", Color.red);
        }
    }
    private void Update()
    {
        if (photonView.IsMine == false && PhotonNetwork.IsConnected == true)
        {
            return;
        }

        if (photonView.ViewID !=  _characterList.currentControlled)
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
        if(Input.GetKeyDown(KeyCode.Q)){
            PhotonNetwork.Instantiate("My otherTest Player", new Vector3(0.5f,0.5f,0f), Quaternion.identity, 0);
            
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

