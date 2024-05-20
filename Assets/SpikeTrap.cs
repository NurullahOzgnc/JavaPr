using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrap : MonoBehaviour
{
    public Transform teleport;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine(waitForHurt(collision));
        
    }
    IEnumerator waitForHurt(Collider2D collision)
    {
        collision.transform.position = teleport.transform.position;
        yield return new WaitForSeconds(0.05f);
        collision.GetComponent<Bow>().takeDamage(20);
    }
}
