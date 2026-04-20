using UnityEngine;
using System;
using System.Collections;
using System.Linq;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class Delegates : MonoBehaviour
{
    public event Action testAction;
    public List<GameObject> objects;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        bool AllNotNull = objects.All(obj => obj != null);
        AllNotNull = true;
        foreach (GameObject obj in objects)
        {
            if (obj == null)
            {
                AllNotNull = false;
                break;
            }
        }

        //IEnumerable<GameObject> belowGround = objects.Where(obj => obj.transform.position.y < 0);
        //IEnumerable<GameObject> aboveGround = objects.Except(belowGround);
        //foreach (GameObject obj in belowGround)
        //{
        //    Destroy(obj);
        //}
        //objects = aboveGround.ToList();
        IEnumerable<string> names = objects.Select(obj => obj.name);

        Vector3 leftmostPosition = objects.Select(obj => obj.transform.position)
            .Where(pos => pos.x > 0)
            .OrderBy(pos => pos.x)
            .FirstOrDefault();

        print(leftmostPosition);

        GameObject closestToThis = objects
            .OrderBy(obj => Vector3.Distance(transform.position, obj.transform.position))
            .FirstOrDefault();

        Dictionary<string, GameObject> objectsByNames = objects.ToDictionary(obj => obj.name, obj => obj);



        //testAction = DebugText;
        //testAction += SomethingElse;
        //testAction += SomethingElse;
        //testAction?.Invoke();
        //testAction -= SomethingElse;
        //Action action = () =>
        //{
        //    Debug.Log("Akcja");
        //};
        //StartCoroutine(DoActionAfterAboveGround(() => Destroy(gameObject), 0));
    }

    public IEnumerator DoActionAfterAboveGround(Action action, float delay)
    {
        yield return new WaitWhile(IsAboveGround);
        action?.Invoke();
    }

    private IEnumerator MoveToPosition(Vector3 targetPosition, Action onArrive = null)
    {
        while (Vector3.Distance(transform.position, targetPosition) > 0)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime);
            yield return null;
        }
        onArrive?.Invoke();
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            testAction?.Invoke();
        }
    }

    private void DebugText()
    {
        Debug.Log("Coœ");
    }

    private void SomethingElse()
    {
        Debug.Log("Coœ innego");
    }

    private bool IsAboveGround()
    {
        return transform.position.y > 0;
    }

}

public delegate void MyDelegate(int num);
