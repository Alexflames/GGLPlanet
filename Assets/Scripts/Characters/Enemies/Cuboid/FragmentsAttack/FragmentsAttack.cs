﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FragmentsAttack : CuboidAttack
{
    public override int priority
    {
        get
        {
            return 10;
        }
    }

    [SerializeField]
    private float AttackDuration;
    public override float duration
    {
        get
        {
            return AttackDuration;
        }
    }

    [SerializeField]
    private Color AttackColor;
    public override Color attackColor
    {
        get
        {
            return AttackColor;
        }
    }

    [SerializeField]
    private float TimeToEachSpawn = 0.25f;

    [SerializeField]
    private GameObject FragmentBombarder;
    [SerializeField]
    private GameObject FragmentBomb;

    [SerializeField]
    private int BombWaveProjectiles = 8;

    [SerializeField]
    private Vector2 spawnRange = new Vector2(5, 5);

    private GameObject player;

    private void Start()
    {
        spawnRange.x = spawnRange.x + gameObject.transform.position.x;
        spawnRange.y = spawnRange.y + gameObject.transform.position.y;
    }

    public override void AttStart()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player == null) return;

        timeToSpawn = TimeToEachSpawn;
    }

    public override void AttUpdate(float attackTimeLeft)
    {
        timeToSpawn -= Time.fixedDeltaTime;
        if (timeToSpawn <= 0 && attackTimeLeft > 0.5f)
        {
            Vector2 spawnPos = new Vector2(
                Random.Range(-spawnRange.x, spawnRange.x),
                Random.Range(-spawnRange.y, spawnRange.y));
            var bomber = Instantiate(FragmentBombarder,
                new Vector3(spawnPos.x, spawnPos.y, gameObject.transform.position.z),
                Quaternion.identity);
            bomber.transform.Rotate(new Vector3(0, 0, Random.Range(-180, 180)));
            timeToSpawn = TimeToEachSpawn;
        }
        //else if (attackTimeLeft < 0.5f && !waveSpawned)
        //{
        //    waveSpawned = true;
        //    bombs = new Transform[BombWaveProjectiles];
        //    for (int i = 0; i < BombWaveProjectiles; i++)
        //    {
        //        var rotation = Quaternion.Euler(0, 0, i / (BombWaveProjectiles * 1.0f) * 360);
        //        var spawnPos = rotation * Vector3.right;
        //        bombs[i] = Instantiate(FragmentBomb, gameObject.transform.position + spawnPos, rotation).transform;
        //    }
        //}
        //else if (waveSpawned)
        //{
        //    for (int i = 0; i < BombWaveProjectiles; i++)
        //    {
        //        bombs[i].Translate((bombs[i].right + Vector3.forward * Mathf.Sign(attackTimeLeft - 0.2f)) * Time.deltaTime, Space.Self);
        //    }
        //}
    }

    public override void AttEnd()
    {
        waveSpawned = false;
        if (player == null) return;
    }

    private float timeToSpawn = 0.25f;

    private bool waveSpawned = false;
    private Transform[] bombs;
}
