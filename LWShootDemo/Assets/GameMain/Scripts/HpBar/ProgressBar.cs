using System;
using UnityEngine;
using System.Collections;
using Sirenix.OdinInspector;

public class ProgressBar : MonoBehaviour
{
    public RectTransform font;
    public RectTransform increaseDelayImage;
    public RectTransform decreaseDelayImage;

    [SerializeField] private float delaySpeed = 0.5f;

    // 要运动到的目标值 也就是当前的进度
    private float targetScale;
    private Coroutine updateProgressBarCoroutine;

    private float currentValue;

    [Button]
    public void UpdateProgressImmeadiatly(float newValue, float maxValue)
    {
        newValue = Mathf.Clamp(newValue, 0f, maxValue);
        targetScale = newValue / maxValue;
        
        // 隐藏增加和减少的图
        increaseDelayImage.gameObject.SetActive(false);
        decreaseDelayImage.gameObject.SetActive(false);
        
        // 进度条直接到达目标值
        font.localScale = new Vector3(newValue / maxValue, 1f, 1f);
    }

    [Button]
    public void UpdateProgress(float newValue, float maxValue)
    {
        newValue = Mathf.Clamp(newValue, 0f, maxValue);
        targetScale = newValue / maxValue;

        // 如果现在正在更新进度条，那么停止协程
        if(updateProgressBarCoroutine != null)
        {
            StopCoroutine(updateProgressBarCoroutine);
        }

        // 开始更新进度条
        updateProgressBarCoroutine = StartCoroutine(UpdateProgressBarCoroutine());
    }

    private IEnumerator UpdateProgressBarCoroutine()
    {
        float initialScale = font.localScale.x;

        // 增长
        if (targetScale > initialScale)
        {
            // 显示增长图,隐藏减少图
            decreaseDelayImage.gameObject.SetActive(false);
            increaseDelayImage.gameObject.SetActive(true);
            // 增长图直接到达目标值
            increaseDelayImage.localScale = new Vector3(targetScale, 1f, 1f);
            // 进度值慢慢增长到目标值
            while (Math.Abs(font.localScale.x - targetScale) > 0.01f)
            {
                float scale = Mathf.MoveTowards(font.localScale.x, targetScale, delaySpeed * Time.deltaTime);
                font.localScale = new Vector3(scale, 1f, 1f);
                yield return null;
            }
        }
        // 减少
        else
        {
            // 显示减少图,隐藏增长图
            increaseDelayImage.gameObject.SetActive(false);
            decreaseDelayImage.gameObject.SetActive(true);
            
            // 减少值直接到达当前值
            decreaseDelayImage.localScale = new Vector3(initialScale, 1f, 1f);
            // 进度值直接到达目标值
            font.localScale = new Vector3(targetScale, 1f, 1f);
            // 减少值慢慢减少到目标值
            while (Math.Abs(decreaseDelayImage.localScale.x - targetScale) > 0.01f)
            {
                float scale = Mathf.MoveTowards(decreaseDelayImage.localScale.x, targetScale, delaySpeed * Time.deltaTime);
                decreaseDelayImage.localScale = new Vector3(scale, 1f, 1f);
                yield return null;
            }
        }

        increaseDelayImage.gameObject.SetActive(false);
        decreaseDelayImage.gameObject.SetActive(false);
    }
}
