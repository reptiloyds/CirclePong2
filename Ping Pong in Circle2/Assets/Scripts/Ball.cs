using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public sealed class Ball : MonoBehaviour
{
    [Header("Skins")]
    public Sprite[] ballSprites;
    [SerializeField] private SpriteRenderer ballSpriteRenderer;
    [Space]
    [SerializeField] private Rigidbody2D ballRigidbody2D;
    [SerializeField] private GameObject spawnPoint;
    [SerializeField] private float speed;
    [SerializeField] private Menu menu;
    [SerializeField] public SaveLoadManager slManager;
    private int countOfReturns;
    private float startSpeed;
    private float normalSpeed;
    private Vector3 lastVelocity;
    private Vector3 direction;

    private void Start()
    {
        slManager = GetComponent<SaveLoadManager>();
        slManager.ChangeSkin += OnChangeSkin;
        if (menu)
        {
            countOfReturns = menu.playerHealthPoint - 1;
        }
        startSpeed = speed / 5;
        normalSpeed = speed * 2;
        try
        {
            EventAggregator.instance.LoseGame += OnLose;
        }
        catch (Exception) { }
        ReturnToSpawn();
        ballSpriteRenderer.sprite = ballSprites[slManager.currentSkin];
    }

    private void OnChangeSkin()
    {
        ballSpriteRenderer.sprite = ballSprites[slManager.currentSkin];
    }

    void Update()
    {
        lastVelocity = ballRigidbody2D.velocity;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        direction = Vector3.Reflect(lastVelocity.normalized, collision.contacts[0].normal).normalized;
        ballRigidbody2D.velocity = direction * normalSpeed;
    }
    private void OnLose()
    {
        ballRigidbody2D.constraints = RigidbodyConstraints2D.FreezePosition;
        EventAggregator.instance.LoseGame -= OnLose;
    }
    private void ReturnToSpawn()
    {
        gameObject.GetComponent<TrailRenderer>().enabled = false;
        gameObject.transform.position = spawnPoint.transform.position;
        StopMoving();
        ballRigidbody2D.AddForce(Vector2.down * startSpeed);
        StartCoroutine(TrailOn());
    }
    private void StopMoving()
    {
        ballRigidbody2D.constraints = RigidbodyConstraints2D.FreezePosition;
        ballRigidbody2D.constraints = RigidbodyConstraints2D.None;
        //ballRigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Border")
        {
            EventAggregator.instance.OnPlayerGetDamage();
            if (countOfReturns > 0)
            {
                ReturnToSpawn();
                countOfReturns--;
            }
            else
            {
                StopMoving();
            }
        }
    }

    private IEnumerator TrailOn()
    {
        yield return new WaitForSeconds(1f);
        gameObject.GetComponent<TrailRenderer>().enabled = true;
    }
    private void OnDestroy()
    {
        EventAggregator.instance.LoseGame -= OnLose;
        slManager.ChangeSkin -= OnChangeSkin;
    }
}

