using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CarController : MonoBehaviour
{
    Rigidbody2D rb;

    [SerializeField]
    float acceleration = 100; // kph

    [SerializeField]
    float steering = 2f; // steering from left to right sensitivity 
    float steeringLvl, speed, direction;
    int gear = 2; // default car gear is neutral

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Debug.Log("Neutral");
    }

    // Update is called once per frame
    void Update()
    {
        steeringLvl = Input.GetAxis ("Horizontal"); // get left and right input (a,d keys)
        speed = Input.GetAxis ("Vertical") * acceleration; // get up and down (w, s keys)
        direction = Mathf.Sign(Vector2.Dot (rb.velocity, rb.GetRelativeVector(Vector2.down)));
        // gearshifter 
        if (Input.GetKeyDown(KeyCode.Alpha1)){
            gear = 1; 
        }
        if (Input.GetKeyDown(KeyCode.Alpha2)){
            gear = 2; 
        }
        if (Input.GetKeyDown(KeyCode.Alpha3)){
            gear = 3; 
        }
    }    
    void FixedUpdate(){
        rb.rotation += steeringLvl * steering *rb.velocity.magnitude * direction;
        if (gear == 1){
            rb.AddRelativeForce (Vector2.up * speed);
            Debug.Log("Drive");
        }
        else if (gear == 2){
            rb.AddRelativeForce (Vector2.zero * speed);
            Debug.Log("Neutral");
        }
        else if (gear == 3){
            rb.AddRelativeForce (Vector2.down * speed);
            Debug.Log("Reverse");
        }
        rb.AddRelativeForce ( - Vector2.right * rb.velocity.magnitude * steeringLvl / 2);
    }    
}
