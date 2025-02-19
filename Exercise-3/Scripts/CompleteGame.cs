using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening; 

public class CompleteGame : MonoBehaviour
{
    public GameObject enemy;
    [SerializeField] private Vector3 dPos; // Μετατόπιση θέσης
    [SerializeField] private float duration = 1f; // Διάρκεια κίνησης
    private bool flag = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!enemy && flag == false)
        {
            Ladder();
        }
    }

    void Ladder()
    {
        transform.DOMove(transform.position + dPos, duration).SetEase(Ease.OutQuad);
        flag = true;
    }
}
