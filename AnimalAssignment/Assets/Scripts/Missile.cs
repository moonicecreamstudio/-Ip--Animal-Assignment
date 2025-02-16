using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    public GameObject missile;
    public Rigidbody rb;
    public float speed;
    public float timer;
    public float despawnTimer;

    // Start is called before the first frame update
    void Start()
    {
        missile = this.gameObject;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = transform.forward * speed;
        timer += Time.deltaTime;
        if (timer >= despawnTimer)
        {
            Destroy(missile);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(missile);
    }
}
