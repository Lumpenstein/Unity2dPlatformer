using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollect : MonoBehaviour
{

    private int collectedBananas = 0;

    [SerializeField] private Text pointCounterText;

    [SerializeField] private AudioSource SoundEffCollect;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Banana"))
        {
            // Destroy collected item
            Destroy(collision.gameObject);
            // Increment counter
            collectedBananas++;
            SoundEffCollect.Play();
            pointCounterText.text = "Points: " + collectedBananas * 10;
        }
    }
}
