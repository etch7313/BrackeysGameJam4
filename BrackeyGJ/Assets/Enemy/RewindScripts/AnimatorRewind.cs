using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorRewind : MonoBehaviour
{
    
    Animator anim;

    void Start()
    {
        anim = this.GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Return))
            RewindOneFrame();
        if (Input.GetKeyUp(KeyCode.Return))
            RewindOneFrame();
    }
    
    void RewindOneFrame(){
        AnimatorStateInfo animState = anim.GetCurrentAnimatorStateInfo(0);
        
        anim.Play(0, -1, animState.normalizedTime * animState.length);
        
//        if (anmimState.normalizedTime < 0)
//            anmimState.normalizedTime = 0;
        
    }
}
