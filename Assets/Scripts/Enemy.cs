using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] GameObject enemyExplosionFX;
    [SerializeField] Material originalEnemyMaterial;
    [SerializeField] int amountToIncrease = 15;
    [SerializeField] int enemyHP = 100;
    [SerializeField] float timeDamaged = 0.2f;

    ScoreBoard scoreBoard;
    GameObject spawnAtRuntime;

    void Start()
    {
        scoreBoard = FindObjectOfType<ScoreBoard>();
        spawnAtRuntime = GameObject.FindWithTag("Spawn At Runtime");

        AddRigidbody();
    }

    void AddRigidbody()
    {
        Rigidbody rb = gameObject.AddComponent<Rigidbody>();
        rb.useGravity = false;
    }

    void OnParticleCollision(GameObject other)
    {
        ProcessHit();
    }
    
    void ProcessHit()
    {        
        Hitmarker();

        enemyHP -= 10;
        if (enemyHP <= 0)
        {
            KillEnemy();
        }
    }

    void Hitmarker()
    {
        GetComponent<MeshRenderer>().material.color = Color.red;
        Invoke("ReturnToDefaultColor", timeDamaged);
    }

    void ReturnToDefaultColor()
    {
        GetComponent<MeshRenderer>().material = originalEnemyMaterial;
    }

    void KillEnemy()
    {
        scoreBoard.IncreaseScore(amountToIncrease);
        
        GameObject fx = Instantiate(enemyExplosionFX, transform.position, Quaternion.identity);
        fx.transform.parent = spawnAtRuntime.transform;
        Destroy(gameObject);
    }
}
