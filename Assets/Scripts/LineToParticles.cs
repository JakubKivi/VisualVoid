using UnityEngine;

public class LineToParticles : MonoBehaviour
{
    public ParticleSystem particleSystem;
    public ChaoticLine chaoticLine; // Referencja do skryptu ChaoticLine

    void Start()
    {
    }

    void Update()
    {
        // Pobieranie aktualnych pozycji cząsteczek z linii
        var main = particleSystem.main;
        ParticleSystem.Particle[] particles = new ParticleSystem.Particle[chaoticLine.numberOfSegments + 1];

        for (int i = 0; i <= chaoticLine.numberOfSegments; i++)
        {
            // Pobierz pozycję punktu linii
            Vector3 position = chaoticLine.lR.GetPosition(i);
            particles[i].position = position;
            particleSystem.Emit(1);
        }

    }
}
