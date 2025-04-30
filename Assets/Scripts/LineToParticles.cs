using UnityEngine;

public class LineToParticles : MonoBehaviour
{
    public ParticleSystem particleSystem;
    public ChaoticLine chaoticLine; // Referencja do skryptu ChaoticLine

    void Start()
    {
        // Ustawienie parametrów cząsteczek do efektywnego zanikania i pojawiania się
        var main = particleSystem.main;
        main.startSize = 0.1f;  // Ustawienie początkowego rozmiaru cząsteczek
        main.startLifetime = 1f;  // Ustawienie czasu życia cząsteczek
        main.startColor = Color.white; // Ustawienie początkowego koloru cząsteczek
    }

    void Update()
    {
        // Pobieranie aktualnych pozycji cząsteczek z linii
        var main = particleSystem.main;
        ParticleSystem.Particle[] particles = new ParticleSystem.Particle[chaoticLine.numberOfSegments + 1];

        for (int i = 0; i <= chaoticLine.numberOfSegments; i++)
        {
            // Pobierz pozycję punktu linii
            Vector3 position = chaoticLine.lineRenderer.GetPosition(i);
            particles[i].position = position;

            // Dostosowanie wielkości cząsteczek na podstawie czasu życia
            float lifeRatio = (float)i / (float)chaoticLine.numberOfSegments;
            particles[i].startSize = Mathf.Lerp(0.1f, 1f, lifeRatio);  // Cząsteczki stopniowo rosną

            // Dodaj efekt zanikania cząsteczek
            particles[i].startColor = new Color(1f, 1f, 1f, Mathf.Lerp(1f, 0f, lifeRatio));  // Zanik oparty na czasie życia
        }

        // Zastosowanie cząsteczek do systemu
        particleSystem.SetParticles(particles, particles.Length);
    }
}
