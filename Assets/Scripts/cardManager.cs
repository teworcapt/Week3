using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class cardManager : MonoBehaviour
{
    private Vector3 originalScale;
    public Material front;
    public Material back;
    private bool isShowingFront = true;
    private Renderer cardRenderer;

    private bool hasFlipped = false;

    void Start()
    {
        originalScale = transform.localScale;
        cardRenderer = GetComponent<Renderer>();

        cardRenderer.material = front;

        ShakeCard();
    }

    void ShakeCard()
    {
        transform.DOShakePosition(5f, strength: 0.5f, vibrato: (int)1.5f, randomness: 100, snapping: false, fadeOut: false)
                .SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine);
    }

    void OnMouseEnter()
    {
        transform.DOScale(originalScale * 1.1f, 0.2f)
                .SetEase(Ease.OutQuad);
    }

    void OnMouseExit()
    {
        transform.DOScale(originalScale, 0.2f)
                .SetEase(Ease.InQuad);
    }

    void OnMouseDown()
    {
        // Check if the card has already been flipped
        if (!hasFlipped)
        {
            hasFlipped = true;

            transform.DORotate(new Vector3(0, 0, 180f), 0.5f, RotateMode.WorldAxisAdd)
                    .SetEase(Ease.OutQuad)
                    .OnUpdate(() => {

                        if (transform.eulerAngles.z >= 90f && transform.eulerAngles.z < 270f)
                        {
                            if (isShowingFront)
                            {
                                StartCoroutine(RotateMaterialTexture(back));
                                cardRenderer.material = back;
                            }
                        }
                    })
                    .OnComplete(() => {
                        isShowingFront = false;
                    });
        }
    }

    private IEnumerator RotateMaterialTexture(Material material)
    {
        cardRenderer.material = material;

        float rotationSpeed = 10f;
        float duration = 0.5f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            float offset = (elapsed * rotationSpeed) % 1f;
            cardRenderer.material.SetTextureOffset("_MainTex", new Vector2(offset, 0));
            elapsed += Time.deltaTime;
            yield return null;
        }

        cardRenderer.material.SetTextureOffset("_MainTex", Vector2.zero);
    }
}
