using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RainbowBrick : BlockLogic
{
    [SerializeField] private GameObject[] bricks;
    [SerializeField] private GameObject particles;
    [SerializeField] private BoxCollider2D boxCollider2D;

    public RainbowBrick()
    {
        healthPoint = 1;
        timeToDestroy = 0.2f;
        trigger = Triggers.Shake.ToString();
    }
    void Start()
    {
        cameraAnimator = Camera.main.GetComponent<Animator>();
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        boxCollider2D.enabled = false;
        EventAggregator.instance.OnRaimbowBlock();
        GetDamage(); 
    }
    protected override void Destroy()
    {
        Instantiate(particles, transform);
        StartCoroutine(WaitTillSpawn());
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        StartCoroutine(WaitingToDestroy(timeToDestroy));
    }

    IEnumerator WaitTillSpawn()
    {
        yield return new WaitForSeconds(0.1f);
        var brick = Instantiate(bricks[Random.Range(0, bricks.Length)], transform);
        brick.transform.SetParent(gameObject.transform.parent);
    }
}
