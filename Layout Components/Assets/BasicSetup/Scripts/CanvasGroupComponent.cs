using System;
using UnityEngine;
using UnityEngine.EventSystems;
namespace Core.Scripts.UI
{
    [Flags]
     public enum CanvasGroupFlags
     {
         Interactable = 1 << 1,
         BlockRaycast = 1 << 2,
         IgnoreParent = 1 << 3,
     }
    [RequireComponent(typeof(CanvasGroup))]
    [DisallowMultipleComponent]
    public class CanvasGroupComponent : UIBehaviour
    {
        [SerializeField] private CanvasGroupFlags properties;
        [SerializeField] private bool enableCanvasGroup=true;
        [SerializeField][RangeWithDecimal(0,1,1)] private float alpha;
        private bool Interactable => properties.HasFlag(CanvasGroupFlags.Interactable);
        private bool BlockRaycast => properties.HasFlag(CanvasGroupFlags.BlockRaycast);
        private bool IgnoreParent => properties.HasFlag(CanvasGroupFlags.IgnoreParent);
        public CanvasGroup CanvasGroup => _canvasGroup ? _canvasGroup : (_canvasGroup = GetComponent<CanvasGroup>());
        private CanvasGroup _canvasGroup;

        protected override void OnValidate()
        {
            base.OnValidate();
            gameObject.name = GetType().Name;
        }

        protected override void Awake()
        {
            base.Awake();
            SetAlpha();
            SetInteractable();
            SetBlockRaycasts();
            SetIgnoreParentGroup();
            EnableCanvasGroup();
        }
        
        public void EnableCanvasGroup()
        {
            CanvasGroup.enabled = enableCanvasGroup;
        }
        
        public void SetAlpha()
        {
            CanvasGroup.alpha = alpha;
        }

        public void SetInteractable()
        {
            CanvasGroup.interactable = Interactable;
        }
        
        public void SetIgnoreParentGroup()
        {
            CanvasGroup.ignoreParentGroups = IgnoreParent;
        }

        public void SetBlockRaycasts()
        {
            CanvasGroup.blocksRaycasts = BlockRaycast;
        }   
    }
}

