using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ControlPlatform : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rigidBody;
    [SerializeField] private float platformSpeed;
    [SerializeField] private Joystick joystick;
    private float horizontalInput, verticalInput;
    private Vector3 centerVector;
    private void Start()
    {
        centerVector = Vector3.zero;
    }
    private void FixedUpdate()
    {
        horizontalInput = joystick.Horizontal;
        verticalInput = joystick.Vertical;
        rigidBody.velocity = Vector2.MoveTowards(rigidBody.transform.position, joystick.Direction, platformSpeed);
    }
}
