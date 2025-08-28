using System;
using System.Numerics;
using UnityEngine;

public class Button : MonoBehaviour
{
    [SerializeField] private int factor;

    public static event Action<int> AddRowPressed;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void AddRow()
    {
        AddRowPressed?.Invoke(factor);
    }
}