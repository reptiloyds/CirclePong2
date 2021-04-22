using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public sealed class BlackHole : MonoBehaviour
{
    [SerializeField] private Transform bricks;
    [SerializeField] private SpriteRenderer holeSpriteRender;
    [SerializeField] private CircleCollider2D holeCircleCollider2D;
    [SerializeField] private Animator holeAnimator;
    [SerializeField] private Animator ballAnimator;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private Menu menu;
    [SerializeField] private float speed;
    private bool isOpened = false;
    private Coroutine coroutine;
    private void Start()
    {
        EventAggregator.instance.WinGame += OnWin;
        EventAggregator.instance.LoseGame += OnLoseGame;
    }

    private void OnLoseGame()
    {
        StartCoroutine(SoundOff());
        if (coroutine != null)
        {
            StopCoroutine(coroutine);
        }
        EventAggregator.instance.LoseGame -= OnLoseGame;
    }

    private void OnWin(object sender, WinGameEventArgs e)
    {
        StartCoroutine(SoundOff());
        StopCoroutine(coroutine);
        EventAggregator.instance.WinGame -= OnWin;
    }

    private void Update()
    {
        if(bricks.childCount <= 0)
        {
            holeSpriteRender.enabled = true;
            holeCircleCollider2D.enabled = true;
            holeAnimator.SetBool("Open", true);
            if (isOpened == false)
            {
                audioSource.Play();
                isOpened = true;
                coroutine = StartCoroutine(WaitForEnd());
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        ballAnimator.SetBool("InBlackHole", true);
        EventAggregator.instance.OnWinGame(menu.playerHealthPoint);
        collision.attachedRigidbody.constraints = RigidbodyConstraints2D.FreezePosition;
        collision.transform.position = Vector2.MoveTowards(collision.transform.position, transform.position.normalized, speed * Time.deltaTime);
        holeCircleCollider2D.enabled = false;
    }
    private IEnumerator WaitForEnd()
    {
        yield return new WaitForSeconds(49);
        EventAggregator.instance.OnLoseGame();
    }
    private IEnumerator SoundOff()
    {
        for(int i = 0; i < 100; i++)
        {
            yield return new WaitForSeconds(0.03f);
            audioSource.volume -= 0.01f;
        }
        audioSource.Stop();
    }
}
