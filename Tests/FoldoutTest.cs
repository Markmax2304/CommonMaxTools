using UnityEngine;

using CommonMaxTools.Attributes;

public class FoldoutTest : MonoBehaviour
{
    [Foldout("First Group")]
    public int value;
    [Foldout("First Group")]
    public float speed;
    public string playerName;
    [Foldout("Second everything group", true)]
    public string text;
    public string description;
    public int[] indexes;
    [Foldout("Third Group")]
    public Vector3 position;
    [Foldout("First Group")]
    public Vector2 size;
    [Foldout("Nested inspector"), DisplayInspector]
    public DisplayInspectorTest test;
}
