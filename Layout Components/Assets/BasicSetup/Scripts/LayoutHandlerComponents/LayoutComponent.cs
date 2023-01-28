using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Core.Scripts.UI
{
    public enum BuilderType
    {
        MarkLayoutForRebuild,
        ForceRebuildLayoutImmediate
    }

    [RequireComponent(typeof(ContentSizeFitter),typeof(CanvasGroup))]
    [DisallowMultipleComponent]
    public class LayoutComponent : UIBehaviour
    {
        [SerializeField] protected BuilderType builderType;
        protected LayoutGroup LayoutGroup; 
        protected RectTransform RectTransform;
        protected CanvasGroup CanvasGroup; 
        protected ContentSizeFitter ContentSizeFitter => GetComponent<ContentSizeFitter>();
       
        protected override void Awake()
        {
            base.Awake();
            LayoutGroup = GetComponent<LayoutGroup>();
            RectTransform = GetComponent<RectTransform>();
            CanvasGroup = GetComponent<CanvasGroup>();
        }
 
        protected void SetBuilderType(BuilderType type)
        {
            this.builderType = type;
        }

        protected async void ForceRebuilder(BuilderType type = BuilderType.MarkLayoutForRebuild)
        {
            if (type == BuilderType.MarkLayoutForRebuild)
            {
                await UniTask.Yield(cancellationToken:this.GetCancellationTokenOnDestroy());
                LayoutRebuilder.MarkLayoutForRebuild(RectTransform);
            }
            else
                LayoutRebuilder.ForceRebuildLayoutImmediate(RectTransform);
            
            ForceUpdateCanvas();
        }
        protected void EnableLayoutGroup(bool isEnabled)
        {
            LayoutGroup.enabled = isEnabled;
        }

        protected void SetCanvasGroupAlpha(float? alpha=null)
        {
            CanvasGroup.alpha = alpha ?? 1f;
        }

        protected void SetCanvasGroupInteractable(bool isInteractable=true)
        {
            CanvasGroup.interactable = isInteractable;
        }
        
        protected void SetCanvasGroupBlockRaycasts(bool blocksRaycast=true)
        {
            CanvasGroup.blocksRaycasts = blocksRaycast;
        }
        
        private async void SetLastSibling(RectTransform rectTransform)
        {
            await UniTask.Yield(cancellationToken:this.GetCancellationTokenOnDestroy());
            LayoutRebuilder.MarkLayoutForRebuild(rectTransform); 
            Canvas.ForceUpdateCanvases();
        }
        protected void ForceUpdateCanvas()
        {
            Canvas.ForceUpdateCanvases();   
        }
        
    }
}