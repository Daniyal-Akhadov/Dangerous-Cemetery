using UnityEngine;

namespace CodeBase.Data
{
    public static class DataExtensions
    {
        public static Vector3Data AsVectorData(this Vector3 vector3) =>
            new Vector3Data(vector3.x, vector3.y, vector3.z);

        public static Vector3 AsUnityVector(this Vector3Data vector) =>
            new Vector3(vector.X, vector.Y, vector.Z);

        public static T ToDeserialized<T>(this string json)
        {
            return JsonUtility.FromJson<T>(json);
        }

        public static string ToJson<T>(this T value)
        {
            return JsonUtility.ToJson(value);
        }
    }
}