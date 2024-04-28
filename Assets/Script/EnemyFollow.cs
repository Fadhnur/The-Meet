using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFollow : MonoBehaviour
{
public UnityEngine.AI.NavMeshAgent enemy;
public Transform player;

public float enemySpeed = 3.0f;

public float detectionRadius = 5.0f;

    // Start is called before the first frame update
    void Start()
    {
        enemy.speed = enemySpeed;
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(transform.position, player.position) < detectionRadius)
        {
            enemy.SetDestination(player.position);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Cek apakah objek yang masuk ke trigger adalah player
        if (other.CompareTag("Player"))
        {
            // Jika ya, mulai mengejar player
            enemy.SetDestination(player.position);
        }
    }

}
