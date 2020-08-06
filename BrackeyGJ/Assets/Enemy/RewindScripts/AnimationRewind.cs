using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationRewind : MonoBehaviour
{
    // Rewinds a GameObject's Animation Component
    // No extra memory is needed, only Math :D
    // The script currently supports one clip rewind because of the stupid Unity Scripting Engine :D

    public bool rewind = false;
    
    // decides what to do when the animation clip is rewinded to the first keyframe
    // Set false to stop rewinding at time == 0
    // Set true to start rewinding from the end of the clip
    public bool loopRewind = false;

    Animation anim;
    AnimationState animState;

    void Start()
    {
        anim = this.GetComponent<Animation>();
        
        
        // Resets the default animation clip to the last animation clip in the animation component's array of clips
        // anim.clip ---> the default clip

        foreach (AnimationState state in anim)
            animState = state;

        anim.clip = animState.clip;
    }

    void Update(){
		if (Input.GetKeyDown(KeyCode.Return))
			rewind = true;

		else if (Input.GetKeyUp(KeyCode.Return))
			rewind = false;
         
    }
    
    void FixedUpdate()
    {
        if (rewind) RewindOneFrame();

    /*
        Negating the Speed gives the same effect as if Loop Rewind was set to True,
        however, you will lose an important property if you do it this way.
        Negating the speed won't tell you how many times the animation was played (looped) before rewinding,
        and thus you can't rewind the clip correctly to the initial state.
        
        let's say the animation clip was played 3 times before rewinding, 
        and you want to rewind 3 times only then stop rewinding.. 
        the only way is to manipulate the animation clip time yourself..
        
        I will keep the code here for a while.. just in case xD
    
        if (rewind) animState.speed = -1.0f;
        else animState.speed = 1.0f;
    */
    }
    
    void RewindOneFrame(){
        animState.time -= 2*Time.fixedDeltaTime;
        
        if (animState.time < 0){
            if (loopRewind) {
                animState.time += animState.length;
            }
            
            else {
                animState.time = 0f; rewind = false;
            }
        }    
    }
}


