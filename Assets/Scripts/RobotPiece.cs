using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public enum PieceType { Head,Arms,Body,Wheel };

public class RobotPiece : MonoBehaviour
{
    public GameObject prefab;
    public PieceType piece;
    public bool owned;

    public GameObject highlight;
}

