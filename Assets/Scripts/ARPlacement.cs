using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class ARPlacement : MonoBehaviour
{

    private ARRaycastManager _raycastManager;
    private GameObject _placementVisual;
    private bool _placementValidation;
    private Pose _placementPose;

    private void Start()
    {
        _raycastManager = FindObjectOfType<ARRaycastManager>();

        _placementVisual = transform.GetChild(0).gameObject;
        _placementVisual.SetActive(false);

        _placementValidation = false;
    }

    private void Update()
    {
        UpdatePlacementIndicatorVisual();
        UpdatePlacementIndicatorPose();
    }

    private void UpdatePlacementIndicatorVisual()
    {
        if (_placementValidation)
        {
            if(!_placementVisual.activeInHierarchy)
                _placementVisual.SetActive(true);
        }
        else
        {
            _placementVisual.SetActive(false);
        }
    }

    private void UpdatePlacementIndicatorPose()
    {
        var screenCenter = Camera.current.ViewportToScreenPoint(new Vector3(0.5f, 0.5f, 0));
        var hits = new List<ARRaycastHit>();
        _raycastManager.Raycast(screenCenter, hits, TrackableType.Planes);

        _placementValidation = hits.Count > 0;

        if (_placementValidation)
        {
            var cameraForward = Camera.current.transform.forward;
            var cameraBearing = new Vector3(cameraForward.x, 0, cameraForward.z).normalized;

            _placementPose.position = hits[0].pose.position;
            _placementPose.rotation = Quaternion.LookRotation(cameraBearing);

            transform.SetPositionAndRotation(_placementPose.position, _placementPose.rotation);
        }
    }

    public bool PlacementValidationState()
    {
        return _placementValidation;
    }


}
