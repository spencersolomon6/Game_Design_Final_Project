using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public int xMin = -25;
    public int xMax = 25;
    public int yMin = 8;
    public int yMax = 25;
    public int zMin = -25;
    public int zMax = 25;
    public int spawnTime = 3;
    public int health = 100;

    public GameObject particlePrefab; 

    // Start is called before the first frame update
    void Start()
    {
        LevelManager.numSpawners++;
        InvokeRepeating("SpawnEnemy", spawnTime, spawnTime);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void SpawnEnemy()
    {
        if (!LevelManager.isGameOver)
        {
            Vector3 enemyPosition;

            enemyPosition.x = transform.position.x + Random.Range(xMin, xMax);
            enemyPosition.y = enemyPrefab.gameObject.transform.position.y;
            enemyPosition.z = transform.position.z + Random.Range(zMin, zMax);
            enemyPosition.z = transform.position.z + Random.Range(zMin, zMax);

            GameObject spawnedEnemy = Instantiate(enemyPrefab, enemyPosition, transform.rotation) as GameObject;
            spawnedEnemy.transform.SetParent(null);
            spawnedEnemy.transform.parent = gameObject.transform;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
        {
            health -= 10;
            Destroy(collision.gameObject);

            if (health <= 0)
            {
                LevelManager.numSpawners--;
                Instantiate(particlePrefab, transform.position, transform.rotation);
                gameObject.SetActive(false);
                Destroy(gameObject, 2);
            }
        }
    }
}
