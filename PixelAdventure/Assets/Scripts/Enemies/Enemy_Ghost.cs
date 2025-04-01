using NUnit.Framework;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy_Ghost : Enemy
{
    [Header("Ghost details")]
    [SerializeField] private float activeDuration;
    private float activeTimer;
    [Space]
    [SerializeField] private float xMinDistance;
    [SerializeField] private float yMinDistance;
    [SerializeField] private float yMaxDistance;

    private bool isChasing;
    private Transform target;

    protected override void Update()
    {
        base.Update();

        if (isDead)
            return;

        activeTimer -= Time.deltaTime;

        if (isChasing == false && idleTimer < 0)
        {
            StartChase();
        }
        else if (isChasing && activeTimer < 0)
        {
            EndChase();
        }

        HandleMovement();
    }

    private void HandleMovement()
    {
        if (canMove == false)
            return;

        HandleFlip(target.position.x);
        transform.position = Vector2.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
    }

    protected override void HandleAnimator()
    {

    }

    private void StartChase()
    {
        List<Player> playerList = PlayerManager.instance.GetPlayerList();

        if (playerList.Count <= 0)
        {
            EndChase();
            return;
        }

        int randomIndex = Random.Range(0, playerList.Count);
        target = playerList[randomIndex].transform;

        float xOffset = Random.Range(0, 100) < 50 ? -1 : 1;
        float yPosition = Random.Range(yMinDistance, yMaxDistance);

        transform.position = target.position + new Vector3(xMinDistance * xOffset, yPosition);


        activeTimer = activeDuration;
        isChasing = true;
        anim.SetTrigger("appear");
    }

    private void EndChase()
    {
        idleTimer = idleDuration;
        isChasing = false;
        anim.SetTrigger("desappear");
    }

    private void MakeInvisible()
    {
        sr.color = Color.clear;
        EnableColliders(false);
    }
    private void MakeVisible()
    {
        sr.color = Color.white;
        EnableColliders(true);
    }

    public override void Die()
    {
        base.Die();
        canMove = false;
    }
}