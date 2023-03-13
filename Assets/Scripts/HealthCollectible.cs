using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthCollectible : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        PlayerCharacter controller = other.GetComponent<PlayerCharacter>();

        if (controller != null)
        {
            controller.ChangeHealth(5);
            Debug.Log("Object that entered the trigger : " + other);
            Destroy(gameObject);
        }
    }
}
