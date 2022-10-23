using UnityEngine;
using UnityEngine.Assertions;

public class EnvironmentToggleController : MonoBehaviour
{
    private int _childCount = 0;
    private int _currChild = 0;

    private void Start()
    {
        _childCount = transform.childCount;
        Assert.IsTrue(_childCount > 0);

        transform.GetChild(_currChild).gameObject.SetActive(true);
    }

    private void ToggleChild()
    {
        transform.GetChild(_currChild).gameObject.SetActive(false);
        _currChild = (_currChild + 1) % _childCount;
        transform.GetChild(_currChild).gameObject.SetActive(true);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            ToggleChild();
        }
    }
}
