using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{

    private Animator ani;
    private Rigidbody2D rb;

    [SerializeField] private AudioSource SoundEffDeath;

    void Start()
    {
        ani = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        Debug.Log("Entering on collising");
        if (coll.gameObject.CompareTag("Trap"))
        {
            Debug.Log("Trap");
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Dying");
        SoundEffDeath.Play();
        rb.bodyType = RigidbodyType2D.Static;
        ani.SetTrigger("death");
    }

    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
