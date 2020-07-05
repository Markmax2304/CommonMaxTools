using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using CommonMaxTools.Attributes;

public class ButtonMethodTest : MonoBehaviour
{
    public int value;

    [ButtonMethod]
    public void TestMethods()
    {
        Debug.Log("Invoked TestMethod");
    }

    [ButtonMethod]
    public static IEnumerator TestCoroutine()
    {
        Debug.Log("Invoked Coroutine");
        yield return null;
    }

    [ButtonMethod("Another name")]
    private string SomeStringMethod(string path = "default")
    {
        Debug.Log($"Output parameter path = {path}");
        return path;
    }

    [ButtonMethod]
    private static void WrongMethod(int value)
    {
    }
}
