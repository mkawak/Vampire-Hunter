using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedCollectible : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        PlayerCharacter controller = other.GetComponent<PlayerCharacter>();

        if (controller != null)
        {
            controller.ChangeSpeed(1); //value can be changed
            Debug.Log("Object that entered the trigger : " + other);
            Destroy(gameObject);
        }
    }
}
