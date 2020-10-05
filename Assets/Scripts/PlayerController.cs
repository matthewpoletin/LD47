﻿using UnityEngine;

public class PlayerController : MonoBehaviour, ICharacter
{
    [SerializeField] private CharacterController controller = default;
    [SerializeField] private Transform _topPlaceholder = default;
    [SerializeField] private GameObject leftPivo = default;
    [SerializeField] private GameObject rightPivo = default;

    private Camera _camera;
    private GlobalParams _globalParams;

    public bool MovementEnabled { get; set; } = true;

    public Transform TopPlaceholder => _topPlaceholder;
    public Vector3 Position => transform.position;

    public void Connect(Camera camera1, GlobalParams globalParams)
    {
        _camera = camera1;
        _globalParams = globalParams;
    }

    public void Tick(float deltaTime)
    {
        if (!MovementEnabled)
        {
            return;
        }

        if (_globalParams == null)
        {
            return;
        }

        float x = Input.GetAxis("Horizontal");

        var movement = transform.right * x;

        controller.Move(movement * (_globalParams.PlayerMovementSpeed * Time.deltaTime));

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
