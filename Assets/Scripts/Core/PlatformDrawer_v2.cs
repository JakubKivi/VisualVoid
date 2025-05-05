using UnityEngine;
using System.Collections.Generic;

public class PlatformDrawer_v2 : MonoBehaviour
{
    [Header("Ustawienia")]
    public GameObject platformEntity;
    public bool isDrawingActive = false;
    public float minDistance = 0.3f;
    public int spawnDelayMs = 50;
    public int maxSegments = 200;

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

            // pobierz worldPos na płaszczyźnie z = 0
            Vector3 worldPos = GetMouseWorldPosition(0f);

            if (Vector3.Distance(lastSpawnPos, worldPos) >= minDistance)
            {
                SpawnCubeSegment(worldPos);
                lastSpawnPos = worldPos;
                lastSpawnTime = nowMs;
            }
        }
        else
        {
            lastSpawnPos = Vector3.positiveInfinity;
        }
    }
    /// Zwraca pozycję myszy w world space na płaszczyźnie z = 0 (albo innej zadeklarowanej)
    Vector3 GetMouseWorldPosition(float planeZ = 0f)
    {
        // 1. Pobierz pozycję w pikselach
        Vector3 mouseScreenPos = Input.mousePosition;
        // 2. Ustaw odległość od kamery do płaszczyzny, na której rysujesz
        //    jeśli kamera stoi na z = -15 i chcesz rysować na z = 0, to distance = 15
        float cameraZ = Camera.main.transform.position.z;
        float distance = Mathf.Abs(cameraZ - planeZ);
        mouseScreenPos.z = distance;
        // 3. Rzutuj na world space
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(mouseScreenPos);
        // 4. Wymus strefę Z (po prostu żeby mieć pewność)
        worldPos.z = planeZ;
        return worldPos;
    }


    void Awake()
{
    if (gameObject.scene.name == null || gameObject.scene.name == "") 
    {
        // Obiekt nie należy do aktywnej sceny = został sklonowany z prefaba
        isClone = true;
    }
}

    Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 10f;
        return cam.ScreenToWorldPoint(mousePos);
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
