using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRecorder : MonoBehaviour {

	public float recordTime = 120f;

	private bool forwardPlay = true; // if set to false, rewinds
    public bool playRecordedStates = false;// whether we should record or play
	playerBlend playerBlendComponent;
	Vector3 initialPosition;
	Quaternion initialRotation;

	LinkedListNode<PlayerState> currentState = null;
	LinkedList<PlayerState> playerStates;
	
	void Start() {
		
		playerStates = new LinkedList<PlayerState>();
		playerBlendComponent = this.GetComponent<playerBlend>();

		initialPosition = transform.position;
		initialRotation = transform.rotation;
	}

    void Update () {
		if (Input.GetKeyDown(KeyCode.Return)){
			changeTimeFlowDirction(false);

			if (forwardPlay){
				currentState = playerStates.First;
				transform.position = initialPosition;
				transform.rotation = initialRotation;
				playerBlendComponent.shootNow = false;
				playerBlendComponent.shootPrev = false;

			} else{
			    currentState = playerStates.Last;
			}
		}
        
		if (Input.GetKeyUp(KeyCode.Return)){
			changeTimeFlowDirction(true);
		}
			
	}

	void FixedUpdate ()
	{
		if (playRecordedStates) {
			if (forwardPlay) playForward();
			else playBackward();
		}
		else Record();
	}
	
	void playState(PlayerState state){
		playerBlendComponent.headpivot.transform.rotation = state.headpivotRotation;
		playerBlendComponent.forwardSpeed = state.forwardSpeed;
		playerBlendComponent.rightSpeed = state.rightSpeed;
		playerBlendComponent.shootNow = state.shootNow;
	}

	void playForward(){
		if (currentState != null){
			playState(currentState.Value);
			currentState = currentState.Next;
		}
		
		else{
            changeTimeFlowDirction(true);
        }
	}
	void playBackward(){
		// Broken /////////////////////////////////////////////////////////////
		if (currentState != null){
			Vector3 angle = currentState.Value.headpivotRotation.eulerAngles;
			PlayerState backwardState = new PlayerState(
											Quaternion.Euler(angle.x, angle.y, angle.z),
											-currentState.Value.forwardSpeed,
											-currentState.Value.rightSpeed,
											currentState.Value.shootNow);

			playState(backwardState);
			currentState = currentState.Previous;
		///////////////////////////////////////////////////////////////////////
		}
		
		else{
            changeTimeFlowDirction(true);
        }
	}

	void Record()
	{
		if (playerStates.Count > recordTime * (1.0/Time.deltaTime)){
			playerStates.RemoveFirst();
			Debug.Log($"Warning!! You shouldn't record for more than {recordTime/60.0} mintes!.");
		}
		
        playerStates.AddLast(
			new PlayerState(
				playerBlendComponent.headpivot.transform.rotation,
				playerBlendComponent.forwardSpeed,
				playerBlendComponent.rightSpeed,
				playerBlendComponent.shootNow
			)
		);
	}

	public void changeTimeFlowDirction(bool direction)
	{
	    /*
	        direction: True for (record)
	                   False for (rewind)
		*/
		
		playerBlendComponent.isPlaying = direction;
		playRecordedStates = !direction;
	}
	
}
