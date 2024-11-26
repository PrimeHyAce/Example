using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class App : MonoBehaviour
{
    // on trigger enter dissapear this object
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("FE"))
        {
            Destroy(gameObject);
        }
    }
}
