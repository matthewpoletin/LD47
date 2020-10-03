﻿using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = default;
    [SerializeField] private CharacterController controller = default;
    [SerializeField] private GameObject leftPivo = default;
    [SerializeField] private GameObject rightPivo = default;

    private Camera _camera;

    public void Connect(Camera camera1)
    {
        _camera = camera1;
    }

    private void FixedUpdate()
    {
        float x = Input.GetAxis("Horizontal");

        var movement = transform.right * x;

        controller.Move(movement * (speed * Time.deltaTime));

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

        _camera.transform.Rotate(0, x * Time.deltaTime * 6, 0);
    }
}