using UnityEngine;

using CommonMaxTools.Attributes;

public class SeparatorTest : MonoBehaviour
{
    public int value1;
    [Separator]
    public int value2;
    [Separator("Strings")]
    public string text;
    [Separator("Positions")]
    public Vector3[] vectors;
}
