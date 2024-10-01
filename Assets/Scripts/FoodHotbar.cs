using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FoodHotbar : MonoBehaviour
{
    public ShootProjectile shootProjectile;
    public GameObject[] foodProjectiles;

    public RectTransform verticalContent;
    public List<GameObject> foodCategories;

    private int currentCategoryIndex = 0;
    private int currentFoodIndex = 0; 

    public Color highlightColor = Color.yellow;
    public Color defaultColor = Color.white;
    public float selectedScale = 1.5f;
    public float unselectedScale = 1.0f;

    void Start()
    {
        UpdateMenuUI();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            NavigateCategory(-1);
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            NavigateCategory(1);
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            NavigateFood(-1);
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            NavigateFood(1);
        }
    }

    void NavigateCategory(int direction)
    {
        int categoryCount = foodCategories.Count;

        currentCategoryIndex = (currentCategoryIndex + direction + categoryCount) % categoryCount;

        currentFoodIndex = Mathf.Clamp(currentFoodIndex, 0, foodCategories[currentCategoryIndex].transform.childCount - 1);

        StartCoroutine(ScrollToCategory(currentCategoryIndex));

        UpdateMenuUI();
    }

    void NavigateFood(int direction)
    {
        Transform foodContainer = foodCategories[currentCategoryIndex].transform;
        int foodCount = foodContainer.childCount;

        currentFoodIndex = (currentFoodIndex + direction + foodCount) % foodCount;

        UpdateMenuUI();
    }

    private IEnumerator ScrollToCategory(int categoryIndex)
    {
        float targetY = categoryIndex * 105;
        float currentY = verticalContent.anchoredPosition.y;

        while (currentY != targetY)
        {
            currentY = Mathf.MoveTowards(currentY, targetY, 5000 * Time.deltaTime);
            verticalContent.anchoredPosition = new Vector2(0, currentY);
            yield return null;
        }
    }

    void UpdateMenuUI()
    {
        for (int i = 0; i < foodCategories.Count; i++)
        {
            Transform foodContainer = foodCategories[i].transform;

            for (int j = 0; j < foodContainer.childCount; j++)
            {
                RectTransform foodItem = foodContainer.GetChild(j).GetComponent<RectTransform>();
                Image foodImage = foodItem.GetComponent<Image>();

                if (i == currentCategoryIndex && j == currentFoodIndex)
                {
                    foodImage.color = highlightColor;
                    foodItem.localScale = Vector3.one * selectedScale;

                    foreach (GameObject foodProjectile in foodProjectiles)
                    {
                        if (foodProjectile.name == foodItem.name)
                        {
                            shootProjectile.point = foodProjectile;
                            shootProjectile.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = foodProjectile.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite;
                            shootProjectile.transform.GetChild(0).localScale = foodProjectile.transform.GetChild(0).localScale;
                        }
                    }
                }
                else
                {
                    foodImage.color = defaultColor;
                    foodItem.localScale = Vector3.one * unselectedScale;
                }
            }
        }
    }
}
