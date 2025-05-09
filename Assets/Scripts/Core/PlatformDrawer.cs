using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting;

public class PlatformDrawer : MonoBehaviour
{
    [Header("Ustawienia")]
    public GameObject platformEntity;
    public GameObject runner;
    private LineRenderer lineRenderer;

    public bool isDrawingActive = false;
    public float minDistance = 0.3f;
    public int spawnDelayMs = 50;
    public int maxSegments = 200;
    public float runnerClearRadius = 1f;

    private Vector3 lastSpawnPos;
    private Camera cam;
    private float lastSpawnTime;
    private bool isClone = false;
    private Queue<GameObject> spawnedSegments = new Queue<GameObject>();

    void Start()
    {
        cam = Camera.main;
        lastSpawnPos = Vector3.positiveInfinity;
        lastSpawnTime = Time.time * 1000f;
    }

    void Update()
    {
        if (isClone || !isDrawingActive || platformEntity == null)
            return;

        if (Input.GetMouseButton(0))
        {
            float nowMs = Time.time * 1000f;
            if (nowMs - lastSpawnTime < spawnDelayMs) return;

            Vector3 worldPos = GetMouseWorldPosition(0f);

            if (Vector3.Distance(worldPos, runner.transform.position) > runnerClearRadius)
            {
                float distance = Vector3.Distance(lastSpawnPos, worldPos);

                if (distance >= minDistance)
                {
                    if (distance >= 2 * minDistance)
                    {
                        Debug.Log("dupa");
                    }
                    //{
                    //    for (int i = 1; i < distance/minDistance; i++)
                    //    {
                    //        Debug.Log("dupa");
                    //        //SpawnCubeSegment(Vector3.Lerp(lastSpawnPos, worldPos, i/ (distance / minDistance)));
                    //    }
                    //}
                    SpawnCubeSegment(worldPos);
                    lastSpawnPos = worldPos;
                    lastSpawnTime = nowMs;
                }
            }             
                      
        }
        else
        {
            lastSpawnPos = Vector3.positiveInfinity;
        }
    }
    Vector3 GetMouseWorldPosition(float planeZ = 0f)
    {
        Vector3 mouseScreenPos = Input.mousePosition;
        float cameraZ = Camera.main.transform.position.z;
        float distance = Mathf.Abs(cameraZ - planeZ);
        mouseScreenPos.z = distance;
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(mouseScreenPos);
        worldPos.z = planeZ;
        return worldPos;
    }


    void Awake()
{
    if (gameObject.scene.name == null || gameObject.scene.name == "") 
    {
        isClone = true;
    }
}
    void SpawnCubeSegment(Vector3 pos)
    {
        GameObject segment = Instantiate(platformEntity, pos, Quaternion.identity);
        spawnedSegments.Enqueue(segment);

        if (spawnedSegments.Count > maxSegments)
        {
            GameObject oldest = spawnedSegments.Dequeue();
            if (oldest != null)
                Destroy(oldest);
        }
    }
}
