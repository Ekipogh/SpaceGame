using UnityEngine;

class Orbit
{
    float A;
    float B;

    public float Rotation{get;}

    float Eccentricity;

    public Orbit(float semi_major, float semi_minor, float rotation)
    {
        this.A = semi_major;
        this.B = semi_minor;
        this.Rotation = rotation;
        this.Eccentricity = Mathf.Sqrt(A*A - B*B);
    }


    public float OrbitalRadius(float angle)
    {
        float rotated_angle = angle  - Rotation;
        float radius = A * (1 - Eccentricity * Eccentricity) / (1 + Eccentricity * Mathf.Cos(rotated_angle));
        return radius;
    }

}