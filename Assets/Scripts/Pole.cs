using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pole : MonoBehaviour
{
    private void Start()
    {
        GetComponent<MeshRenderer>().material.color += Color.gray;
    }
}
