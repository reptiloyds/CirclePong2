using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BlockLogic : MonoBehaviour
{
    protected enum Triggers
    {
        Shake,
        Explosion
    }
    protected Animator cameraAnimator;
    protected int healthPoint;
    protected float timeToDestroy;
    protected string trigger;
    protected void GetDamage()
    {
        cameraAnimator.SetTrigger(trigger);
        healthPoint--;
        if (healthPoint == 0)
        {
            Destroy();
        }
    }

    protected virtual void Destroy()
    {
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        StartCoroutine(WaitingToDestroy(timeToDestroy));
    }

    protected IEnumerator WaitingToDestroy(float timeToDestroy)
    {
        yield return new WaitForSeconds(timeToDestroy);
        Destroy(gameObject);
    }
}
