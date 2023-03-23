using System;
using DG.Tweening;
using UnityEngine;

namespace Plugins.MobileBlur
{
    [ExecuteInEditMode]
    public class MobileBlur : MonoBehaviour, IBlur
    {
        private static readonly string KernelKeyword = "KERNEL";
        private static readonly int BlurAmountString = Shader.PropertyToID("_BlurAmount");
        private static readonly int BlurTexString = Shader.PropertyToID("_BlurTex");
        private static readonly int MaskTexString = Shader.PropertyToID("_MaskTex");
        
        private Texture2D _previous;
        private Sequence _sequence;
        
        [SerializeField, Range(1, 5)] private int numberOfPasses = 3;
        [SerializeField,Range(2, 3)] private int kernelSize = 2;
        [SerializeField,Range(0, 3)] private float blurAmount = 0;
        [SerializeField] private Texture2D maskTexture;
        [SerializeField] private Material material = null;
        [SerializeField] private float targetBlur;
        [SerializeField] private float switchDuration;

        public bool Enabled => gameObject.activeInHierarchy;

        public void Enable()
        {
            _sequence?.Kill();
            _sequence = DOTween.Sequence();

            blurAmount = 0f;
            gameObject.SetActive(true);
            _sequence.Append(DOTween.To(() => blurAmount, (v) => blurAmount = v, targetBlur, switchDuration));
        }

        public void Disable()
        {
            _sequence?.Kill();
            _sequence = DOTween.Sequence();

            _sequence.Append(DOTween.To(() => blurAmount, (v) => blurAmount = v, 0, switchDuration));
            _sequence.OnComplete(() => gameObject.SetActive(false));
        }

        private void OnDestroy()
        {
            _sequence?.Kill();
        }

        private void OnRenderImage(RenderTexture source, RenderTexture destination)
        {
            if (kernelSize == 2)
                material.DisableKeyword(KernelKeyword);
            else
                material.EnableKeyword(KernelKeyword);

            material.SetFloat(BlurAmountString, blurAmount);

            if(maskTexture != null || _previous != maskTexture)
            {
                _previous = maskTexture;
                material.SetTexture(MaskTexString, maskTexture);
            }

            RenderTexture blurTex = null;

            if (blurAmount == 0)
            {
                Graphics.Blit(source, destination);
                return;
            }
            else if (numberOfPasses == 1)
            {
                blurTex = RenderTexture.GetTemporary(Screen.width / 2, Screen.height / 2, 0, source.format);
                Graphics.Blit(source, blurTex, material, 0);
            }
            else if (numberOfPasses == 2)
            {
                blurTex = RenderTexture.GetTemporary(Screen.width / 2, Screen.height / 2, 0, source.format);
                var temp1 = RenderTexture.GetTemporary(Screen.width / 4, Screen.height / 4, 0, source.format);
                Graphics.Blit(source, temp1, material, 0);
                Graphics.Blit(temp1, blurTex, material, 0);
                RenderTexture.ReleaseTemporary(temp1);
            }
            else if (numberOfPasses == 3)
            {
                blurTex = RenderTexture.GetTemporary(Screen.width / 4, Screen.height / 4, 0, source.format);
                var temp1 = RenderTexture.GetTemporary(Screen.width / 8, Screen.height / 8, 0, source.format);
                Graphics.Blit(source, blurTex, material, 0);
                Graphics.Blit(blurTex, temp1, material, 0);
                Graphics.Blit(temp1, blurTex, material, 0);
                RenderTexture.ReleaseTemporary(temp1);
            }
            else if (numberOfPasses == 4)
            {
                blurTex = RenderTexture.GetTemporary(Screen.width / 8, Screen.height / 8, 0, source.format);
                var temp1 = RenderTexture.GetTemporary(Screen.width / 16, Screen.height / 16, 0, source.format);
                var temp2 = RenderTexture.GetTemporary(Screen.width / 4, Screen.height / 4, 0, source.format);
                Graphics.Blit(source, temp2, material, 0);
                Graphics.Blit(temp2, blurTex, material, 0);
                Graphics.Blit(blurTex, temp1, material, 0);
                Graphics.Blit(temp1, blurTex, material, 0);
                RenderTexture.ReleaseTemporary(temp1);
                RenderTexture.ReleaseTemporary(temp2);
            }
            else if (numberOfPasses == 5)
            {
                blurTex = RenderTexture.GetTemporary(Screen.width / 4, Screen.height / 4, 0, source.format);
                var temp1 = RenderTexture.GetTemporary(Screen.width / 8, Screen.height / 8, 0, source.format);
                var temp2 = RenderTexture.GetTemporary(Screen.width / 16, Screen.height / 16, 0, source.format);
                Graphics.Blit(source, blurTex, material, 0);
                Graphics.Blit(blurTex, temp1, material, 0);
                Graphics.Blit(temp1, temp2, material, 0);
                Graphics.Blit(temp2, temp1, material, 0);
                Graphics.Blit(temp1, blurTex, material, 0);
                RenderTexture.ReleaseTemporary(temp1);
                RenderTexture.ReleaseTemporary(temp2);
            }

            material.SetTexture(BlurTexString, blurTex);
            RenderTexture.ReleaseTemporary(blurTex);

            Graphics.Blit(source, destination, material, 1);
        }
    }
}