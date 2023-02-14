using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounterVisual : MonoBehaviour
{
    private const float PLATE_OFFSET_Y = .1f;

    [SerializeField] private PlatesCounter platesCounter;
    [SerializeField] private Transform counterTopPoint;
    [SerializeField] private GameObject plateVisualPrefab;

    private List<GameObject> plateVisualList;

    private void Awake()
    {
        plateVisualList = new List<GameObject>();
    }

    private void Start()
    {
        platesCounter.OnPlateSpawn += PlatesCounter_OnPlateSpawn;
        platesCounter.OnPlateRemoved += PlatesCounter_OnPlateRemoved;
    }

    private void PlatesCounter_OnPlateRemoved(object sender, System.EventArgs e)
    {
        GameObject plate = plateVisualList[^1];
        plateVisualList.Remove(plate);
        Destroy(plate);
    }

    private void PlatesCounter_OnPlateSpawn(object sender, System.EventArgs e)
    {
        GameObject plateVisual = Instantiate(plateVisualPrefab, counterTopPoint);
        plateVisual.transform.localPosition = new Vector3(0, PLATE_OFFSET_Y * plateVisualList.Count, 0);
        plateVisualList.Add(plateVisual);
    }
}
