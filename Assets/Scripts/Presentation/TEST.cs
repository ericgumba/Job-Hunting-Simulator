using UnityEngine;


public class TEST : MonoBehaviour
{
    public EventLog log;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void myFunc()
    {
        log.AddMessage("Button clicked!");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
