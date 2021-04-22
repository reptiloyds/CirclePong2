using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class BombBrick : BlockLogic
{
    [SerializeField] private Animator animator;
    [SerializeField] private BoxCollider2D boxCollider2D;

    public BombBrick()
    {
        healthPoint = 1;
        timeToDestroy = 0.4f;
        trigger = Triggers.Explosion.ToString();
    }
    void Start()
    {
        cameraAnimator = Camera.main.GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        EventAggregator.instance.OnExplosion();
        Touched();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        EventAggregator.instance.OnExplosion();
        Touched();
    }
    private void Touched()
    {
        //boxCollider2D.enabled = false;
        animator.SetTrigger(trigger);
        GetDamage();
    }

    protected override void Destroy()
    {
        StartCoroutine(WaitingToDestroy(timeToDestroy));
    }
}
