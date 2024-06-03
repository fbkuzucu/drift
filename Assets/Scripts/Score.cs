using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;

public class Score : MonoBehaviour
{

    public Text scoreText;
    public Transform player;
    public double score;
    float driftValue;
    float driftAngle;
    Rigidbody rb;
    float x;
    float time;
    bool timecheck;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        score = 0;
        time = 0;
        x = player.rotation.y;
    }

    //IEnumerator bes()
    //{
    //    yield return new WaitForSecondsRealtime(5f);
    //    time = 5;
    //}

    

    void OnCollisionEnter(Collision c)
    {
        if (c.gameObject.name.Contains("Wooden Crate"))
        {
            Debug.Log("kutuya çarptı");
            score = score + 25;
        }

    }

    void OnTriggerEnter(Collider c)
    {
        Debug.Log("alanda");
        timecheck = true;
        Debug.Log("time: " + time );
    }

    void OnTriggerExit(Collider c)
    {
        timecheck = false;
        Debug.Log("Alanda değil");
        if(time < 5)
        {
            Debug.Log("enough time for drift xd");
            Debug.Log("time: " + time);
            time = 0;
        }
        else
        {
            score = score * 2;
            time = 0;
        }


    }

    // Update is called once per frame
    void Update()
    {
        //if(player.rotation.y < 0)
        //{
        //    score = (player.rotation.y * -1) + score;
        //}

        //else
        driftValue = Vector3.Dot(rb.velocity, transform.forward);
        driftAngle = Mathf.Acos(driftValue) * Mathf.Rad2Deg;
        float y = player.rotation.y;
        //Debug.Log(driftValue);
        //Debug.Log("X:" + x);
        //Debug.Log("y:" + y);
        if ((x != y)&&(player.rotation.x == 0 && player.rotation.z == 0))
        {
            //Debug.Log("!x: " + " y:" + y);
            x = y;
            score = score + 0.1 ;
        }

        if (timecheck)
        {
            time += 1f * Time.deltaTime;
            Debug.Log("time2: " + time);
            
        }


         
        scoreText.text = score.ToString("0");
        
    }

    

}


   