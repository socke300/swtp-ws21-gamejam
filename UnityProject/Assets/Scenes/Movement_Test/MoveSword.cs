using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveSword : MonoBehaviour
{

    public int height ;
    public float maxHeight;
    public float speed;
    public float maxForce;

    public float cooldownJump = 5;
   

    private float timeGone = 5;
    public GameObject forceSlider;

    float swordAngle;

    Camera cam;

    Vector3 anchorVector = new Vector3();
    Rigidbody rb;
    Rigidbody torso;
    Rigidbody leftLeg;
    Rigidbody rightLeg;
    
    Slider forceBar;

    float forcePool = 0f;
    float forceMultiplier;
    float forceRate = 25f;


    
    // Start is called before the first frame update
    void Start()
    {

     
        forceBar = forceSlider.GetComponent<Slider>();
        forceBar.maxValue = maxForce;
        cam = Camera.main;
        rb = GetComponent<Rigidbody>();
        torso = GameObject.Find("spine.003").GetComponent<Rigidbody>();
        leftLeg = GameObject.Find("shin.L").GetComponent<Rigidbody>();
        rightLeg = GameObject.Find("shin.R").GetComponent<Rigidbody>();   
    }

    // Update is called once per frame
    void Update()
    {
       //Movement
        forceBar.value = forcePool;

        if(forcePool<=maxForce) forcePool += 5f*Time.deltaTime;

        if(Input.GetMouseButton(0) && forcePool>0){
            forceMultiplier += forceRate*Time.deltaTime;
            forcePool -= forceRate*Time.deltaTime;
            if(forceMultiplier >= maxForce) forceMultiplier = 15f;
        }

        if(transform.position.z != 0) transform.position = new Vector3(transform.position.x, transform.position.y, 0);
      //Anker

      anchorVector = GetComponentInChildren<Transform>().position;


      // Attachement

      GameObject k = GameObject.Find("hand.R");
      Vector3 zwischenvektor = anchorVector;
      // Nachjustierung für Hand

      //zwischenvektor.y +=  -0.3f;

      k.transform.position = zwischenvektor;

        
        Vector2 torsoForce = torso.velocity*torso.mass;
        Vector2 legLForce = leftLeg.velocity*leftLeg.mass; 
        Vector2 legRForce = rightLeg.velocity*rightLeg.mass;

        Vector2 totalForce = (torsoForce+legLForce+legRForce)*0.05f;
        



      //Schwertrotation
       Vector2 mousePos = new Vector2();
       mousePos = Input.mousePosition;
       

        Vector3 mouse = new Vector3(mousePos.x-Screen.width/2,mousePos.y-Screen.height/2,transform.position.z);
        Vector3 direction = mouse - anchorVector ;

        float angle;

        angle = Mathf.Atan2(mouse.y,mouse.x) * Mathf.Rad2Deg-90;

        transform.rotation = Quaternion.Euler(0,0,angle);

        
        

        
        
    //Debug.Log(Vector3.right + new Vector3(totalForce.x, totalForce.y, 0));
        //rb.AddForce(-totalForce);



        if(Input.GetMouseButtonUp(0)&& timeGone > cooldownJump){

            
            //print("Zeit : "+Time.time+"  timeForNextJump"+timeForNextJump);

            //
            
            timeGone = 0;

            //timeForNextJump = (Time.time- time) +cooldownJump;



            // Impuls 



            rb.AddForce(direction.normalized*forceMultiplier, ForceMode.Impulse);
            forceMultiplier = 0;
        }else{
           
            timeGone = timeGone+Time.deltaTime;
        }

        //Debug.Log("timeGone : "+ timeGone + "coolDownJump :  "+ cooldownJump);
    }

    void FixedUpdate(){

      
        

        



    }
}