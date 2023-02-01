using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Core.Scripts.UI
{
    
    [Flags]
    public enum TextMeshFlags
    {
        RaycastTarget = 1 << 1,
        AutoSize = 1 << 2,
        Wrapping = 1 << 3,
    }

    [RequireComponent(typeof(ContentSizeFitter))]
    [DisallowMultipleComponent]
    public class TextMeshUIComponent : UIBehaviour
    {
        [SerializeField] private TextMeshFlags flags;
        [SerializeField] private Vector4 margin = Vector4.zero;
        [SerializeField] private float fontSize;
        [SerializeField] private bool isEnableOnStart = true;
        private bool RaycastTarget => flags.HasFlag(TextMeshFlags.RaycastTarget);
        private bool AutoSize => flags.HasFlag(TextMeshFlags.AutoSize);
        private bool Wrapping => flags.HasFlag(TextMeshFlags.Wrapping);

        protected TextMeshProUGUI TextMeshProUGUI =>
            _textMeshProUGUI ? _textMeshProUGUI : (_textMeshProUGUI = GetComponent<TextMeshProUGUI>());
        protected ContentSizeFitter ContentSizeFitter => GetComponent<ContentSizeFitter>();
        private TextMeshProUGUI _textMeshProUGUI;

        protected override void Awake()
        {
            base.Awake();
           Enable(isEnableOnStart);
           SetDefaults();
        }

        protected override void OnRectTransformDimensionsChange()
        {
            base.OnRectTransformDimensionsChange();
            SetDefaults();
        }

        private void OnValidate()
        {
             SetDefaults();
        }

        protected void Enable(bool isEnable)
        {
            this.gameObject.SetActive(isEnable);
        }

        protected void SetFontExtras()
        {
            TextMeshProUGUI.margin = margin;
            TextMeshProUGUI.raycastTarget = RaycastTarget;
            TextMeshProUGUI.autoSizeTextContainer = AutoSize;
            TextMeshProUGUI.enableWordWrapping = Wrapping;
        }

        protected void SetFontSize()
        {
            TextMeshProUGUI.fontSize = fontSize;
        }

        private void SetDefaults()
        {
            SetFontExtras();
            SetFontSize();
        }
    }
    
}
