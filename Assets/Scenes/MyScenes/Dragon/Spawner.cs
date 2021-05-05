using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject projectile;
    public float projectileSpeed;
    public GameObject mouthEnd;

    private Animator animator;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        animator.SetInteger("dragon_motion", -1);
        MouthAttack();
        Fireball();
        AttackFly();
    }

    public void MouthAttack()
    {
        if (Input.GetKey(KeyCode.H))
        {
            Debug.Log("attackMouth");
            animator.SetInteger("dragon_motion", 2);
        }
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
