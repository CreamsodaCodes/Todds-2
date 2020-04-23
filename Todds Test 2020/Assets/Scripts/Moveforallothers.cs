using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
public class Moveforallothers : MonoBehaviourPunCallbacks
{
    [Tooltip("The local player instance. Use this to know if the local player is represented in the Scene")]
    public static GameObject LocalPlayerInstance;
    private int ViewId;

    private Vector2 direction;
    GameObject FirstSpawn;
    private int Id = 1001;

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
            Moveforallothers.LocalPlayerInstance = this.gameObject;
        }
        // #Critical
        // we flag as don't destroy on load so that instance survives level synchronization, thus giving a seamless experience when levels load.
        DontDestroyOnLoad(this.gameObject);
    }

    private void Start() 
    {
        Id = 1001;
        ViewId = photonView.ViewID;

        if (ViewId == 1002 || ViewId == 1003 || ViewId == 1004 || ViewId == 1005 )
        {
            GameObject FirstSpawn = PhotonView.Find(Id).gameObject;
            CharacterList Script = FirstSpawn.GetComponent<CharacterList>();
            _characterList = Script;
            Script.CharaList.Add(ViewId);
            
        }

        if (ViewId == 2002 || ViewId == 2003 || ViewId == 2004 || ViewId == 2005 )
        {
            GameObject FirstSpawn =  PhotonView.Find(2001).gameObject;
            CharacterList Script = FirstSpawn.GetComponent<CharacterList>();
            _characterList = Script;
            Script.CharaList.Add(ViewId);

        }
        
        
        
        
        //CharacterList.CharaList.Add(ViewId);
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
