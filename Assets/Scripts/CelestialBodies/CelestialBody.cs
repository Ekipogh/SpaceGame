using UnityEngine;

class CelestialBody
{
    private const int CELESTIAL_BODY_Z = 10;
    private float orbitalAngle = 0;
    private readonly GameObject gameObject;
    private readonly Orbit orbit;

    public Transform Transform { get => gameObject.transform; }

    public CelestialBody(GameObject gameObject, float initialAngle, Orbit orbit)
    {
        this.gameObject = gameObject;
        this.orbitalAngle = initialAngle;
        this.orbit = orbit;
        float radius = orbit.OrbitalRadius(initialAngle);
        float x = orbit.Center.x + radius * Mathf.Cos(initialAngle);
        float y = orbit.Center.y + radius * Mathf.Sin(initialAngle);

        gameObject.transform.position = new Vector3(x, y, CELESTIAL_BODY_Z);
        gameObject.name = RandomName.Generate();
    }

    public void Update(float deltaTime)
    {
        float angularVelocity = orbit.GetAngularVelocity(orbitalAngle);
        orbitalAngle += angularVelocity * deltaTime;
        float radius = orbit.OrbitalRadius(orbitalAngle);
        float x = orbit.Center.x + radius * Mathf.Cos(orbitalAngle);
        float y = orbit.Center.y + radius * Mathf.Sin(orbitalAngle);
        gameObject.transform.position = new(x, y, CELESTIAL_BODY_Z);
    }
}