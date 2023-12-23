using UnityEngine;

public abstract class InterfaceOption : ScriptableObject
{
    public Object instance;
    public abstract void Create(Transform parent);
}
