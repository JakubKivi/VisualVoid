using System.Collections;
using UnityEngine;

public class ChaoticLine : MonoBehaviour
{
    public Transform startObject; // Pierwszy obiekt, od którego zaczyna się linia
    public Transform endObject;   // Drugi obiekt, do którego prowadzi linia
    public int numberOfSegments = 10; // Liczba segmentów/łamań
    public float sharpness = 1f;    // Ostrość zakrętów
    public float stepSize = 0.1f;   // Odległość, o jaką przesuwa się linia co klatkę
    public float curveIntensity = 0.5f; // Intensywność zmienności krzywej

    public LineRenderer lineRenderer;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = numberOfSegments + 1; // Dla każdego segmentu + 1 punkt
    }

    void Update()
    {
        CreateChaoticLine();
    }

    void CreateChaoticLine()
    {
        Vector3 startPosition = startObject.position;
        Vector3 endPosition = endObject.position;

        // Dodaj przesunięcie w prawo na każdą klatkę
        startPosition.x += stepSize * Time.time;

        for (int i = 0; i <= numberOfSegments; i++)
        {
            float t = (float)i / (float)numberOfSegments;
            Vector3 point = Vector3.Lerp(startPosition, endPosition, t);

            // Wprowadź losowość (chaotyczność) krzywej
            float offsetX = Random.Range(-curveIntensity, curveIntensity);
            float offsetY = Random.Range(-curveIntensity, curveIntensity);
            point.x += offsetX * sharpness;
            point.y += offsetY * sharpness;

            lineRenderer.SetPosition(i, point);
        }
    }
}