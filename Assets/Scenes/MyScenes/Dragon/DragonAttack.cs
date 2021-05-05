using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonAttack : MonoBehaviour
{
    public GameObject mouthEnd;

    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other);

        other.gameObject.GetComponent<IDamageable>()?.TakeDamage(20);
        //Destroy(gameObject);
        other.gameObject.GetComponent<Rigidbody>().AddForce(mouthEnd.transform.forward * 100);
        //ParticleSystem ps = Instantiate(explosionVFX, transform.position, transform.rotation) as ParticleSystem;

       // ps.Play();

        //Invoke(DestroyPS, 2.0f);
    }
    public void Fireball()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Fireball");
            animator.SetInteger("dragon_motion", 1);
            GameObject fireball = Instantiate(projectile,
                new Vector3(mouthEnd.transform.position.x, mouthEnd.transform.position.y, mouthEnd.transform.position.z),
                transform.rotation) as GameObject;
            Rigidbody rb = fireball.GetComponent<Rigidbody>();
            rb.velocity = transform.forward * projectileSpeed;
        }
    }

    public void AttackFly()
    {
        if (Input.GetKey(KeyCode.J))
        {
            Debug.Log("attackFly");
            animator.SetInteger("dragon_motion", 3);
        }
    }
}
