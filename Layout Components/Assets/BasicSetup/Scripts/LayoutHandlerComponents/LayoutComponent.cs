using System;
using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Core.Scripts.UI
{
    public enum BuilderType
    {
        MarkLayoutForRebuild,
        ForceRebuildLayoutImmediate
    }


    [RequireComponent(typeof(ContentSizeFitter), typeof(CanvasGroup))]
    [DisallowMultipleComponent]
    public class LayoutComponent : UIBehaviour
    {
        
        [SerializeField] private bool isEnableOnStart = true;
        [SerializeField] protected BuilderType builderType;
        [SerializeField] protected bool useForceCanvases = true;
        protected HorizontalOrVerticalLayoutGroup LayoutGroup;
        protected RectTransform RectTransform;
        protected CanvasGroup CanvasGroup;
        protected ContentSizeFitter ContentSizeFitter => GetComponent<ContentSizeFitter>();
        
        protected override void Awake()
        {
            base.Awake();
            LayoutGroup = GetComponent<HorizontalOrVerticalLayoutGroup>();
            RectTransform = GetComponent<RectTransform>();
            CanvasGroup = GetComponent<CanvasGroup>();
            EnableLayoutGroup(isEnableOnStart);
        }

        protected override void OnRectTransformDimensionsChange()
        {
            base.OnRectTransformDimensionsChange();
            ForceRebuilder();
        }

        protected void SetBuilderType(BuilderType type)
        {
            this.builderType = type;
        }

        protected async void ForceRebuilder(BuilderType type = BuilderType.MarkLayoutForRebuild)
        {
            if (type == BuilderType.MarkLayoutForRebuild)
            {
                await UniTask.Yield(cancellationToken: this.GetCancellationTokenOnDestroy());
                LayoutRebuilder.MarkLayoutForRebuild(RectTransform);
            }
            else
                LayoutRebuilder.ForceRebuildLayoutImmediate(RectTransform);

            ForceUpdateCanvas();
            
            Debug.Log("Dimension Changed");
        }

        protected void EnableLayoutGroup(bool isEnabled)
        {
            LayoutGroup.enabled = isEnabled;
        }

        protected void SetCanvasGroupAlpha(float? alpha = null)
        {
            CanvasGroup.alpha = alpha ?? 1f;
        }

        protected void SetCanvasGroupInteractable(bool isInteractable = true)
        {
            CanvasGroup.interactable = isInteractable;
        }

        protected void SetCanvasGroupBlockRaycasts(bool blocksRaycast = true)
        {
            CanvasGroup.blocksRaycasts = blocksRaycast;
        }

        private async void SetLastSibling(RectTransform rectTransform)
        {
            await UniTask.Yield(cancellationToken: this.GetCancellationTokenOnDestroy());
            LayoutRebuilder.MarkLayoutForRebuild(rectTransform);
            Canvas.ForceUpdateCanvases();
        }

        protected void ForceUpdateCanvas()
        {
            if(!useForceCanvases)return;
            Canvas.ForceUpdateCanvases();
        }

    }
}