using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private Rigidbody targetRb;
    private GameManager gameManager;
    private float minSpeed = 12.0f;
    private float maxSpeed = 16.0f;
    private float torque = 10.0f;
    private float xSpawnPos = 4.0f;
    private float ySpawnPos = 2.0f;

    public ParticleSystem explosionEffect;
    public int pointValue;


    // Start is called before the first frame update
    void Start()
    {
        targetRb = GetComponent<Rigidbody>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();

        targetRb.AddForce(RandomForce(), ForceMode.Impulse);
        targetRb.AddTorque(new Vector3(RandomTorque(), RandomTorque(), RandomTorque()), ForceMode.Impulse);
        transform.position = RandomSpawnPos();
            
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnMouseDown()
    {
        if (gameManager.isGameActive)
        {
        Destroy(this.gameObject);
        Instantiate(explosionEffect, transform.position, explosionEffect.transform.rotation);
        gameManager.UpdateScore(pointValue);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(this.gameObject);      
        if (!this.gameObject.CompareTag("Bad"))
        {
            gameManager.GameOver();           
        }
    }

    Vector3 RandomForce()
    {
        return Vector3.up * Random.Range(minSpeed, maxSpeed);
    }

    float RandomTorque()
    {
        return Random.Range(-torque, torque);
    }

     Vector3 RandomSpawnPos()
    {
        return new Vector3(Random.Range(-xSpawnPos, xSpawnPos), -ySpawnPos);        
    }
}
