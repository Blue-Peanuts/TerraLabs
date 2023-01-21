using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BitsConsumption : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Energy energy = gameObject.GetComponent<Energy>();
        if (collision.gameObject.CompareTag("Giblet"))
        {
            energy.Give(collision.gameObject.GetComponent<Energy>().energyLevel, collision.gameObject.GetComponent<Energy>());
        }
    }
}
