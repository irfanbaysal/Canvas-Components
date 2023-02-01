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


    [RequireComponent(typeof(ContentSizeFitter))]
    [DisallowMultipleComponent]
    public class LayoutComponent : UIBehaviour
    {
        
        [SerializeField] private bool isEnableOnStart = true;
        [SerializeField] protected BuilderType builderType;
        [SerializeField] protected bool useForceCanvases = true;
        protected HorizontalOrVerticalLayoutGroup LayoutGroup;
        protected RectTransform RectTransform;
        protected ContentSizeFitter ContentSizeFitter => GetComponent<ContentSizeFitter>();
        
        protected override void Awake()
        {
            base.Awake();
            LayoutGroup = GetComponent<HorizontalOrVerticalLayoutGroup>();
            RectTransform = GetComponent<RectTransform>();
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
        }

        protected void EnableLayoutGroup(bool isEnabled)
        {
            LayoutGroup.enabled = isEnabled;
        }
        
        protected void ForceUpdateCanvas()
        {
            if(!useForceCanvases)return;
            Canvas.ForceUpdateCanvases();
        }
    }
}