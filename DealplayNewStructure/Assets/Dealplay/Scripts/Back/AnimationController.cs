using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;


public class AnimationController : MonoBehaviour
{
    public Animator anim;

    public void TriggerNPCAnimation(int animID) {
        switch(animID) {
            case 0:
                anim.SetTrigger("Idle");
            break;
            case 1:
                anim.SetTrigger("Talk1");
            break;
            case 2:
                anim.SetTrigger("Talk2");
            break;
            case 3:
                anim.SetTrigger("Talk2");
            break;
            case 4:
                anim.SetTrigger("Action1");
            break;
            case 5:
                anim.SetTrigger("Action2");
            break;
            case 6:
                anim.SetTrigger("Action3");
            break;
            case 7:
                anim.SetTrigger("Action4");
            break;
            default:
                anim.SetTrigger("Idle");
            break;
        }
    }
}
