using UnityEngine;

namespace Application.Scripts.Library.Extensions.Vector
{
    public static class VectorExtension
    {
        public static Vector3 Rotate(this Vector3 direction, float degree)
        {
            var cos = Mathf.Cos(-degree * Mathf.Deg2Rad);
            var sin = Mathf.Sin(-degree * Mathf.Deg2Rad);
            var rotatedVector = new Vector3(
                direction.x * cos - direction.y * sin,
                direction.x * sin + direction.y * cos
            );

            return rotatedVector;
        }
        
        public static Vector2 Rotate(this Vector2 direction, float degree) => Rotate((Vector3)direction, degree);
    }
}