using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatform : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Joystick joystick;
    [SerializeField] private AudioSource audioSource;

    private void FixedUpdate()
    {
        if(joystick.Direction != Vector2.zero)
        {
            rb.constraints = RigidbodyConstraints2D.None;
            rb.MovePosition(joystick.Direction * joystick.Direction.magnitude * speed);
        }
        
        else
        {
            rb.constraints = RigidbodyConstraints2D.FreezePosition;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        audioSource.pitch = Random.Range(0.9f, 1.1f);
        audioSource.Play();
    }
}