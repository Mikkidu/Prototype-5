using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Target : MonoBehaviour
{
    float ySpawnPos = -2, xRangePos = 4, maxTorq = 10, minForce =12, maxforce = 16;
    public int pointValue;
    public ParticleSystem explosionParticle;
    
    
    void Start()
    {
        Rigidbody targetRb = GetComponent<Rigidbody>();
        transform.position = RandomPosition();
        targetRb.AddForce(Vector3.up * RandomForce() + Vector3.back * 3, ForceMode.Impulse);
        targetRb.AddTorque(RandomTorque(), ForceMode.Impulse);
    }

    public void DestroyTarget()
    {
        if (GameManager.instance.isGameActive)
        {
            Destroy(gameObject);
            GameManager.instance.UpdateScore(pointValue);
            Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
            if (pointValue < 0)
            {
                GameManager.instance.HitThePlayer();
            }
        }
        
    }

    void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
        if (pointValue > 0)
        {
            GameManager.instance.HitThePlayer();
        }
    
    }

    Vector3 RandomPosition()
    {
        return new Vector3(Random.Range(-xRangePos, xRangePos), ySpawnPos);
    }

    float RandomForce()
    {
        return Random.Range(minForce, maxforce);
    }

    Vector3 RandomTorque()
    {
        return new Vector3(Random.Range(-maxTorq, maxTorq), Random.Range(-maxTorq, maxTorq), 
            Random.Range(-maxTorq, maxTorq));
    }

    
}
