using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Missile : MonoBehaviour
{
    public Rigidbody rb;
    public GameObject explosion;
    public LayerMask whatSubject, whatPlayer;

    public float explosionRange;
    public float explosionForce;

    public int maxCollisions;
    public float maxLifetime;
    public bool explodeOnTouch = true;

    PhysicMaterial physics_mat;

    int collisions;
    void Start()
    {
        Setup();
    }
    void Update()
    {
        if (collisions > maxCollisions)
        {
            Explode();
        }
        maxLifetime -= Time.deltaTime;
        if(maxLifetime <= 0 ) {
            Explode();
                }
    }
    private void Explode()
    {
        Collider[] subjects = Physics.OverlapSphere(transform.position, explosionRange, whatSubject);
        for (int i = 0; i < subjects.Length; i++)
        {
            if (subjects[i].GetComponent<Rigidbody>())
            {
                subjects[i].GetComponent<Rigidbody>().AddExplosionForce(explosionForce, transform.position, explosionRange);
            }
        }
        Collider[] player = Physics.OverlapSphere(transform.position, explosionRange, whatPlayer);
        for (int i = 0; i < subjects.Length; i++)
        {
            if (subjects[i].GetComponent<Rigidbody>())
            {
                subjects[i].GetComponent<Rigidbody>().AddExplosionForce(explosionForce / 2, transform.position, explosionRange);
            }
        }
        //if (explosion != null) { Instantiate(explosion, transform.position, Quaternion.identity); }
        Invoke("Delay", 0.05f);
    }
    private void Delay()
    {
        
        Destroy(gameObject);
    }
    private void OnCollisionEnter(Collision collision)
    {
        collisions++;
    }
    private void Setup()
    {
        physics_mat = new PhysicMaterial();

        GetComponent<BoxCollider>().material = physics_mat;
    }


}
