using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereConfig : MonoBehaviour
{
    private Renderer _objectRenderer;

    private Vector3 _objectDefaultScale;
    private Vector3 _objectDefaultPosition;
    private float _minScale;
    private float _maxScale;

    [SerializeField]
    private float _maxY;
    [SerializeField]
    private float _maxX;

    [SerializeField]
    private float _maxDistance;

    private void Start()
    {

        _objectRenderer = GetComponent<Renderer>();

        _objectDefaultScale = transform.localScale;
        _objectDefaultPosition = transform.position;
        _minScale = 1;
        _maxScale = 2.5f;

        ChangeProperties();
    }

    private void Update()
    {
        if (_maxY != 0 || _maxX != 0)
            ChangeYbyGettingCloser();
    }

    private void ChangeYbyGettingCloser()
    {

        float distance = UpdateDistance();

        float min = 0;
        float maxDistance = _maxDistance;

        float maxY = _maxY;
        float maxX = _maxX;

        float multiY = min;
        float multiX = min;

        if (distance <= min)
        {
            multiY = maxY;
            multiX = maxX;
        }
        else if (distance >= maxDistance)
        {

            multiY = min;
            multiX = min;
        }
        else
        {
            var newY = (maxY * distance) / maxDistance;
            var newX = (maxX * distance) / maxDistance;

            multiY = maxY - newY;
            multiX = maxX - newX;
        }

        transform.position = new Vector3(_objectDefaultPosition.x + multiX, _objectDefaultPosition.y + multiY, _objectDefaultPosition.z);
    }

    private float UpdateDistance()
    {
        var cameraPosition = Camera.current.transform.position;
        var objectPosition = transform.position;

        var distance = Mathf.Sqrt(Mathf.Pow(cameraPosition.x - objectPosition.x, 2) + Mathf.Pow(cameraPosition.y - objectPosition.y, 2) + Mathf.Pow(cameraPosition.z - objectPosition.z, 2));

        return distance;
    }

    private void ChangeProperties()
    {
        //Scale
        //var randomScaleMult = UnityEngine.Random.Range(_minScale, _maxScale);
        //transform.localScale = new Vector3(_objectDefaultScale.x * randomScaleMult, _objectDefaultScale.y * randomScaleMult, _objectDefaultScale.z * randomScaleMult);

        _objectRenderer.material.SetColor("_Color", getRandomColor());
    }

    private Color getRandomColor()
    {
        int r = UnityEngine.Random.Range(1, 5);
        Color color;
        switch (r)
        {
            case 1:
                color = Color.red;
                break;
            case 2:
                color = Color.green;
                break;
            case 3:
                color = Color.blue;
                break;
            case 4:
                color = Color.yellow;
                break;
            case 5:
                color = Color.white;
                break;
            default:
                color = Color.black;
                break;
        }

        return color;
    }
}
