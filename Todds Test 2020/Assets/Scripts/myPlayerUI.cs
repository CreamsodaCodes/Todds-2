using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class myPlayerUI : MonoBehaviour
{
    void LateUpdate()
    {
    // Do not show the UI if we are not visible to the camera, thus avoid potential bugs with seeing the UI, but not the player itself.
        if (targetRenderer!=null)
        {
            this._canvasGroup.alpha = targetRenderer.isVisible ? 1f : 0f;
        }


    // #Critical
    // Follow the Target GameObject on screen.
    if (targetTransform != null)
    {
        targetPosition = targetTransform.position;
        targetPosition.y += characterControllerHeight;
        this.transform.position = Camera.main.WorldToScreenPoint (targetPosition) + screenOffset;
    }
    }
    [Tooltip("UI Text to display Player's Name")]
    [SerializeField]
    private Text playerNameText;

    [Tooltip("Pixel offset from the player target")]

    private Vector3 screenOffset = new Vector3(-38f,40f,0f);

    private Movement target;
    float characterControllerHeight = 0f;
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
