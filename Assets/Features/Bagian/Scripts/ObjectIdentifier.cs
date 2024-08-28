using UnityEngine;

public enum ObjectType
{
    target,
    selectedObject
}

public class ObjectIdentifier : MonoBehaviour
{
    [HideInInspector] public string objectName;
    public ObjectType objectType;
    public bool isMatched = false;

    private void Awake()
    {
        objectName = gameObject.name;
    }
}