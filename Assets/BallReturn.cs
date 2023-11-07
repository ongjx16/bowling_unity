using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq; // Required for LINQ extension methods like .Where()

public class BallReturn : MonoBehaviour
{
    public GameObject bowlingBallPrefab; // Assign your bowling ball prefab in the inspector
    private float spacing = 0.25f; // Distance between the balls
    private Vector3 spawnOffset = new Vector3(2.0f, 0.75f, -7f);
    private Queue<GameObject> ballsOnTray = new Queue<GameObject>();
    private int maxBallsOnTray = 4;
    private Transform spawnPoint; // No need to set in inspector, it's the same as this GameObject's transform


    // Start is called before the first frame update
    void Start()
    {
        spawnPoint = this.transform; // Set the spawnPoint to this GameObject's transform
        bowlingBallPrefab = Resources.Load<GameObject>("Prefabs/BowlingBall");

        for (int i = 0; i < maxBallsOnTray; i++)
        {
            SpawnBowlingBall(i);
        }
    }

    // Update is called once per frame
    void Update()
    {
        while (ballsOnTray.Count < maxBallsOnTray)
        {
            SpawnBowlingBall(ballsOnTray.Count);
        }
    }

    void SpawnBowlingBall(int index)
    {
        Vector3 newPosition = spawnPoint.position + spawnOffset + spawnPoint.forward * index * spacing;
        GameObject newBall = Instantiate(bowlingBallPrefab, newPosition, spawnPoint.rotation);
        ballsOnTray.Enqueue(newBall);
    }

}
