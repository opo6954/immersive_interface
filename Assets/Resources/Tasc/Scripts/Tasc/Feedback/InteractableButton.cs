﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

namespace Tasc
{
    [RequireComponent(typeof(Interactable))]
    [RequireComponent(typeof(Tasc.Button))]
    public class InteractableButton : FeedbackSteamVR
    { 
        public override void Awake()
        {
            base.Awake();
            terminus = GetComponent<Button>();
        }

        public override void Proceed(Hand hand)
        {
            UpdateInControl(hand);
            if (isInControl)
            {
                Transform newTrans = terminus.Control(this.transform, hand.transform.position,hand.transform.rotation);
                this.transform.SetPositionAndRotation(newTrans.position, newTrans.rotation);
            }
        }

        public override void UpdateInstructions(List<Interface> interfaces)
        {
            for (int i = 0; i < interfaces.Count; i++)
            {
                terminus.UpdateInformation();
                interfaces[i].Transfer(terminus.information.GetContent(interfaces[i].context));
            }
        }
    }
}
