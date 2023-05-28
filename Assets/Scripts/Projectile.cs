using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 20f;
    private Vector3 objectPoolPos;
    float time = 0;
    // Start is called before the first frame update
    void Start()
    {
        objectPoolPos = transform.position;

    }

    // Update is called once per frame
    void Update()
    {

        if (transform.position != objectPoolPos) { time += Time.deltaTime; transform.Translate(speed * Time.deltaTime * Vector3.up); }
        if (time > 5f) { transform.position = objectPoolPos; time = 0; }


    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.collider.CompareTag("RedEnemy") && gameObject.CompareTag("RedLaser"))
        {
            Target healthScript = collision.collider.GetComponent<Target>();
            if (healthScript != null)
            {
                healthScript.DealDamage(50f);
            }
        }
        else if (collision.collider.CompareTag("BlueEnemy") && gameObject.CompareTag("BlueLaser"))
        {
            Target healthScript = collision.collider.GetComponent<Target>();
            if (healthScript != null)
            {
                healthScript.DealDamage(50f);
            }
        }
        transform.position = objectPoolPos;
    }


}
