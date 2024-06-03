using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class CarController : MonoBehaviour
{
    
    [SerializeField] float carSpeed;
    [SerializeField] float maxSpeed;
    [SerializeField] float steerAngle;

    

    float dragAmount = 0.99f;

    [SerializeField] float Traction;

    public Transform lw,rw;

    Vector3 _rotVec;
    Vector3 _moveVec;

    //slope değişkenleri
    Quaternion fromRotation;
    Quaternion toRotation;
    Vector3 targetNormal;
    RaycastHit hit;
    float weight = 9;
    float adjustSpeed = 1;

    Rigidbody carRigidbody;
    


    void Start()
    {
        carRigidbody = GetComponent<Rigidbody>();
        targetNormal = transform.up;
    }

    
    void Update()
    {

       carRigidbody.velocity += transform.forward * carSpeed * Time.deltaTime;

       _rotVec += new Vector3(0,Input.GetAxis("Horizontal"),0); 
    
       transform.Rotate(Vector3.up * Input.GetAxis("Horizontal") * steerAngle * Time.deltaTime*carRigidbody.velocity.magnitude);
       //transform.Rotate(Vector3.up * CrossPlatformInputManager.GetAxis("Horizontal") * steerAngle * Time.deltaTime * carRigidbody.velocity.magnitude);


        carRigidbody.velocity *= dragAmount;
       carRigidbody.velocity = Vector3.ClampMagnitude(carRigidbody.velocity,maxSpeed);
       carRigidbody.velocity = Vector3.Lerp(carRigidbody.velocity.normalized,transform.forward, Traction * Time.deltaTime) * carRigidbody.velocity.magnitude;

       _rotVec = Vector3.ClampMagnitude(_rotVec, steerAngle);

       lw.localRotation = Quaternion.Euler(_rotVec);
       rw.localRotation = Quaternion.Euler(_rotVec);

       

       //slope
        if (Physics.Raycast(transform.position, -Vector3.up, out hit))
        {


            targetNormal = hit.normal;
            fromRotation = transform.rotation;
            toRotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
            weight = 0;


            if (weight <= 1)
            {
                weight += Time.deltaTime * adjustSpeed;
                transform.rotation = Quaternion.Slerp(fromRotation, toRotation, weight);

            }
        }

    }

    






}



