using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class myPlayerUI : MonoBehaviour
{
[Tooltip("UI Text to display Player's Name")]
[SerializeField]
private Text playerNameText;

[Tooltip("Pixel offset from the player target")]
[SerializeField]
private Vector3 screenOffset = new Vector3(0f,30f,0f);

private Movement target;

Transform targetTransform;
Renderer targetRenderer;
CanvasGroup _canvasGroup;
Vector3 targetPosition;

private void Update() 
{
    if (target == null)
    {
        Destroy(this.gameObject);
        return;
    }    
}

public void SetTarget(Movement _target)
{
    if (_target == null)
    {
        Debug.LogError("<Color=Red><a>Missing</a></Color> PlayMakerManager target for PlayerUI.SetTarget.", this);
        return;
    }
    // Cache references for efficiency
    target = _target;
    targetTransform = this.target.GetComponent<Transform>();
    targetRenderer = this.target.GetComponent<Renderer>();
    if (playerNameText != null)
    {
        playerNameText.text = target.photonView.Owner.NickName;
    }
}
void Awake()
{
    this.transform.SetParent(GameObject.Find("Canvas").GetComponent<Transform>(), false);
    _canvasGroup = this.GetComponent<CanvasGroup>();
}

}
