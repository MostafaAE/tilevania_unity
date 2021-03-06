using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    [SerializeField] AudioClip coinPickup;
    [SerializeField] int coinValue = 100;
    bool wasCollected = false;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !wasCollected)
        {
            wasCollected = true;
            FindObjectOfType<GameSession>().increaseScore(coinValue);
            AudioSource.PlayClipAtPoint(coinPickup, Camera.main.transform.position);
            Destroy(this.gameObject); 
        }
    }

}
