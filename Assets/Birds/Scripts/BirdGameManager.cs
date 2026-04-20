using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class BirdGameManager : MonoBehaviour
{
    private List<Target> targets;

    private void Start()
    {
        targets = FindObjectsByType<Target>(FindObjectsInactive.Exclude, FindObjectsSortMode.None).ToList();
        foreach (Target target in targets)
        {
            target.OnTargetDestoryed += OnTargetDestroyed;
        }
    }

    private void OnTargetDestroyed(Target target)
    {
        target.OnTargetDestoryed -= OnTargetDestroyed;
        targets.Remove(target);
        if (targets.Count == 0)
        {
            print("All targets destroyed");
        }
    }
}
