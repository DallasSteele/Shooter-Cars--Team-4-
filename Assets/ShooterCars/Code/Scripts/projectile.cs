using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile : MonoBehaviour
{

    //#serialization
    [SerializeField] private float lifetime = 5f;
    [SerializeField] private float damage = 10f;

    void Start()
    {
        // Destroy the projectile, we don't want them lying around the whole time
       // Destroy(gameObject, lifetime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        //check if the projectile hit a targer
        if (collision.gameObject.CompareTag("Target"))
        {
            //apply the damage
            //collision.gameObject.GetComponent<Health>().TakeDamage(damage);

            //destroy it
            Destroy(gameObject);
        }
    }
}
