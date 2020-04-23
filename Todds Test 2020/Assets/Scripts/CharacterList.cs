using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
public class CharacterList : MonoBehaviourPunCallbacks
{
    public List<int> CharaList = new List<int>();
    private int Durchzähler = 0;
    public int currentControlled;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.IsMine == false && PhotonNetwork.IsConnected == true)
        {
            return;
        }
        
        string combindedString = string.Join( ",", CharaList.ToArray() );
        Debug.Log(currentControlled);
        if(Input.GetKeyDown(KeyCode.E))
        {
            Durchzähler = Durchzähler + 1;
            if(Durchzähler > 4){
                Durchzähler = 0;
            }
        }

        currentControlled = CharaList[Durchzähler];

    }
}
