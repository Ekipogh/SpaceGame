class RandomName
{
    public static string Generate()
    {
        string[] prefixes = { "Zor", "Xan", "Vel", "Kry", "Lun", "Sol", "Neb", "Gal", "Ast", "Cos" };
        string[] suffixes = { "on", "ar", "is", "us", "ea", "ix", "or", "um", "ax", "es" };

        string prefix = prefixes[UnityEngine.Random.Range(0, prefixes.Length)];
        string suffix = suffixes[UnityEngine.Random.Range(0, suffixes.Length)];

        return prefix + suffix;
    }
}