using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Collections;
using Unity.Jobs;
using UnityEngine;

public class NpcMemory : MonoBehaviour
{
    [SerializeField]List<MemoryItem> _memoryItems = new List<MemoryItem>();
    public List<MemoryItem> MemoryItems { get { return _memoryItems; } }
    public int MaximumNumberOfItemsInMemory;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void UpdateTimers()
    {
        for (int i = 0; i < _memoryItems.Count; i++)
        {
            _memoryItems[i].ModifyMemory(Time.deltaTime);
            if (_memoryItems[i].TimeRemaining < 0 )
            {
                //removes memory item once timer is 0
                _memoryItems.Remove(_memoryItems[i]);
            }
        }
    }

    public void AddMemoryItem(GameObject item, float remainingTime)
    {
        if(!_memoryItems.Any(x => x.ObjectToRemember == item) && _memoryItems.Count < MaximumNumberOfItemsInMemory)
        {
            MemoryItem itemMem = new MemoryItem(item, remainingTime);
            _memoryItems.Add(itemMem);
        }
    }
    public void AddMemoryItem(MemoryItem item)
    {
        if(!MemoryItems.Any(x=>x.ObjectToRemember == item.ObjectToRemember)&&_memoryItems.Count < MaximumNumberOfItemsInMemory)
        {
            _memoryItems.Add(item);
        }
    }
}

[System.Serializable]
public class MemoryItem
{
    [SerializeField] GameObject objectToRemember;
    [SerializeField] float timeRemaining;

    public GameObject ObjectToRemember { get { return objectToRemember; } }
    public float TimeRemaining { get { return timeRemaining; } }

    public void ModifyMemory(float change)
    {
        timeRemaining -= change;
    }
    public MemoryItem(GameObject objectToRemember, float timeRemaining)
    {
        this.objectToRemember = objectToRemember;
        this.timeRemaining = timeRemaining;
    }
}
