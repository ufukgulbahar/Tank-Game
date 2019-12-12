using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bullet : MonoBehaviour
{


    private void Start()
    {
        Destroy(gameObject, 5);
    }

    private void OnCollisionEnter(Collision collision)
    {
        HealthBehevaiour healtBehaviour = collision.gameObject.GetComponent<HealthBehevaiour>();

        if (healtBehaviour != null)
        {
            healtBehaviour.TakeDemage(20f);
        }

        Destroy(gameObject, 1);
    }

}
