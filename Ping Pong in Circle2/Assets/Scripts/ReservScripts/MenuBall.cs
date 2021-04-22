using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public sealed class MenuBall : MonoBehaviour
{
    [SerializeField] private Rigidbody2D ballRigidbody2D;
    [SerializeField] private float speed;
    private Vector3 lastVelocity;
    private Vector3 platformVector;
    private Vector3 direction;


    private void Start()
    {
        ballRigidbody2D.AddForce(Vector2.down * speed);
    }
    void Update()
    {
        lastVelocity = ballRigidbody2D.velocity;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        var localSpeed = lastVelocity.magnitude;
        direction = Vector3.Reflect(lastVelocity.normalized, collision.contacts[0].normal);
        ballRigidbody2D.velocity = direction * localSpeed;
    }
}

