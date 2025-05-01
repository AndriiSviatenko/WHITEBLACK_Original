using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] private int speed;

    public void Rotate(Vector3 value)
    {
        float angle = GetAngle(value);

        Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle));

        transform.rotation = 
            Quaternion.RotateTowards(transform.rotation, 
            targetRotation, 
            speed * Time.deltaTime);
    }

    private float GetAngle(Vector3 value) =>
        Mathf.Atan2(value.y - transform.position.y, 
            value.x - transform.position.x) * Mathf.Rad2Deg;
}
