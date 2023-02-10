using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "KitchenObjectTemplete", menuName = "Kitchen Objects/Create new KitchenObjectTemplete")]
public class KitchenObjectTemplete : ScriptableObject
{
    [SerializeField] private string objectName;
    [SerializeField] private GameObject prefab;
    [SerializeField] private Sprite sprite;

    public string ObjectName => objectName;
    public GameObject Prefab => prefab;
    public Sprite Sprite => sprite;
}
