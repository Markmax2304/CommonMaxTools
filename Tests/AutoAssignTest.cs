using UnityEngine;

using CommonMaxTools.Attributes;


public class AutoAssignTest : MonoBehaviour
{
    [AutoAssign]
    public Transform ownTransform;
    [AutoAssign]
    public int intVariable;
    [AutoAssign]
    public Collider[] colliders;
    [AutoAssign]
    public SpriteRenderer spriteRenderer;
}
