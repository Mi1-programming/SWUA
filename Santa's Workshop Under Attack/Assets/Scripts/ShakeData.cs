using UnityEngine;

// This is the shake data object used while applying shakes

[CreateAssetMenu(fileName = "New Shake", menuName = "Polishing/Shake")]
public class ShakeData : ScriptableObject
{
    public float magnitude; // The size of the shake
    public float duration; // The time the shake lasts
    public AnimationCurve curve; // The curve the size of the shake follows
}
