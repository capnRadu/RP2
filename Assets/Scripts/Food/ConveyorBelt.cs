using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBelt : MonoBehaviour
{
    [SerializeField] private Transform startPoint;
    [SerializeField] private Transform endPoint;
    [SerializeField] private Recipe recipePrefab;

    [NonSerialized] public float spawnCooldown = 15.0f;
    [NonSerialized] public float recipeMoveSpeed = 1f;

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
            recipe.conveyorBeltScript = this;
            recipe.moveSpeed = recipeMoveSpeed;
            yield return new WaitForSeconds(spawnCooldown);
        }
    }

    public void IncreaseDifficulty()
    {
        spawnCooldown -= 0.5f;
        recipeMoveSpeed += 0.1f;

        var recipes = FindObjectsOfType<Recipe>();
        foreach (var recipe in recipes)
        {
            recipe.moveSpeed = recipeMoveSpeed;
        }
    }
}
