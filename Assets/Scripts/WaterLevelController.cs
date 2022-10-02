using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterLevelController : MonoBehaviour
{
    [SerializeField][Range(0.0f, 1.0f)] private float _minLevel;
    [SerializeField][Range(0.0f, 1.0f)] private float _maxLevel;
    [SerializeField][Range(0.0f, 1.0f)] private float _step;

    private Transform GetChildRoot => transform.GetChild(0).gameObject.transform;

    public void Init()
    {
        GetChildRoot.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
    }

    public void SetLevel(bool up)
    {
        Debug.Log("Setting Water Level " + (up ? "UP" : "DOWN"));

        var currPos = GetChildRoot.position;
        var newPos = Mathf.Clamp(currPos.y + (up ? _step : -_step), _minLevel, _maxLevel);
        currPos.y = newPos;
        GetChildRoot.position = currPos;

        Debug.Log("Current Water Level " + GetChildRoot.position.y);
    }
}
