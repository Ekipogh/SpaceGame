using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public Transform MainBody;
    private List<CelestialBody> CelestialBodies;
    public int NumberOfBodies;
    public GameObject CelestialBodyPrefab;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InitializeStarSystem();
    }

    void InitializeStarSystem()
    {
        CelestialBodies = new(NumberOfBodies);

        float minimalOrbitDistance = 5.0f;
        float maxOrbitDistance = 50.0f;

        for (var i = 0; i < NumberOfBodies; i++)
        {
            float random_angle = UnityEngine.Random.Range(0.0f, (float)(2 * Math.PI));
            Orbit orbit = GenerateRandomOrbit(MainBody, minimalOrbitDistance, maxOrbitDistance);

            CelestialBody celestialBody = InitializeCelestialBody(CelestialBodyPrefab, random_angle, orbit);
            CelestialBodies.Add(celestialBody);
            int numberOfMoons = UnityEngine.Random.Range(0, 4);

            // Moon are closer and faster
            float moonMinimalOrbitDistance = 0.3f;
            float moonMaxOrbitDistance = 1.0f;
            float moonOrbitalVelocityScale = 0.5f;
            float moonScale = 0.3f;
            for (var j = 0; j < numberOfMoons; j++)
            {
                Orbit moonOrbit = GenerateRandomOrbit(celestialBody.Transform, moonMinimalOrbitDistance, moonMaxOrbitDistance, velocityScale: moonOrbitalVelocityScale);
                float moon_angle = UnityEngine.Random.Range(0.0f, (float)(2 * Math.PI));
                CelestialBody moon = InitializeCelestialBody(CelestialBodyPrefab, moon_angle, moonOrbit, moonScale);
                CelestialBodies.Add(moon);
            }
        }
    }

    private Orbit GenerateRandomOrbit(Transform center, float minOrbitDistance = 1.5f, float maxOrbitDistance = 4.0f, float velocityScale = 0.1f)
    {
        float random_rotation = UnityEngine.Random.Range(0.0f, (float)(2 * Math.PI));
        var random_semi_major = UnityEngine.Random.Range(minOrbitDistance, maxOrbitDistance);
        var random_semi_minor = UnityEngine.Random.Range(minOrbitDistance, random_semi_major);
        var random_velocity = UnityEngine.Random.Range(
            velocityScale / random_semi_major,
            velocityScale / (random_semi_major * 0.5f)
        );

        return new(center, random_semi_major, random_semi_minor, random_rotation, random_velocity);
    }

    CelestialBody InitializeCelestialBody(GameObject prefab, float initialAngle, Orbit orbit, float scale = 1.0f)
    {
        GameObject gameObject = Instantiate(prefab);
        gameObject.transform.localScale = gameObject.transform.localScale * scale;
        CelestialBody celestialBody = new(gameObject, initialAngle, orbit);
        return celestialBody;
    }


    // Update is called once per frame
    void Update()
    {
        float deltaT = Time.deltaTime;
        foreach (var c in CelestialBodies)
        {
            c.Update(deltaT);
        }
    }
}
