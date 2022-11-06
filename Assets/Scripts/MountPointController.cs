using UnityEngine;
using UnityEngine.Assertions;

public class MountPointController : MonoBehaviour
{
    private int _currentChild = -1;
    private int _totalChildren = 0;

    public Transform Environment;

    private void Start()
    {
        _totalChildren = transform.childCount;
        Assert.IsTrue(_totalChildren > 0);

        _currentChild = 0;
        MountByIndex(_currentChild);
    }

    public void MountByIndex(int childIndex)
    {
        var target = transform.GetChild(childIndex);
        Environment.SetParent(target, false);  
    }

    //private void Update()
    //{
    //    if (Input.GetKeyUp(KeyCode.M))
    //    {
    //        _currentChild = (_currentChild + 1) % _totalChildren;
    //        MountByIndex(_currentChild);
    //    }
    //}
}
