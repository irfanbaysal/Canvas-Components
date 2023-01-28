using UnityEngine;
using UnityEngine.UI;

namespace Core.Scripts.UI
{
    [RequireComponent(typeof(HorizontalLayoutGroup))]
    public class HorizontalLayoutComponent : LayoutComponent
    {
        protected void OnValidate()
        {
            if(ContentSizeFitter==null)return;
            SetHorizontalFit();
            gameObject.name = GetType().Name;
        }

        protected override void Awake()
        {
            base.Awake();
            LayoutGroup = GetComponent<VerticalLayoutGroup>();
        }
        
        private void SetHorizontalFit(ContentSizeFitter.FitMode fitMode = ContentSizeFitter.FitMode.PreferredSize)
        {
            ContentSizeFitter.horizontalFit = fitMode;
        }
    }
}