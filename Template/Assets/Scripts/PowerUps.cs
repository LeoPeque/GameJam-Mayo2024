using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUps : MonoBehaviour
{
    public GameObject[] objectsToSpawn;
    public float spawnInterval = 2f;  // Interval between spawns

    private float screenWidthWorldUnits;  // Screen width in world units

    void Start()
    {
        // Check if Camera.main is available
        if (UnityEngine.Camera.main == null)
        {
            Debug.LogError("Main Camera not found. Make sure your camera has the 'MainCamera' tag.");
            return;
        }

        float height = 2f * UnityEngine.Camera.main.orthographicSize;
        screenWidthWorldUnits = height * UnityEngine.Camera.main.aspect;

        StartCoroutine(SpawnObjects());
    }

    IEnumerator SpawnObjects()
    {
        while (true)
        {
            GameObject[] powerUps = GameObject.FindGameObjectsWithTag("PowerUp");
            if (powerUps.Length == 2)
            {
                int randomIndex = Random.Range(0, objectsToSpawn.Length);
                Vector3 spawnPosition = new Vector3(Random.Range(-screenWidthWorldUnits / 2, screenWidthWorldUnits / 2), 11, 0);
                Instantiate(objectsToSpawn[randomIndex], spawnPosition, Quaternion.identity);
            }
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
