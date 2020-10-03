using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    public float speed;

    public GameObject leftPivo;
    public GameObject rightPivo;

    public GameObject camera;

    public CharacterController controller;

    // Update is called once per frame
    void FixedUpdate()
    {
        float x = Input.GetAxis("Horizontal");

        Vector3 movement = transform.right * x;

        controller.Move(movement * speed * Time.deltaTime);

        if (transform.position.x > rightPivo.transform.position.x)
        {
            transform.position = rightPivo.transform.position;
            x = 0;
        }
        else if (transform.position.x < leftPivo.transform.position.x)
        {
            transform.position = leftPivo.transform.position;
            x = 0;
        }

        camera.transform.Rotate(0, x * Time.deltaTime * 6, 0);
    }
}
