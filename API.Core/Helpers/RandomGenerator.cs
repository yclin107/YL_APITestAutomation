using System.Security.Cryptography;

namespace API.Core.Helpers;

public class RandomGenerator
{
    public static T GetRandomFromList<T>(List<T> list)
    {
        return list[RandomNumberGenerator.GetInt32(0, list.Count)];
    }
}
