using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    [Header("Day/Night Cycle Settings")]
    [Tooltip("Duration of a full day in seconds")]
    public float dayDurationInSeconds = 120f;

    [Tooltip("Starting time of day (0 = midnight, 0.5 = noon)")]
    [Range(0f, 1f)]
    public float startTimeOfDay = 0.25f;

    [Header("Optional: Sun Rotation")]
    [Tooltip("Axis around which the sun rotates")]
    public Vector3 rotationAxis = Vector3.right;

    private float currentTimeOfDay;

    void Start()
    {
        // Initialize the current time of day
        currentTimeOfDay = startTimeOfDay;
        UpdateSunRotation();
    }

    void Update()
    {
        // Calculate how much time has passed as a fraction of the day
        float dayProgress = Time.deltaTime / dayDurationInSeconds;

        // Update current time of day (wraps around after 1.0)
        currentTimeOfDay += dayProgress;
        if (currentTimeOfDay >= 1f)
        {
            currentTimeOfDay -= 1f;
        }

        UpdateSunRotation();
    }

    void UpdateSunRotation()
    {
        // Calculate rotation angle (0-360 degrees based on time of day)
        float rotationAngle = currentTimeOfDay * 360f;

        // Apply rotation to the light
        transform.rotation = Quaternion.AngleAxis(rotationAngle, rotationAxis);
    }
}