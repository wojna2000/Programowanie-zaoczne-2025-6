using UnityEngine;

public class DelegateSubscriber : MonoBehaviour
{
    public Delegates delegates;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void OnEnable()
    {
        delegates.testAction += Test;
    }

    private void OnDisable()
    {
        delegates.testAction -= Test;
    }

    public void Test()
    {
        print("Test" + gameObject.name);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
