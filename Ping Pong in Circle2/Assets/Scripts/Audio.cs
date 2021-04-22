using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class Audio : MonoBehaviour
{
    [SerializeField] private AudioSource bombAudioSource;
    [SerializeField] private AudioSource heartAudioSource;
    [SerializeField] private AudioSource starsAudioSource;
    [SerializeField] private AudioSource raimbowAudioSource;
    void Start()
    {
        SubscribeEvents();
    }
    private void OnWinGame(object sender, WinGameEventArgs e)
    {
        var countStars = e.HealthPoint;
        StartCoroutine(PlayStarSounds(countStars));
        UnsubscribeEvents();
    }
    private void OnGetDamage()
    {
        heartAudioSource.pitch = Random.Range(0.9f, 1.1f);
        heartAudioSource.Play();
    }

    private void OnExplosion()
    {
        bombAudioSource.pitch = Random.Range(0.9f, 1.1f);
        bombAudioSource.PlayOneShot(bombAudioSource.clip);
    }
    private void OnRaimbowBrick()
    {
        raimbowAudioSource.pitch = Random.Range(0.9f, 1.1f);
        raimbowAudioSource.PlayOneShot(raimbowAudioSource.clip);
    }
    private void SubscribeEvents()
    {
        EventAggregator.instance.WinGame += OnWinGame;
        EventAggregator.instance.Explosion += OnExplosion;
        EventAggregator.instance.GetDamage += OnGetDamage;
        EventAggregator.instance.RaimbowBrick += OnRaimbowBrick;
    }

    private void UnsubscribeEvents()
    {
        EventAggregator.instance.WinGame -= OnWinGame;
        EventAggregator.instance.Explosion -= OnExplosion;
        EventAggregator.instance.GetDamage -= OnGetDamage;
        EventAggregator.instance.RaimbowBrick -= OnRaimbowBrick;
    }
    private IEnumerator PlayStarSounds(int countstar)
    {
        yield return new WaitForSeconds(1.45f);
        for(int i = 0; i < countstar; i++)
        {
            yield return new WaitForSeconds(0.4f);
            starsAudioSource.pitch = Random.Range(0.95f, 1.05f);
            starsAudioSource.PlayOneShot(starsAudioSource.clip);
        }
    }
}
