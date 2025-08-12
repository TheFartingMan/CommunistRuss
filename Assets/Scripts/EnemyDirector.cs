using UnityEngine;
using System.Collections.Generic;
using System;
using System.Runtime.CompilerServices;
using System.Linq;
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
    [SerializeField] private float WavePercentageOfTrickle = .90f;

    [SerializeField] private float EnemySpawnPoint;

    [Header("Enemy Debugging")]
    [SerializeField] private int usedEnemies;
    [SerializeField] private int totalEnemyCost;

    [SerializeField] private Vector2 spawnPoint;
    [SerializeField] private int WaveCounter;
    private int PotentialEnemyCounter = 0;
    private bool WaveIsOver = false;
    private int EnemiesLeftInBag;
    private int WaveEnemyIndex = 0;
    private bool PrepForWave;

    void Update()
    {
        EnemySpawnTime += Time.deltaTime;
        if (EnemySpawnTime >= EnemySpawnTimer && EnemiesLeftInBag > 0)
        {
            SpawnEnemy();
        }
        if (EnemySpawnTime >= EnemySpawnTimer * .2 && EnemiesLeftInBag <= 0)
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
        PotentialEnemies.Add(new Enemy(basicEnemyCosts[0], 0, () => amountOfEnemies[0]++));

        SetUpBag();
        usedEnemies = 0;
        WaveCounter = 0;
        PrepForWave = true;
    }

    public void SpawnEnemy()
    {
        EnemiesLeftInBag = EnemiesInBag.Count - usedEnemies;
        
        

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
            if (PrepForWave == true)
            {
                RemoveRandomTricklePercent(EnemiesInBag);
                //turn on huge wave text
                PrepForWave = false;
            }
            

            if (PrepForWave == false)
            {
                Enemy enemyToSpawn = EnemiesInBag[WaveEnemyIndex];
                FindEnemySpawnPosition();
                Instantiate(enemyPrefabs[enemyToSpawn.prefabIndex], spawnPoint, Quaternion.identity);
                WaveEnemyIndex++;
                EnemySpawnTime = 0f;
            }



            if (WaveEnemyIndex >= EnemiesInBag.Count)
            {
              WaveIsOver = true;  
            }
           

            
        }

        //Refill and increase difficulty

        if (WaveIsOver == true)
        {
            EnemiesInBag.Clear();
            IncreaseDifficulty();
            SetUpBag();
            usedEnemies = 0;
            totalEnemyCost = 0;
            WaveIsOver = false;
            WaveEnemyIndex = 0;
            WaveCounter++;
            PrepForWave = true;
        }





    }

    public void FindEnemySpawnPosition()
    {
        float SharedRandomVariable = UnityEngine.Random.value;
        float randomPosx;
        float randomPosy;
        randomPosx = EnemySpawnPoint * Mathf.Cos(SharedRandomVariable * 2 * Mathf.PI);
        randomPosy = EnemySpawnPoint * Mathf.Sin(SharedRandomVariable * 2 * Mathf.PI);

        spawnPoint = new Vector2(randomPosx, randomPosy);
    }

    public void IncreaseDifficulty()
    {
        SetBagCredits = (int)Math.Floor(SetBagCredits * 1.5);
        EnemySpawnTimer -= .02f;

        if (WaveCounter % 3 == 2)
        {
            AddNextPotentialEnemy();
            return;
        }
    }

    public void AddNextPotentialEnemy()
    {
        PotentialEnemyCounter++;
        PotentialEnemies.Add(new Enemy(basicEnemyCosts[PotentialEnemyCounter], PotentialEnemyCounter, () => amountOfEnemies[PotentialEnemyCounter]++));
    }

void RemoveRandomTricklePercent<T>(List<T> list)
    {
        int countToRemove = Mathf.FloorToInt(list.Count * (1-WavePercentageOfTrickle));

        // Shuffle the list indices randomly
        List<int> indices = Enumerable.Range(0, list.Count).OrderBy(i => UnityEngine.Random.value).ToList();

        // Remove from the end (to avoid index shift issues)
        for (int i = 0; i < countToRemove; i++)
        {
            int indexToRemove = indices[i];
            list.RemoveAt(indexToRemove - i); // adjust for shifted indices
        }
    }

}
