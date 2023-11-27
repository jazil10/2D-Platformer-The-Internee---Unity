using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeHead : EnemyDamage
{
    [Header("Spikehead Properties")]
    [SerializeField] private float speed;
    [SerializeField] private float range;
    [SerializeField] private float checkDelay;
    [SerializeField] private LayerMask playerLayer;
    private float checkTimer;
    private Vector3 destination;
    private bool attacking;
    private Transform player;

    [Header("Spikehead Sound")]
    [SerializeField] private AudioClip spikeheadSound;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        Stop();
    }

    private void Update()
    {
        if (attacking)
        {
            Vector3 direction = (player.position - transform.position).normalized;
            transform.Translate(direction * Time.deltaTime * speed);
        }
        else
        {
            checkTimer += Time.deltaTime;
            if (checkTimer >= checkDelay)
            {
                CheckForPlayer();
            }
        }
    }

    private void CheckForPlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, range, playerLayer);

        if (hit.collider != null && !attacking)
        {
            attacking = true;
            checkTimer = 0;
        }
    }

    private void Stop()
    {
        attacking = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SoundManager.instance.PlaySound(spikeheadSound);
            base.OnTriggerEnter2D(collision);
            Stop();
        }
    }
}
