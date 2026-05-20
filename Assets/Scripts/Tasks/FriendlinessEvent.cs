using System;
using System.Linq;
using UnityEngine;

public class FriendlinessEvent : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ApplyFriendliness(Character trustee, double value, float detectionRadius = 5f)
    {
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, detectionRadius);

        foreach (Collider2D col in hitColliders)
        {
            NPC truster = col.GetComponent<NPC>();
            if (truster != null && truster != trustee)
                truster.IncreaseFriendliness(trustee, value);
        }
        Destroy(gameObject);
    }
}
