using System.Collections;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    /// <summary>
    /// Makes the main camera shake according to the following variables
    /// </summary>
    /// <param name="magnitude">The size of the shake</param>
    /// <param name="duration">The time the shake will last</param>
    /// <param name="curve">The easing the shake follows</param>
    private IEnumerator ShakeEnumerator(float magnitude, float duration, AnimationCurve curve)
    {
        // Initialize variables
        Camera main = Camera.main;
        Vector3 traveled = Vector3.zero;
        float waited = 0.0f;

        while (waited < duration) // Loop until waited for duration
        {
            waited += Time.deltaTime; // Progress time

            main.transform.position -= traveled; // Removes the movement applied the last frame
            traveled = Random.insideUnitSphere * magnitude * (1.0f - curve.Evaluate(waited / duration)); // Generates a random position offset
            main.transform.position += traveled; // Applies movement

            yield return 0; // Wait a frame
        }
        main.transform.position -= traveled; // Removes the extra left over frame of traveled distance
    }
    /// <summary>
    /// A version of the shake method which can be easily triggered with events in the Unity Editor
    /// </summary>
    /// <param name="shake">Input shake scriptable object</param>
    public void Shake(ShakeData shake)
    {
        StartCoroutine(ShakeEnumerator(shake.magnitude, shake.duration, shake.curve)); // Calls the original function
    }
}
