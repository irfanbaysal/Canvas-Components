using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Core.Scripts.UI
{
    [DisallowMultipleComponent]
    [RequireComponent(typeof(CanvasScaler),typeof(CanvasGroupComponent))]
    public class CanvasComponent : UIBehaviour
    {
        [SerializeField] private bool enableCanvas=true;
        public CanvasScaler CanvasScaler => _canvasScaler ? _canvasScaler : (_canvasScaler = GetComponent<CanvasScaler>());
        private CanvasScaler _canvasScaler;
        public CanvasGroupComponent CanvasGroupComponent => _canvasGroupComponent
            ? _canvasGroupComponent
            : (_canvasGroupComponent = GetComponent<CanvasGroupComponent>());
        private CanvasGroupComponent _canvasGroupComponent;

        private void OnValidate()
        {
            SetDynamicFitUIMode();
            gameObject.name = GetType().Name;
        }

        protected override void Awake()
        {
            base.Awake();
            EnableCanvas();
        }

        private void EnableCanvas()
        {
            this.gameObject.SetActive(enableCanvas);
        }

        private void SetDynamicFitUIMode()
        { 
            if(CanvasScaler ==null) return;
            CanvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
            CanvasScaler.screenMatchMode = CanvasScaler.ScreenMatchMode.Expand;
            CanvasScaler.referenceResolution = new Vector2(1080f, 1920f);
        }
    }
}

