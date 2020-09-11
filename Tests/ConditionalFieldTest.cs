using UnityEngine;

using CommonMaxTools.Attributes;

public class ConditionalFieldTest : MonoBehaviour
{
    public bool enable = true;
    public string hello = "hello";
    public Vector3 direction = Vector3.one;
    public Transform ownTransform;

    [Separator("Contidionals")]
    [ConditionalField("enable", false, true)]
    public string test;
    [ConditionalField("enable", true, true)]
    public string inverseTest;

    [ConditionalField("hello", false, "hello", "world")]
    public string multiStringTest;
    [ConditionalField("hello", false)]
    public string emptyStringTest;

    [ConditionalField("direction", false)]
    public string vectorTest;

    [ConditionalField("ownTransform", false)]
    public TestInfo structTest;
}
