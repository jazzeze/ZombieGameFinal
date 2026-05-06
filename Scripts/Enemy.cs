using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public int health = 50;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Damage")
        {
            health -= 10;

            if(health <= 0)
            {
                Die();
            }
        }
    }

    public void TakeDamage (int amount)
    {
        health -= amount;
        if (health <= 0f) 
        {
            Die();
        }
    }
    void Die ()
    {
        //Destroy(gameObject);
        rb.freezeRotation = false;
        transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, transform.rotation.z + 5f);
        this.enabled = false;
    }
}
