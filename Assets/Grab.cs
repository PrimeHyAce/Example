using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Grab : MonoBehaviour
{
    // make f as a button to start pickup animation and then grab the object and use it
    // there is trigger to show if the object is grabable or not
    // there is ui to show if the object is grabable or not
    // each object has a specific use, player can only use 1 object at a time
    // if the player use another object, the previous object will be dropped
    // the object will be put on the player's hand (use transform to put the object on the player's hand)
    // hint on each object to show the player how to use the object
    // the object will be dropped if the player F again

    // each object has a specific use
    // to use the object, player must press left mouse button

    // this script is attached to the object that can be grabbed
    // the object must have a trigger collider to show if the object is grabable or not
    // the object must have a ui to show if the object is grabable or not
    // the object must have a hint to show the player how to use the object

    public GameObject player;
    public GameObject grabableUI;
    public GameObject hintUI;
    
    // water cannon particle system
    public GameObject waterCannon;
    
    private bool isGrabable = false;
    private bool isGrabbed = false;
    private bool isUsed = false;
    private bool isHintShown = false;
    
    public UnityEvent useEvent;

    private void Start()
    {
        grabableUI.SetActive(false);
    }

    private void Update()
    {
        if (isGrabable)
        {
            grabableUI.SetActive(true);
        }
        else
        {
            grabableUI.SetActive(false);
        }

        if (isGrabable && Input.GetKeyDown(KeyCode.F))
        {
            if (!isGrabbed)
            {
                isGrabbed = true;
                transform.SetParent(player.transform);
                transform.position = player.transform.position;
                transform.rotation = player.transform.rotation;
                transform.localScale = new Vector3(1, 1, 1);
                grabableUI.SetActive(false);
            }
            else
            {
                isGrabbed = false;
                transform.SetParent(null);
                transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
            }
        }

        // if the object is grabbed, the object can be used
        if (isGrabbed)
        {
            grabableUI.SetActive(false);

            // show hint to use the object if the object is grabbed
            if (!isHintShown)
            {
                hintUI.SetActive(true);
                isHintShown = true;
            }

            // use the object
            if (Input.GetMouseButtonDown(0))
            {
                isUsed = true;
                // call function from UseObject.cs use event system
                useEvent.Invoke();                
            }
        }
        else if (!isGrabbed && isHintShown)
        {
            hintUI.SetActive(false);
            isHintShown = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isGrabable = true;
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isGrabable = false;
        }
    }

    public void WaterCannon()
    {
        // play particle system
        waterCannon.SetActive(true);
        waterCannon.GetComponent<ParticleSystem>().Play();
        // set active collider from particle system
        waterCannon.GetComponent<Collider>().enabled = true;
    }

}
    
