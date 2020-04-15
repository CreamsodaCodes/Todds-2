using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Controller : MonoBehaviour
{
    [SerializeField]  
    public Text countText;
    private Vector2 direction;
    private Vector2 Playerposition;
    private int count;
 /*void OnCollisionEnter2D(Collision2D other){
        if (other.gameObject.CompareTag ("Base")){
            count = count + 1;
            SetCountText();
            direction += Vector2.down;
            Move();
        }
        } */ 
  void OnTriggerStay2D(Collider2D other){
        if (other.gameObject.CompareTag ("Base")){
            count = count + 1;
            SetCountText();
            direction += Vector2.down;
            Move();
        }
        else if (other.gameObject.CompareTag ("Left")){
            count = count + 1;
            SetCountText();
            direction += Vector2.right;
            Move();
        }
         else if (other.gameObject.CompareTag ("Right")){
            count = count + 1;
            SetCountText();
            direction += Vector2.left;
            Move();
        }
         else if (other.gameObject.CompareTag ("Bottom")){
            count = count + 1;
            SetCountText();
            direction += Vector2.up;
            Move();
        }
        }

    void Start(){
        count = 20;
        SetCountText();    
    }
    private void Update() {
       GetInput();
       Move();
       
   }

   public void Move(){
     transform.Translate(direction*1);
     /*tarnsform = das menu oben links bei unity... Translate heißt glaub umwandeln oder so... direction ist die funktion in der wir die richtung bestimmen in dem wir nem vector + richtung rechnen und 
     dann * 1 für 1 feld in die richtung  */

   }


   private void GetInput(){

       direction = Vector2.zero;
       /* ohne des Zero wird des immer mehr also wenn man zweimal w drückt dann ist die richtung stärker als davor und wenn man dann einmal s drückt geht es nicht zurück also wie ne zahl die aber in vier richtungen 
       geht vlht x u nd y mit - und + */
       if(count > 0){
        if(Input.GetKeyDown(KeyCode.W)){
            direction += Vector2.up;
            count = count - 1;
            SetCountText();
        }
        if(Input.GetKeyDown(KeyCode.A)){
            direction += Vector2.left;
            count = count - 1;
            SetCountText();
        }
        if(Input.GetKeyDown(KeyCode.S)){
            direction += Vector2.down;
            count = count - 1;
            SetCountText();
        }
        if(Input.GetKeyDown(KeyCode.D)){
            direction += Vector2.right;
            count = count - 1;
            SetCountText();
        }
       }
       
       
   }
   void SetCountText(){
       countText.text = "Remaining Steps: " + count.ToString();
    }
    /* count.ToString = irgendeine variable zu dem Text */
}