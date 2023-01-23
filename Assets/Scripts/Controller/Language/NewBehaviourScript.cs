using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public Frame[] frames;
}
[System.Serializable]
public struct Frame
{
    public Sprite sprite;
    public float duration;
}