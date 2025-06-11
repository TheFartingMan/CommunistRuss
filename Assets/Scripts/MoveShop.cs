using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class MoveShop : MonoBehaviour
{
    public RectTransform Shop;
    public Vector2 moveAmount = new Vector2(-1797.69f, 0f);
    [SerializeField] private float time;
    [SerializeField] private GameObject ButtonGameObject;
    private bool everyOther;

    public void MoveShopButtonClicked()
    {
        Button myButton = ButtonGameObject.GetComponent<Button>();
        everyOther = !everyOther;

        if (everyOther == true)
        {
            StartCoroutine(SmoothMove(Shop.anchoredPosition + moveAmount));
        }

        else
        {
            StartCoroutine(SmoothMove(Shop.anchoredPosition - moveAmount));
        }
        
    }

    private IEnumerator SmoothMove(Vector2 targetPos)
{
    Vector2 startPos = Shop.anchoredPosition;
    float elapsed = 0f;

    while (elapsed < time)
    {
        elapsed += Time.deltaTime;
        float t = elapsed / time;
        Shop.anchoredPosition = Vector2.Lerp(startPos, targetPos, t);
        yield return null;
    }

    Shop.anchoredPosition = targetPos;
}
}
