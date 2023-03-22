using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShootProjectile : MonoBehaviour
{
    public GameObject projectilePrefab;
    public float projectileSpeed = 100f;
    public GameObject parentObject;
    public AudioClip shootAudio;
    public int totalAmmo = 150;
    public int clipSize = 30;
    public Text ammoText;
    public Text reloadText;

    int currentAmmo;
    int currentClip;
    public Image crosshair;
    public Color crosshairColor;

    // Start is called before the first frame update
    void Start()
    {
        parentObject = GameObject.FindGameObjectWithTag("ProjectileParent");
        crosshairColor = crosshair.color;

        currentAmmo = totalAmmo;
        currentClip = clipSize;
    }

    // Update is called once per frame
    void Update()
    {
        if (!LevelManager.isGameOver)
        {
            ammoText.text = currentClip + "/" + currentAmmo;

            if (Input.GetKeyDown(KeyCode.R) && currentAmmo > 0)
            {
                ReloadGun();
            }

            if (currentClip == 0)
            {
                reloadText.gameObject.SetActive(true);
            } else
            {
                reloadText.gameObject.SetActive(false);
            }

            if (Input.GetButtonDown("Fire1") && currentClip > 0)
            {
                currentClip -= 1;
                AudioSource.PlayClipAtPoint(shootAudio, Camera.main.transform.position);

                Quaternion rotation = transform.rotation * projectilePrefab.gameObject.transform.rotation;

                GameObject projectile = Instantiate(projectilePrefab, transform.position + transform.forward, rotation) as GameObject;
                projectile.transform.SetParent(parentObject.transform);

                Rigidbody rb = projectile.GetComponent<Rigidbody>();
                rb.AddForce(transform.forward * projectileSpeed, ForceMode.VelocityChange);

                // AudioSource.PlayClipAtPoint(shootAudio, transform.position);

                Destroy(projectile, 5);
            }
        }
    }

    void ReloadGun()
    {
/*        transform.Rotate(new Vector3(0, 90, 0));

        yield return new WaitForSeconds(1);

        transform.Rotate(new Vector3(0, -90, 0));*/

        if (clipSize - currentClip > currentAmmo)
        {
            currentClip = currentClip + currentAmmo;
            currentAmmo = 0;
        } else
        {
            currentAmmo -= (clipSize - currentClip);
            currentClip = clipSize;
        }
    }

    public void addAmmo(int ammoAmount)
    {
        currentAmmo += ammoAmount;
    }
}
