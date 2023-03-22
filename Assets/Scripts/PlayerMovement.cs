using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public float gravity;
    public float speed;
    public float jumpStrength;
    public float airControl;

    Vector3 input;
    Vector3 movementDirection;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!LevelManager.isGameOver)
        {
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");

            input = (transform.right * moveHorizontal + transform.forward * moveVertical).normalized * speed;


            if (controller.isGrounded)
            {
                movementDirection = input;

                if (Input.GetButton("Jump"))
                {
                    movementDirection.y = Mathf.Sqrt(2 * jumpStrength * gravity);
                }
                else
                {
                    movementDirection.y = 0.0f;
                }
            }
            else
            {
                input.y = movementDirection.y;
                movementDirection = Vector3.Lerp(movementDirection, input, Time.deltaTime * airControl);
            }

            movementDirection.y -= gravity * Time.deltaTime;
            controller.Move(movementDirection * Time.deltaTime);
        }
    }

    void OnDrawGizmos()
    {
        // Display the explosion radius when selected
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, 50f);
    }
}
