using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject chest;
    public GameObject head;
    public GameObject aimat;
    float rotate = 45f;
    Animator anim;
    GameObject headpivot;

    //recordables
    public Quaternion aimdirection;
    public bool forward;
    public bool backward;
    //public bool right;
    //public bool left;
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
        chest.transform.LookAt(aimat.transform.position);
        chest.transform.Rotate(new Vector3(0, rotate, 0));
        head.transform.LookAt(aimat.transform.position);
    }

    bool once = true;
    private void Update()
    {
        this.transform.LookAt(aimat.transform);
        this.transform.rotation = Quaternion.Euler(0, this.transform.eulerAngles.y, 0);
        headpivot.transform.position = head.transform.position;


        if(forward)
        {
            if(once)
            {
                movement();
                once = false;
            }
        }
        else
        {
            if(!once)
            {
                movement();
                once = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            forward = true;
            backward = false;
            

        }
        else if (Input.GetKeyUp(KeyCode.W))
        {
            forward = false;
            movement();
        }
        mouserotation();
    }
    void movement()
    {
        if(forward)
        {
            anim.SetBool("moving", true);
            anim.SetBool("forward", true);
        }
        else
        {
            anim.SetBool("moving", false);
            anim.SetBool("forward", false);
        }
    }
    float xRotation = 0;
    void mouserotation()
    {
        float mouseX = Input.GetAxis("Mouse X") * 150f * Time.deltaTime;
        float mouseY = -Input.GetAxis("Mouse Y") * 250f * Time.deltaTime;
        mouseY = Mathf.Clamp(mouseY, -60f, 60f);
        xRotation += mouseY;
        xRotation = Mathf.Clamp(xRotation, -45, 45);
        
        //Debug.Log(mouseY);

        headpivot.transform.Rotate(Vector3.up * mouseX);
        headpivot.transform.eulerAngles = new Vector3(xRotation, headpivot.transform.eulerAngles.y, 0);
        //headpivot.transform.Rotate(Vector3.right * mouseY);
        //headpivot.transform.localRotation = Quaternion.Euler(mouseY, headpivot.transform.rotation.y, 0);


        //if (headpivot.transform.rotation.ToEuler().x > -0.5f)
        //{

        //}

        //float xclamp = Mathf.Clamp(headpivot.transform.rotation.ToEuler().x, -20f, 20f);
        //float y = headpivot.transform.rotation.ToEuler().y;
        //float z = headpivot.transform.rotation.ToEuler().z;

        //Debug.Log(headpivot.transform.rotation.ToEuler().x );
        //Quaternion test = new Quaternion(Mathf.Clamp(headpivot.transform.localRotation.x, -0.35f, 0.5f), headpivot.transform.localRotation.y, headpivot.transform.localRotation.z, headpivot.transform.localRotation.w);
        //headpivot.transform.localRotation = test;


        // e3mel hena y limit el camera angle on its local X axis 

    }
}
