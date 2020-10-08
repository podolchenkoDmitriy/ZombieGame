﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    // Start is called before the first frame update
    public LevelData[] levels;

    public ExtraWaveData[] extraLevels;
    private int _numberOfCurrentLevel = 0;
    private int _countOfNormalZombies;
    private int _boundForSpawn;
    private float _timeBetweenSpawn;
    private GameObject[] normalZombiePrefab;
    private GameObject clownZombiePrefab;
    private GameObject bossPrefab;

    public Transform[] spawnPoints;
    public Transform bossSpawnPoint;
    Transform player;
    private void Start()
    {
        player = FindObjectOfType<PlayerController>().transform;
        StartCoroutine(CountTimeForStart());
    }
    private void SetupLevelData()
    {

        _numberOfCurrentLevel = levels[_numberOfCurrentLevel].NumberOfMainLevel;
        _countOfNormalZombies = levels[_numberOfCurrentLevel].CountOfNormalZombies;
        _timeBetweenSpawn = levels[_numberOfCurrentLevel].TimeBetweenSpawn;
        _boundForSpawn = levels[_numberOfCurrentLevel].BoundsForNormalZombiesCount;
        normalZombiePrefab = levels[_numberOfCurrentLevel].NormalZombiesPrefab;
        clownZombiePrefab = levels[_numberOfCurrentLevel].ClownZombiePrefab;
        bossPrefab = levels[_numberOfCurrentLevel].BossPrefab;
    }
    public static List<GameObject> normalZombies = new List<GameObject>();
    private int time = 5;

    private IEnumerator CountTimeForStart()
    {
        UIInstance.instance.countStartText.gameObject.SetActive(true);
        while (time > 0)
        {
            UIInstance.instance.countStartText.text = time.ToString();

            time--;
            yield return new WaitForSeconds(1f);


        }
        UIInstance.instance.countStartText.gameObject.SetActive(false);
        yield return StartCoroutine(StartSpawnNormalWave());
    }
    IEnumerator ShowText(string text)
    {
        
        UIInstance.instance.countStartText.gameObject.SetActive(true);
        UIInstance.instance.countStartText.text = text;
        yield return new WaitForSeconds(1f);
        UIInstance.instance.countStartText.gameObject.SetActive(false);

    }
    private IEnumerator StartSpawnNormalWave()
    {
        while (normalZombies.Count != 0)
        {
            yield return new WaitForSeconds(0.5f);

        }
        yield return new WaitForFixedUpdate();

        SetupLevelData();

        StartCoroutine(ShowText("Wave " + (_numberOfCurrentLevel + 1).ToString()));

        int numOfZombiesSpawned = 0;

        while (numOfZombiesSpawned < _countOfNormalZombies)
        {
            if (normalZombies.Count < _boundForSpawn)
            {
                numOfZombiesSpawned++;

                GameObject zomb;
                int n = Random.Range(0, spawnPoints.Length);

                if (Vector3.Distance(spawnPoints[n].position, player.position) < 20f)
                {
                    if (n== spawnPoints.Length - 1)
                    {
                        n--;
                    }
                    else 
                    {
                        n++;
                    }

                }

                int k = Random.Range(0, normalZombiePrefab.Length - 1);

                zomb = Instantiate(normalZombiePrefab[k], spawnPoints[n].position, Quaternion.identity);
                normalZombies.Add(zomb);
            }


            yield return new WaitForSeconds(_timeBetweenSpawn);
        }
        _numberOfCurrentLevel++;
        PlayerPrefs.SetInt("Level", _numberOfCurrentLevel);
        if (_numberOfCurrentLevel > 2)
        {
            yield return StartCoroutine(BossSpawn());

        }
        else
        {
            yield return StartCoroutine(SpawnExtraWave());
        }
    }

    private IEnumerator SpawnExtraWave()
    {
        while (normalZombies.Count != 0)
        {
            yield return new WaitForSeconds(0.5f);

        }

        yield return new WaitForFixedUpdate();
        StartCoroutine(ShowText("ExtraWave " + (_numberOfCurrentLevel).ToString()));

        
        for (int i = 0; i < spawnPoints.Length - 1; i++)
        {
            int numOfNormalZombies = extraLevels[_numberOfCurrentLevel - 1].CountOfNurmalZombies[i];
            int numOfClowns = extraLevels[_numberOfCurrentLevel - 1].CountOfClowns[i];
            if (numOfNormalZombies > 0)
            {
                for (int j = 0; j < numOfNormalZombies; j++)
                {
                    GameObject zomb;
                    int k = Random.Range(0, normalZombiePrefab.Length - 1);
                    zomb = Instantiate(normalZombiePrefab[k], spawnPoints[i].transform.position, Quaternion.identity);

                    normalZombies.Add(zomb);
                    yield return new WaitForSeconds(0.2f);
                }

            }
            yield return new WaitForFixedUpdate();

            if (numOfClowns > 0)
            {
                for (int z = 0; z < numOfClowns; z++)
                {
                    GameObject zomb;
                    zomb = Instantiate(clownZombiePrefab, spawnPoints[i].transform.position, Quaternion.identity);
                    normalZombies.Add(zomb);
                }
            }
            yield return new WaitForFixedUpdate();
        }

        yield return StartCoroutine(StartSpawnNormalWave());

    }

    private IEnumerator BossSpawn()
    {
        while (normalZombies.Count > 0)
        {
            yield return new WaitForSeconds(0.5f);
        }
        StartCoroutine(ShowText("BossFight"));

       
        GameObject boss = Instantiate(bossPrefab, bossSpawnPoint.transform.position, Quaternion.identity);
        Camera.main.GetComponent<CameraFollow>().Shake(0.2f, 0.5f);
        normalZombies.Add(boss);

        yield return new WaitForSeconds(0.5f);

        yield return StartCoroutine(DetectWin());
    }

    private IEnumerator DetectWin()
    {
        while (normalZombies.Count > 0)
        {
            yield return new WaitForSeconds(1f);
        }

        UIInstance.instance.winPanel.gameObject.SetActive(true);
        Time.timeScale = 0;

    }
}
