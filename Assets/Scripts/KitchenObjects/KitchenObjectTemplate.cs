using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "KitchenObjectTemplate", menuName = "Kitchen Objects/Create new KitchenObjectTemplate")]
public class KitchenObjectTemplate : ScriptableObject
{
    [SerializeField] private string objectName;
    [SerializeField] private GameObject prefab;
    [SerializeField] private Sprite sprite;

    public string ObjectName => objectName;
    public GameObject Prefab => prefab;
    public Sprite Sprite => sprite;
}
