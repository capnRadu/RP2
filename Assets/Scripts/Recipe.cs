using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Recipe : MonoBehaviour
{
    private float moveSpeed = 2f;
    private Transform endPoint;
    public Transform EndPoint
    {
        get
        {
            return endPoint;
        }

        set
        {
            endPoint = value;
        }
    }

    [System.Serializable]
    public class FoodItem
    {
        public string name;
        public Sprite sprite;
    }

    public FoodItem[] appetizers;
    public FoodItem[] mainCourses;
    public FoodItem[] sideDishes;
    public FoodItem[] desserts;
    public FoodItem[] drinks;

    private Dictionary<string, FoodItem[]> foodCategories;

    private List<string> categoryOrder = new List<string> { "Appetizer", "Main Course", "Side Dish", "Dessert", "Drink" };

    [SerializeField] private Transform orderCanvasBackground;

    private void Start()
    {
        foodCategories = new Dictionary<string, FoodItem[]>
        {
            { "Appetizer", appetizers },
            { "Main Course", mainCourses },
            { "Side Dish", sideDishes },
            { "Dessert", desserts },
            { "Drink", drinks }
        };

        int orderDifficulty = Random.Range(1, 6);
        GenerateOrder(orderDifficulty);
    }

    private void Update()
    {
        if (EndPoint != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, EndPoint.position, moveSpeed * Time.deltaTime);
        }

        if (transform.position == EndPoint.position)
        {
            Destroy(gameObject);
        }
    }

    private void GenerateOrder(int difficultyLevel)
    {
        List<string> selectedCategories = new List<string>();

        while (selectedCategories.Count < difficultyLevel)
        {
            string randomCategory = categoryOrder[Random.Range(0, categoryOrder.Count)];

            if (!selectedCategories.Contains(randomCategory))
            {
                selectedCategories.Add(randomCategory);
            }
        }

        selectedCategories.Sort((x, y) => categoryOrder.IndexOf(x).CompareTo(categoryOrder.IndexOf(y)));

        foreach (string category in selectedCategories)
        {
            FoodItem[] items = foodCategories[category];
            FoodItem randomItem = items[Random.Range(0, items.Length)];
            Debug.Log(randomItem.name);

            GameObject orderItem = new GameObject(randomItem.name);
            orderItem.transform.SetParent(orderCanvasBackground);
            orderItem.transform.localPosition = new Vector3(0, 0, 0);
            orderItem.transform.localScale = new Vector3(1, 1, 1);
            orderItem.AddComponent<Image>().sprite = randomItem.sprite;
            orderItem.GetComponent<RectTransform>().sizeDelta = new Vector2(35, 35);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
        {
            Destroy(gameObject);
        }
    }
}