using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class SimpleAttach : MonoBehaviour
{
    private Interactable Interactable;
    // Start is called before the first frame update
    void Start()
    {
        Interactable = GetComponent<Interactable>();
    }


    void onHandHoverBegin(Hand hand)
    {
        hand.ShowGrabHint();
    }
    void onHandHoverEnd(Hand hand)
    {
        hand.HideGrabHint();
    }
    void onHandHoverUpdate(Hand hand)
    {
        GrabTypes grabType = hand.GetGrabStarting();
        bool isGrabbingEnding = hand.IsGrabEnding(gameObject);

        if (Interactable.attachedToHand == null && grabType != GrabTypes.None)
        {
            hand.AttachObject(gameObject, grabType);
            hand.HoverLock(Interactable);
            hand.HideGrabHint();
        }
        else if (isGrabbingEnding)
        {
            hand.DetachObject(gameObject);
            hand.HoverUnlock(Interactable);

        }
    }
}
