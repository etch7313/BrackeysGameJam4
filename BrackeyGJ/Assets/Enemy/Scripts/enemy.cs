using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy : MonoBehaviour
{
    public GameObject chest;
    public GameObject head;
    public GameObject aimat;
    float rotate = 45f;
    Animator anim;

    bool dead=false;

    public float aimHeight = 1.5f;

    
    void Start()
    {
        anim = GetComponent<Animator>();
        temp = new GameObject();

        hotspots = hotspotsParent.GetComponentsInChildren<Transform>();

        pathcolor = new Color(Random.value, Random.value, Random.value);
    }

    void LateUpdate()
    {
        if (!dead)
        {
            chest.transform.LookAt(aimat.transform.position);
            chest.transform.Rotate(new Vector3(0, rotate, 0));
            head.transform.LookAt(aimat.transform.position);
        }
    }
    private void Update()
    {
        if (!dead)
        {
            this.transform.LookAt(aimat.transform);
            this.transform.rotation = Quaternion.Euler(0, this.transform.eulerAngles.y, 0);
        }


        position = this.transform.position;

        smoothFollow(aimat, target);

        moveHotspots();

        //death test, remove this ba3deen w call die() lama el player yshoot el 3ars
        if(Input.GetKeyDown(KeyCode.Space))
        {
            die();
        }

    }
    // el 7etta el hate7tagha
    bool moving;
    bool shooting;
    public Vector3 position;
     void moveForward()
    {
        if(!moving)
        {
            anim.SetBool("forward", true);
            moving = true;
        }
    }
     void LookAt(Vector3 target)
    {
        aimat.transform.position = target;
    }
     void stopMoving()
    {
        if(moving)
        {
            anim.SetBool("forward", false);
            moving = false;
        }
    }

    void shoot(GameObject target)
    {
        LookAt(target.transform.position);
        anim.SetTrigger("shoot");
        shooting = true;
        Invoke("stopShoot", 1);
    }
    void stopShoot()
    {
        shooting = false;
    }

    public void die()
    {
        dead = true;
        anim.SetTrigger("die");
    }

    Transform[] hotspots;
    public GameObject hotspotsParent;
    
    private int currentHotspot = 0;
    [SerializeField]
    private float enemySpeed = 1.0f;
    [SerializeField]
    private float accuracy = 0.1f;
    [SerializeField]
    private float rotationSpeed = 0.5f;

    Vector3 target;

    GameObject temp;

    Color pathcolor;
    void moveHotspots()
    {
        if (hotspots.Length == 0)
        {
            return;
        }
        visualize();
        if (!shooting)
        {
            target = new Vector3(hotspots[currentHotspot].position.x, transform.position.y, hotspots[currentHotspot].position.z);
        }
        Vector3 temptarget = target;
        Vector3 direction = target - transform.position;
        //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), rotationSpeed * Time.deltaTime);
       
        LookAt(aimat.transform.position);
        //target = Vector3.Slerp(target,  )
        if (direction.magnitude < accuracy)
        {
            currentHotspot++;
            
            if (currentHotspot == hotspots.Length)
            {
                stopMoving();
                currentHotspot = hotspots.Length - 1;
                return;
            }
            if (hotspots[currentHotspot].gameObject.CompareTag("shoot"))
            {
                stopMoving();
                
                shoot(aimat);

                if (shooting)
                {
                    target = Camera.main.transform.position;
                }
                else
                {
                    target = temptarget;
                }
            }
        }
        moveForward();
    }

    void visualize()
    {
        
        for(int i = 0; i < hotspots.Length-1; i++)
        {
            Debug.DrawLine(hotspots[i].position, hotspots[i + 1].position, pathcolor);
        }
        Debug.DrawLine(this.transform.position, target, Color.white);
    }

    //this is for turning speed
    float smoothness = 2; //less = smoother but more glitching w manyaka 2 is perfect ya ksomak
    void smoothFollow(GameObject x, Vector3 y)
    {
        x.transform.position = Vector3.Lerp(x.transform.position, y, Time.deltaTime * smoothness);
        x.transform.position = new Vector3(x.transform.position.x, aimHeight, x.transform.position.z);
    }
}
