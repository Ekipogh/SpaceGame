using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public Transform MainBody;
    private List<CelestialBody> CelestialBodies;
    public int NumberOfBodies;

    float minOrbitDistance = 5;
    float maxOrbitDistance = 25;

    public GameObject CelestialBodyPrefab;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CelestialBodies = new(NumberOfBodies);

        for (var i = 0; i < NumberOfBodies; i++)
        {
            GameObject gameObject = GameObject.Instantiate(CelestialBodyPrefab);
            SpriteRenderer renderer = gameObject.GetComponent<SpriteRenderer>();
            var random_red = UnityEngine.Random.value;
            var random_green = UnityEngine.Random.value;
            var random_blue = UnityEngine.Random.value;
            renderer.color = new Color(random_red, random_green, random_blue);

            float random_angle = UnityEngine.Random.Range(0.0f, (float)(2 * Math.PI));
            float random_rotation = UnityEngine.Random.Range(0.0f, (float)(2 * Math.PI));
            var random_semi_major = UnityEngine.Random.Range(minOrbitDistance, maxOrbitDistance);
            var random_semi_minor = UnityEngine.Random.Range(minOrbitDistance, random_semi_major);

            Orbit orbit = new(random_semi_major, random_semi_minor, random_rotation);


            CelestialBody celestialBody = new(gameObject, random_angle, orbit);
            CelestialBodies.Add(celestialBody);
        }
    }

    // Update is called once per frame
    void Update()
    {
        float deltaT = Time.deltaTime;
        foreach(var c in CelestialBodies)
        {
            c.Update(deltaT);
        }
    }
}
