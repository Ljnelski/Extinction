using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AttackArea : MonoBehaviour
{
    HashSet<GameObject> objs = new();
    [SerializeField] List<string> tags;

    public abstract void Attack(GameObject obj);

    public void Run()
    {
        List<GameObject> toRemove = new();
        foreach (var item in objs)
        {
            if (!item)
            {
                toRemove.Add(item);
                continue;
            }
            Attack(item);
        }

        foreach (var item in toRemove)
        {
            objs.Remove(item);
        }
    }

    bool CheckTags(Collider other)
    {
        foreach (var tag in tags)
        {
            if (other.CompareTag(tag)) return true;
        }

        return false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!CheckTags(other)) return;

        objs.Add(other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!CheckTags(other)) return;

        objs.Remove(other.gameObject);
    }
}