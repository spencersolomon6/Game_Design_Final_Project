using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehavior : MonoBehaviour
{
    public Transform player;
    public float moveSpeed = 5f;
    public float minDistance = 0f;
    public int damageAmount = 20;
    public AudioClip deathAudio;
    public GameObject pickupPrefab;
    public Transform pickupParent;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        pickupParent = GameObject.FindGameObjectWithTag("PickupParent").transform;
    }

    // Update is called once per frame
    void Update()
    {
        if (!LevelManager.isGameOver) {
            if (Vector3.Distance(player.position, transform.position) > minDistance)
            {
                Vector3 movement = Vector3.MoveTowards(transform.position, player.position, moveSpeed * Time.deltaTime);
                movement.y = transform.position.y;
                transform.position = movement;
                transform.LookAt(player);
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
        {
            AudioSource.PlayClipAtPoint(deathAudio, Camera.main.transform.position);

            if (Random.Range(0, 100) <= 20)
            {
                GameObject pickup = Instantiate(pickupPrefab, transform.position, transform.rotation);
                pickup.transform.SetParent(pickupParent);
            }

            Destroy(gameObject);
            Destroy(collision.gameObject);

        } else if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth health = collision.gameObject.GetComponent<PlayerHealth>();
            health.TakeDamage(damageAmount);
            Destroy(gameObject);
        }
    }
}
