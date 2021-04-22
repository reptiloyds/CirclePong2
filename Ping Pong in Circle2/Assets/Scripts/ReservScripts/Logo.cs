using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Logo : MonoBehaviour
{
    [SerializeField] private SpriteRenderer sprRen;
    [SerializeField] private Sprite[] sprite;
    [SerializeField] private Animator animator;
    [SerializeField] private ParticleSystem circleParticle;
    [SerializeField] private ParticleSystem pongParticle;
    [SerializeField] private AudioSource audioSource;
    private int status = 0;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        audioSource.pitch = Random.Range(0.9f, 1.1f);
        audioSource.Play();
        PlayParticle();
        if (status <= 2)
        {
            animator.enabled = false;
            sprRen.sprite = sprite[status];
            status++;
        }
        else
        {
            animator.enabled = true;
            animator.SetTrigger("Recovery");
            status = 0;
        }
    }
    private void PlayParticle()
    {
        circleParticle.Play();
        pongParticle.Play();
    }
}
