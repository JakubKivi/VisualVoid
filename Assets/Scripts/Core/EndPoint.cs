using UnityEngine;

public class EndPoint : MonoBehaviour
{
    public System.Action OnPlayerReachedFinish;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Runner"))
        {
            OnPlayerReachedFinish?.Invoke();
        }
    }
}