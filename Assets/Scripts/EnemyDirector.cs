using UnityEngine;
using System.Collections.Generic;
using System;
public class EnemyDirector : MonoBehaviour
{

    private int BagCredits;

    private int[] basicEnemyCosts = new int[] { 1, 3, 7, 15, 31 };
    public int[] amountOfEnemies = new int[5]; // Automatically initialized to 0
    List<Enemy> PotentialEnemies = new List<Enemy>();
    List<Enemy> EnemiesInBag = new List<Enemy>();
    [Header("Parameters")]
    [SerializeField] private int SetBagCredits;
    public float EnemySpawnTimer;
    [SerializeField] private GameObject[] enemyPrefabs;
    private float EnemySpawnTime;

    [SerializeField] private float EnemySpawnPoint;

    [Header("Enemy Debugging")]
    [SerializeField] private int usedEnemies;
    [SerializeField] private int totalEnemyCost;

    [SerializeField] private Vector2 spawnPoint;

    void Update()
    {
        EnemySpawnTime += Time.deltaTime;
        if (EnemySpawnTime >= EnemySpawnTimer)
        {
        SpawnEnemy();
        }

    }


    private void SetUpBag()
    {
        BagCredits = SetBagCredits;
        while (BagCredits > 0)
        {
            int TemporaryRandomVariable = UnityEngine.Random.Range(0, PotentialEnemies.Count);

            Enemy selectedEnemy = PotentialEnemies[TemporaryRandomVariable];

            if (BagCredits >= selectedEnemy.cost)
            {
                BagCredits -= selectedEnemy.cost;
                selectedEnemy.onBuy.Invoke();
                EnemiesInBag.Add(selectedEnemy);
            }



        }




        for (int i = 0; i < basicEnemyCosts.Length; i++)
        {
            amountOfEnemies[i] = 0; // Reset after logging
        }



    }

    public class Enemy
    {
        public int cost;
        public Action onBuy;
        public int prefabIndex;

        public Enemy(int cost, int prefabIndex, Action onBuy)
        {
            this.cost = cost;
            this.onBuy = onBuy;
            this.prefabIndex = prefabIndex;
        }
    }

    void Start()
    {
        for (int i = 0; i < basicEnemyCosts.Length; i++)
        {
            int index = i; // capture loop variable
            PotentialEnemies.Add(new Enemy(basicEnemyCosts[i], index, () => amountOfEnemies[index]++));
        }
        SetUpBag();
        usedEnemies = 0;
    }

    public void SpawnEnemy()
    {
        int EnemiesLeftInBag = EnemiesInBag.Count - usedEnemies;
        bool WaveIsOver = false;
        float WavePercentageOfTrickle = .90f;

        //If the bag still has stuff

        if (EnemiesLeftInBag > 0)

        {
            Enemy enemyToSpawn = EnemiesInBag[usedEnemies];
            if (enemyToSpawn.prefabIndex >= 0 && enemyToSpawn.prefabIndex < enemyPrefabs.Length)
            {
                FindEnemySpawnPosition();
                Instantiate(enemyPrefabs[enemyToSpawn.prefabIndex], spawnPoint, Quaternion.identity);
                totalEnemyCost += enemyToSpawn.cost;
            }
            usedEnemies++;
            EnemySpawnTime = 0f;
        }

        //Wave
        if (EnemiesLeftInBag <= 0)
        {


            for (int i = 0; i < (EnemiesInBag.Count * WavePercentageOfTrickle); i++)
            {
                Enemy enemyToSpawn = EnemiesInBag[i];
                FindEnemySpawnPosition();
                Instantiate(enemyPrefabs[enemyToSpawn.prefabIndex], spawnPoint, Quaternion.identity);
            }


            WaveIsOver = true;
        }

        //Refill and increase difficulty

        if (WaveIsOver == true)
        {
            EnemiesInBag.Clear();
            SetUpBag();
            IncreaseDifficulty();
            usedEnemies = 0;
            totalEnemyCost = 0;
            WaveIsOver = false;
        }





    }

    public void FindEnemySpawnPosition()
    {
        float SharedRandomVariable = UnityEngine.Random.value;
        float randomPosx;
        float randomPosy;
        randomPosx = EnemySpawnPoint * Mathf.Cos(SharedRandomVariable*2*Mathf.PI);
        randomPosy = EnemySpawnPoint * Mathf.Sin(SharedRandomVariable*2*Mathf.PI);

        spawnPoint = new Vector2(randomPosx, randomPosy);
    }

    public void IncreaseDifficulty()
    {
        SetBagCredits = (int)Math.Floor(SetBagCredits * 1.10);
        EnemySpawnTimer = EnemySpawnTimer * 0.99f;
    }



}
