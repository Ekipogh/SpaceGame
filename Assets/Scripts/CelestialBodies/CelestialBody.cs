using System;
using Unity.VisualScripting;
using UnityEngine;

class CelestialBody
{
    private const int CELESTIAL_BODY_Z = -10;
    float OrbitalAngle = 0;
    GameObject GameObject;

    private Orbit orbit;

    public CelestialBody(GameObject gameObject, float initialAngel, Orbit orbit)
    {
        this.GameObject = gameObject;
        this.OrbitalAngle = initialAngel;
        this.orbit = orbit;
        var x = orbit.OrbitalRadius(initialAngel) * Mathf.Cos(initialAngel);
        var y = orbit.OrbitalRadius(initialAngel) * Mathf.Sin(initialAngel);

        gameObject.transform.position = new Vector3(x, y, CELESTIAL_BODY_Z);
    }

    public void Update(float deltaT)
    {
        OrbitalAngle += deltaT;
        float new_radius = orbit.OrbitalRadius(OrbitalAngle);
        float x = new_radius * Mathf.Cos(OrbitalAngle - orbit.Rotation);
        float y = new_radius * Mathf.Sin(OrbitalAngle - orbit.Rotation);
        GameObject.transform.position = new(x, y, CELESTIAL_BODY_Z);
    }
}