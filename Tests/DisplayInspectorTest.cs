using UnityEngine;

using CommonMaxTools.Attributes;
using System.Collections.Generic;

public class DisplayInspectorTest : MonoBehaviour
{
    [Separator]
    [DisplayInspector(.5f, .5f, 1f)]
    public TestSettings settings;
    [Separator, DisplayInspector, AutoAssign]
    public SeparatorTest separatorTest;
}

[CreateAssetMenu]
public class TestSettings : ScriptableObject
{
    public int value;
    public string text;
    public Vector3 position;
    public string[] messages;
    [Separator]
    public Vector2[] directions;
    [Separator]
    public List<TestInfo> lists;

    [ButtonMethod]
    public void Test()
    {

    }
}

[System.Serializable]
public struct TestInfo
{
    public int value;
    public string name;
}
