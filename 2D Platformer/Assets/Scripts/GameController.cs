using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    Vector2 checkpointPos;
    Rigidbody2D playerRb;

    AudioManager audioManager;

    private void Awake()
    {
        playerRb = GetComponent<Rigidbody2D>();
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

    //As soon as the game start checkPointPos variable will be set to the player's starting position
    private void Start()
    {
        checkpointPos = transform.position;
    }

    //Player will die when touch an obstacle
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle"))
        {
            audioManager.PlaySFX(audioManager.death);
            Die();
        }
    }

    //Corutine will start when player dies
    void Die()
    {
        StartCoroutine(Respawn(0.5f));
    }

    //When the player reaches any checkpoint, will change the position of the checkpointPos variable
    public void UpdateCheckpoint(Vector2 pos)
    {
        checkpointPos = pos;
    }

    IEnumerator Respawn(float duration)
    {
        //As soon as the player hits the obstacle, will disabled the simulated feature of the rigid body component
        playerRb.simulated = false;

        //As soon as the player hits the obstacle, will reset the player's speed
        playerRb.velocity = new Vector2(0, 0);

        //When the player hits obstacle, it will become invisible
        transform.localScale = new Vector3(0, 0, 0);

        yield return new WaitForSeconds(duration);
        transform.position = checkpointPos;

        //It will become visible again, after the cooldown expires
        transform.localScale = new Vector3(1, 1, 1);

        //As soon as player respawn, will enable the simulated feature
        playerRb.simulated = true;

    }

}
