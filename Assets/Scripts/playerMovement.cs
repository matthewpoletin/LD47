using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    public float speed;

    public CharacterController controller;

    // Update is called once per frame
    void FixedUpdate()
    {
        float x = Input.GetAxis("Horizontal");

        Vector3 movement = transform.right * x;

        controller.Move(movement * speed * Time.deltaTime);
    }
}
