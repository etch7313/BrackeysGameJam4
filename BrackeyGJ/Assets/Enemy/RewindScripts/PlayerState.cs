using UnityEngine;

public struct PlayerState
{
    public Quaternion headpivotRotation;
    public float forwardSpeed;//Forward Animation
    public float rightSpeed; //Right/Left Animation
    public bool shootNow;
    
	public PlayerState(Quaternion _headpivotRotation, 
					   float _forwardSpeed, 
					   float _rightSpeed, 
					   bool _shootNow)
	{
		headpivotRotation = _headpivotRotation;
		forwardSpeed = _forwardSpeed;
		rightSpeed = _rightSpeed;
		shootNow = _shootNow;
	}

}
