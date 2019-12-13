using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{

    [SerializeField]
    private GameObject _objectToSpawn;

    private ARPlacement _placementIndicator;

    private void Start()
    {
        _placementIndicator = FindObjectOfType<ARPlacement>();
    }

    private void Update()
    {
        if (_placementIndicator.PlacementValidationState() && Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {

            Instantiate(_objectToSpawn, _placementIndicator.transform.position, _placementIndicator.transform.rotation);
        }
    }
}
