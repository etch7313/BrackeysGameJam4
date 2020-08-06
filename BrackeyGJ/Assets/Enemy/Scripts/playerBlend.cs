using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerBlend : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject chest;
    public GameObject head;
    public GameObject aimat;
    float rotate = 45f;
    Animator anim;
    public Quaternion aimdirection;
    public bool forward;
    public bool backward;
    public bool isPlaying = true;
    
    //recordables
    public GameObject headpivot; //rotation only
    public float forwardSpeed;//Forward Animation
    public float rightSpeed; //Right/Left Animation
    public bool shootNow = false;
    //animation 
    [SerializeField] private float acceleration = 0.006f;

    void Start()
    {
        anim = GetComponent<Animator>();
        headpivot = new GameObject();
        aimat.transform.parent = headpivot.transform;
        aimat.transform.localPosition = new Vector3(0, 0, 2.7f);

        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (isPlaying){
            chest.transform.LookAt(aimat.transform.position);
            chest.transform.Rotate(new Vector3(0, rotate, 0));
            head.transform.LookAt(aimat.transform.position);
        }
    }

    private void Update()
    {
        //alligns player's direction
        DirectionAllignment();
        if (shootNow) Shoot();

        if (isPlaying)
        {
            //Keyboard input
            MyInput();
            //Mouse input
            mouserotation();
        }
        else{
            applyRightForward();
        }
    }
    
    float xRotation = 0;

    void DirectionAllignment()
    {
        this.transform.LookAt(aimat.transform);
        this.transform.rotation = Quaternion.Euler(0, this.transform.eulerAngles.y, 0);
        headpivot.transform.position = head.transform.position;
    }
    void MyInput()
    {
        //Forward & Backward
        if (Input.GetKey(KeyCode.W) && forwardSpeed<=1)
        {
            forwardSpeed = Mathf.Lerp(forwardSpeed, 1.5f, acceleration);
        }
        else if (Input.GetKey(KeyCode.S) && forwardSpeed >=-1)
        {
            forwardSpeed = Mathf.Lerp(forwardSpeed, -1.5f, acceleration);

        }
        else if (Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.W))
        {
            Invoke("resetForwardToZero",0.5f);
        }
        else
        {
            forwardSpeed = Mathf.Lerp(forwardSpeed, 0.0f, 0.05f);
        }
        //Right & Left
        if (Input.GetKey(KeyCode.D) && rightSpeed <= 1)
        {
            rightSpeed = Mathf.Lerp(rightSpeed, 1.5f, acceleration);
        }
        else if(Input.GetKey(KeyCode.A)&& rightSpeed>=-1)
        {
            rightSpeed = Mathf.Lerp(rightSpeed, -1.5f, acceleration);
        }
        else if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
        {
            Invoke("resetRightToZero",0.5f);
        }
        else
        {
            rightSpeed = Mathf.Lerp(rightSpeed, 0.0f, 0.05f);
        }
        forwardSpeed = Mathf.Clamp(forwardSpeed, -1, 1);
        rightSpeed = Mathf.Clamp(rightSpeed, -1, 1);
        applyRightForward();
    }

    void applyRightForward(){
        anim.SetFloat("Forward",forwardSpeed);
        anim.SetFloat("Right",rightSpeed);
    }

    void resetForwardToZero()
    {
        forwardSpeed = 0;
    }
    void resetRightToZero()
    {
        rightSpeed = 0;
    }
    void mouserotation()
    {
        float mouseX = Input.GetAxis("Mouse X") * 150f * Time.deltaTime;
        float mouseY = -Input.GetAxis("Mouse Y") * 250f * Time.deltaTime;
        mouseY = Mathf.Clamp(mouseY, -60f, 60f);
        xRotation += mouseY;
        xRotation = Mathf.Clamp(xRotation, -45, 45);
        headpivot.transform.Rotate(Vector3.up * mouseX);
        headpivot.transform.eulerAngles = new Vector3(xRotation, headpivot.transform.eulerAngles.y, 0);
    }

    void Shoot()
    {



        shootNow = false;
    }
    //b7b shanab hesham
    //kosom abusamra
}
