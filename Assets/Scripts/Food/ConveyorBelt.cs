using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBelt : MonoBehaviour
{
    [SerializeField] private Transform startPoint;
    [SerializeField] private Transform endPoint;
    [SerializeField] private Recipe recipePrefab;

    private void Start()
    {
        StartCoroutine(SpawnRecipe());
    }

    private IEnumerator SpawnRecipe()
    {
        while (true)
        {
            Recipe recipe = Instantiate(recipePrefab, startPoint.position, Quaternion.identity);
            recipe.EndPoint = endPoint;
            yield return new WaitForSeconds(15.0f);
        }
    }
}
