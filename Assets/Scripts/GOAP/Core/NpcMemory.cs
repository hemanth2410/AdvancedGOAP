using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NpcMemory : MonoBehaviour
{
    List<MemoryItem> _memoryItems = new List<MemoryItem>();
    public List<MemoryItem> MemoryItems { get { return _memoryItems; } } 
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
            _memoryItems[i].TimeRemaining -= Time.deltaTime;
            if (_memoryItems[i].TimeRemaining < 0 )
            {
                _memoryItems.Remove(_memoryItems[i]);
            }
        }
    }

    public void AddMemeryItem(GameObject item, float remainingTime)
    {
        if(_memoryItems.Any(x => x.ObjectToRemember == item))
        {
            MemoryItem itemMem = new MemoryItem(item, remainingTime);
            _memoryItems.Add(itemMem);
        }
    }
}

[System.Serializable]
public class MemoryItem
{
    GameObject objectToRemember;
    float timeRemaining;

    public GameObject ObjectToRemember { get { return objectToRemember; } }
    public float TimeRemaining;
    public MemoryItem(GameObject objectToRemember, float timeRemaining)
    {
        this.objectToRemember = objectToRemember;
        this.timeRemaining = timeRemaining;
    }
}
