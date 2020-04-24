using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
public class Menu : MonoBehaviourPunCallbacks
{
    GameObject Merken;
    int MenuNrs;
    public GameObject Paper1;

    public GameObject Paper2;

    public GameObject Paper3;

   public GameObject Paper4;

    public void SwitchMenu()
    {
        if(MenuNrs == 4)
        {
            MenuNrs = 0;
            MenuAktivator();
            return;
        }
        MenuNrs = MenuNrs + 1;
        MenuAktivator();


    
    }

    private void MenuAktivator()
    {
        if (MenuNrs == 0)
        {
            Paper1.SetActive(false);
            Paper2.SetActive(false);
            Paper3.SetActive(false);
            Paper4.SetActive(false);
                        

            
        }
        if (MenuNrs == 1)
        {
            Paper1.SetActive(true);

            

            
        }
        if (MenuNrs == 2)
        {
            Paper1.SetActive(false);
            Paper2.SetActive(true);

            
        }
        if (MenuNrs == 3)
        {
          

            Paper2.SetActive(false);
            Paper3.SetActive(true);

        }
        if (MenuNrs == 4)
        {

            Paper3.SetActive(false);
            Paper4.SetActive(true);
            
        }

        
    }
    
}
