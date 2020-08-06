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

    float forwardNonsmooth;
    float rightNonsmooth;
    
    //recordables
    public GameObject headpivot; //rotation only
    public float forwardSpeed;//Forward Animation
    public float rightSpeed; //Right/Left Animation
    public bool shootNow = false;
    //animation 
    [SerializeField] private float acceleration = 0.00005f;

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
        //if (isPlaying){
            chest.transform.LookAt(aimat.transform.position);
            chest.transform.Rotate(new Vector3(0, rotate, 0));
            head.transform.LookAt(aimat.transform.position);
        //}
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
        smoothInput();
    }

    public void smoothInput()
    {
        forwardSpeed = smoothValue(forwardSpeed, forwardNonsmooth);
        rightSpeed = smoothValue(rightSpeed, rightNonsmooth);
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
        if (Input.GetKey(KeyCode.W))
        {
            forwardNonsmooth = 1;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            forwardNonsmooth = -1;
        }
        else
        {
            forwardNonsmooth = 0;
        }
        if (Input.GetKey(KeyCode.D) && rightSpeed <= 1)
        {
            rightNonsmooth = 1;
        }
        else if(Input.GetKey(KeyCode.A)&& rightSpeed>=-1)
        {
            rightNonsmooth = -1;
        }
        else
        {
            rightNonsmooth = 0;
        }
        applyRightForward();
    }

    void applyRightForward(){
        anim.SetFloat("Forward",forwardSpeed);
        anim.SetFloat("Right",rightSpeed);
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

    float smoothness = 8; //less = smoother but 8 is perfect ya ksomak
    float smoothValue(float x, float y)
    {
        return Mathf.Lerp(x, y, Time.deltaTime * smoothness);
    }


    //b7b shanab hesham
    //kosom abusamra
}
