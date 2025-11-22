using UnityEngine;

class Orbit
{
    private readonly float semiMajorAxis;
    private readonly float semiMinorAxis;
    private readonly float eccentricity;

    public float Rotation { get; }

    private readonly Transform center;

    public Vector3 Center { get => center.position; }

    public float Velocity { get; set; } = 1.0f;

    public Orbit(Transform center, float semiMajor, float semiMinor, float rotation, float velocity)
    {
        this.semiMajorAxis = semiMajor;
        this.semiMinorAxis = semiMinor;
        this.Rotation = rotation;
        float c = Mathf.Sqrt(semiMajor * semiMajor - semiMinor * semiMinor);
        this.eccentricity = c / semiMajor;
        this.Velocity = velocity;
        this.center = center;
    }


    public float OrbitalRadius(float angle)
    {
        float rotatedAngle = angle - Rotation;
        float eccSquared = eccentricity * eccentricity;
        return semiMajorAxis * (1 - eccSquared) / (1 + eccentricity * Mathf.Cos(rotatedAngle));
    }

    public float GetAngularVelocity(float angle)
    {
        float r = OrbitalRadius(angle);
        float r0 = semiMinorAxis; // Reference radius
        return Velocity * (r0 * r0) / (r * r);
    }
}