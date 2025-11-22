using UnityEngine;

public class RandomColor : MonoBehaviour
{
    void Start()
    {
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        var random_red = UnityEngine.Random.value;
        var random_green = UnityEngine.Random.value;
        var random_blue = UnityEngine.Random.value;
        renderer.color = new Color(random_red, random_green, random_blue);
    }
}
