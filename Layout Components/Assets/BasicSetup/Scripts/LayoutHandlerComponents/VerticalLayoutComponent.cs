using UnityEngine;
using UnityEngine.UI;

namespace Core.Scripts.UI
{
    [RequireComponent(typeof(VerticalLayoutGroup))]
    public class VerticalLayoutComponent : LayoutComponent
    {
        protected void OnValidate()
        {
            if(ContentSizeFitter==null)return;
            SetVerticalFit();
            gameObject.name = GetType().Name;
        }

        protected override void Awake()
        {
            base.Awake();
            LayoutGroup = GetComponent<VerticalLayoutGroup>();
        }

        private void SetVerticalFit(ContentSizeFitter.FitMode fitMode = ContentSizeFitter.FitMode.PreferredSize)
        {
            ContentSizeFitter.verticalFit = fitMode;
        }
    }
    
}
