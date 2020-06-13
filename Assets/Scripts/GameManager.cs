using System;
using System.Collections;
using TMPro;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public GameObject ballPrefab;

    public float xBound = 3f;
    public float yBound = 3f;
    public float ballSpeed = 3f;
    public float respawnDelay = 2f;
    public int[] playerScores;

    public TextMeshProUGUI mainText;
    public TextMeshProUGUI[] playerTexts;

    private EntityManager entityManager;
    private BlobAssetStore blobAssetStore;
    
    private Entity ballEntityPrefab;

    private WaitForSeconds oneSecond;
    private WaitForSeconds delay;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        playerScores = new int[2];

        entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
        blobAssetStore = new BlobAssetStore();

        var settings = GameObjectConversionSettings.FromWorld(World.DefaultGameObjectInjectionWorld, blobAssetStore);
        ballEntityPrefab = GameObjectConversionUtility.ConvertGameObjectHierarchy(ballPrefab, settings);
        

        oneSecond = new WaitForSeconds(1f);
        delay = new WaitForSeconds(respawnDelay);

        StartCoroutine(CountdownAndSpawnBall());
    }

    private void OnDisable()
    {
        blobAssetStore.Dispose();
    }

    public void PlayerScored(int playerID)
    {
        playerScores[playerID]++;
        for (int i = 0; i < playerScores.Length && i < playerTexts.Length; i++)
            playerTexts[i].text = playerScores[i].ToString();

        StartCoroutine(CountdownAndSpawnBall());
    }

    IEnumerator CountdownAndSpawnBall()
    {
        mainText.text = "Get Ready";
        yield return delay;

        mainText.text = "3";
        yield return oneSecond;

        mainText.text = "2";
        yield return oneSecond;

        mainText.text = "1";
        yield return oneSecond;

        mainText.text = "";

        SpawnBall();
    }

    void SpawnBall()
    {
        var ball = entityManager.Instantiate(ballEntityPrefab);
        
        var dir = new Vector3(
            Random.Range(0, 2) == 0 ? -1f : 1f,
            Random.Range(-0.5f, 0.5f),
            0f).normalized;
        var speed = dir * ballSpeed;
        
        var velocity = new PhysicsVelocity()
        {
            Linear = speed,
            Angular = float3.zero
        };

        entityManager.AddComponentData(ball, velocity);
    }
}
