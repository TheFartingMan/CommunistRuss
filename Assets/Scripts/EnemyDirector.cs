using UnityEngine;
using System.Collections.Generic;
using System;
public class EnemyDirector : MonoBehaviour
{
    [SerializeField] private int SetBagCredits;
    [SerializeField] private int BagCredits;

    private int[] basicEnemyCosts = new int[] { 1, 3, 7, 15, 31 };
    private int[] amountOfEnemies = new int[5]; // Automatically initialized to 0
    List<Enemy> PotentialEnemies = new List<Enemy>();
    List<Enemy> EnemiesInBag = new List<Enemy>();

    public float EnemySpawnTimer;
    private float EnemySpawnTime;
    public int total;
    [SerializeField] private int usedEnemies;
    [SerializeField] private int totalEnemyCost;

    [SerializeField] private GameObject[] enemyPrefabs; // size 5
    [SerializeField] private Vector2 spawnPoint; // where the enemy will appear

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
        string debugMessage = "Spent ";
        total = 0;

        for (int i = 0; i < basicEnemyCosts.Length; i++)
        {
            debugMessage += $"{basicEnemyCosts[i]}*({amountOfEnemies[i]})";
            if (i < basicEnemyCosts.Length - 1)
                debugMessage += ", ";
            total += basicEnemyCosts[i] * amountOfEnemies[i];
            amountOfEnemies[i] = 0; // Reset after logging
        }

        debugMessage += $". Spent a total of {total}";
        Debug.Log(debugMessage);


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
        if (EnemiesLeftInBag > 0)

        {
            Enemy enemyToSpawn = EnemiesInBag[usedEnemies];
            if (enemyToSpawn.prefabIndex >= 0 && enemyToSpawn.prefabIndex < enemyPrefabs.Length)
            {
                FindEnemySpawnPosition();
                Instantiate(enemyPrefabs[enemyToSpawn.prefabIndex], spawnPoint, Quaternion.identity);
                Debug.Log($"Spawned enemy of cost {enemyToSpawn.cost}");
                totalEnemyCost += enemyToSpawn.cost;
            }
            usedEnemies++;
            EnemySpawnTime = 0f;
        }
        else
        {
            EnemiesInBag.Clear();
            SetUpBag();
            usedEnemies = 0;
            totalEnemyCost = 0;
        }





    }

    public void FindEnemySpawnPosition()
    {
        float SharedRandomVariable = UnityEngine.Random.value;
        float randomPosx;
        float randomPosy;
        randomPosx = 7* Mathf.Cos(SharedRandomVariable*2*Mathf.PI);
        randomPosy = 7* Mathf.Sin(SharedRandomVariable*2*Mathf.PI);

        spawnPoint = new Vector2(randomPosx, randomPosy);
    }
        




}
