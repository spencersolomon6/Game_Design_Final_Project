using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    Transform playerBody;
    public float sensitivity = 100f;
    public Transform gunBody;

    float pitch = 0;

    // Start is called before the first frame update
    void Start()
    {
        playerBody = transform.parent.transform;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (!LevelManager.isGameOver)
        {
            float moveX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
            float moveY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

            // Yaw
            playerBody.Rotate(Vector3.up * moveX);

            // Pitch
            pitch -= moveY;
            pitch = Mathf.Clamp(pitch, -90, 90);

            transform.localRotation = Quaternion.Euler(pitch, 0, 0);
            gunBody.localRotation = Quaternion.Euler(pitch, 0, 0);
        }
    }
}
