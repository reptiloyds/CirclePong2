using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class Brick : BlockLogic
{
    [SerializeField] private GameObject particles;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private BoxCollider2D boxCollider2D;

    public Brick()
    {
        healthPoint = 1;
        timeToDestroy = 0.2f;
        trigger = Triggers.Shake.ToString();
    }
    private void Start()
    {
        cameraAnimator = Camera.main.GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Touched();

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Touched();
    }
    private void Touched()
    {
        boxCollider2D.enabled = false;
        audioSource.pitch = Random.Range(0.9f, 1.1f);
        audioSource.Play();
        Instantiate(particles, transform);
        GetDamage();
    }
}
