using System.Collections;
using UnityEngine;

public class ChaoticLine : MonoBehaviour
{
    public Transform startObject; // Pierwszy obiekt, od którego zaczyna się linia
    public Transform endObject;   // Drugi obiekt, do którego prowadzi linia

    public int numberOfSegments = 10; // Liczba segmentów/łamań
    public float curveRandomOffset = 0.5f;  // Intensywność zmienności krzywej
    public float refreshRate = 1.0f;
    public float segmentDrawSpeed = 1.0f;

    public LineRenderer lR;
    public ParticleSystem particles;

    void Start()
    {
        lR = GetComponent<LineRenderer>();
        lR.positionCount = numberOfSegments + 1; 
        StartCoroutine(CreateChaoticLine());
    }

    void Update()
    {
    }

    private IEnumerator CreateChaoticLine()
    {
    
        Debug.Log("dupa");
        Vector3 startPosition = startObject.position;
        Vector3 endPosition = endObject.position;

        Vector3 prevPoint = startPosition;

        for (int i = 0; i <= numberOfSegments; i++)
        {
            float t = (float)i / (float)numberOfSegments;
            Vector3 point = Vector3.Lerp(startPosition, endPosition, t);

            point.x += Random.Range(-curveRandomOffset, curveRandomOffset);
            point.y += Random.Range(-curveRandomOffset, curveRandomOffset);

            //particles.transform.position = point;
            //particles.Emit(1);
            lR.enabled = true ;
            lR.SetPosition(i, point);
            prevPoint = point;
            yield return new WaitForSeconds(segmentDrawSpeed);
        }
        yield return new WaitForSeconds(refreshRate);
        StartCoroutine(CreateChaoticLine());
    }
}