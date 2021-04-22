using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class DefendedBrick : BlockLogic
{
    [SerializeField] private BoxCollider2D boxCollider2D;
    [SerializeField] private GameObject particles;
    [SerializeField] private AudioSource audioSource;

    public DefendedBrick()
    {
        healthPoint = 2;
        timeToDestroy = 0.2f;
        trigger = Triggers.Shake.ToString();
    }
    void Start()
    {
        cameraAnimator = Camera.main.GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        audioSource.pitch = Random.Range(0.9f, 1.1f);
        audioSource.Play();
        Touched();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        audioSource.pitch = Random.Range(0.9f, 1.1f);
        audioSource.Play();
        Touched();
    }
    private void Touched()
    {
        gameObject.GetComponent<Animator>().SetTrigger("Broke");
        Instantiate(particles, transform);
        GetDamage();
    }
    protected override void Destroy()
    {
        boxCollider2D.enabled = false;
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        StartCoroutine(WaitingToDestroy(timeToDestroy));
    }
}
