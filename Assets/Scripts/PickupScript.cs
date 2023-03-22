using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupScript : MonoBehaviour
{
    public int ammoAmount = 10;
    public AudioClip pickupAudio;

    GameObject gun;

    // Start is called before the first frame update
    void Start()
    {
        gun = GameObject.FindGameObjectWithTag("Gun");

        Vector3 position = transform.position;
        position.y = Random.Range(1f, 1.5f);
        transform.position  = position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up, 90 * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            gameObject.SetActive(false);

            gun.GetComponent<ShootProjectile>().addAmmo(ammoAmount);
            AudioSource.PlayClipAtPoint(pickupAudio, transform.position);

            Destroy(gameObject, 1f);
        }
    }
}
