using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Knockback : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D rb;

    [SerializeField]
    private float knockbackStrength =16;
    private float delay = 0.5f;

    public UnityEvent OnBegin, OnDone;

    public void Knockingback(GameObject sender)
    {
        StopAllCoroutines();
        OnBegin?.Invoke();
        Vector2 direction = (transform.position - sender.transform.position).normalized;
        rb.AddForce(direction * knockbackStrength, ForceMode2D.Impulse);
        StartCoroutine(Reset());
    }


    private IEnumerator Reset()
    {
        yield return new WaitForSeconds(delay);
        rb.velocity= Vector3.zero;
        OnDone?.Invoke();
    }
}
