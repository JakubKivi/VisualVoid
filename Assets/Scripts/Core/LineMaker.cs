using System.Collections;
using System.Net;
using UnityEngine;

public class LineMaker : MonoBehaviour
{

    public Transform startObject;
    public Transform endObject;

    public float delayWhenFinished = 1.0f;


    public float drawDurationPerMeter = 2f; 

    private float elapsed = 0f;

    public ParticleSystem particles;

    private TrailRenderer trailRenderer;

    [Header("Perlin Noise")]
    public float noiseFrequency = 5f;  // jak często zmienia się szum
    public float noiseAmplitude = 2f;  // jak bardzo punkt może się wychylić

    void Start()
    {
        trailRenderer = GetComponent<TrailRenderer>();
        StartCoroutine(CreateChaoticLine());
    }

    void Update()
    {
    }

    private IEnumerator CreateChaoticLine()
    {
        Vector3 startPosition = startObject.position;
        Vector3 endPosition = endObject.position;

        float drawDuration = drawDurationPerMeter * Vector3.Distance(startPosition,endPosition);

        trailRenderer.emitting = false;
        transform.position = startPosition;
        yield return new WaitForSeconds(0.05f);
        trailRenderer.emitting = true;

        while (elapsed < drawDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / drawDuration;

            Vector3 point = Vector3.Lerp(startPosition, endPosition, t);

            point.x += Mathf.PerlinNoise(t * noiseFrequency, 0f) * noiseAmplitude - (noiseAmplitude / 2f);
            point.y += Mathf.PerlinNoise(0f, t * noiseFrequency) * noiseAmplitude - (noiseAmplitude / 2f);

            transform.position = point;
            particles.transform.position = point;
            particles.Emit(1);
            yield return new WaitForSeconds(0.001f);

        }
        elapsed = 0;


        yield return new WaitForSeconds(delayWhenFinished);
        GameObject clone = Instantiate(gameObject, startObject.position, transform.rotation);

        while (particles != null && particles.IsAlive(true))
        {
            yield return null;
        }
        Destroy(gameObject);
    }
}
