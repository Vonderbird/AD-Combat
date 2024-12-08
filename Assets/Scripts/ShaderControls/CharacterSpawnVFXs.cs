using ADC.API;
using UnityEngine;
using UnityEngine.VFX;

namespace ADC
{
    public class CharacterCreationVFXs : MonoBehaviour
    {
        [SerializeField] private VisualEffect[] visualEffects;
        [SerializeField] private Materialize[] materializes;

        [SerializeField] private Renderer[] manualControlRenderer;

        public void OnSpawnCharacter()
        {
            foreach (var v in visualEffects)
                v.SendEvent("in");

            foreach (var m in materializes)
                m.Start_in();

            foreach (var renderer1 in manualControlRenderer)
                renderer1.enabled = true;
        }

        public void OnDeleteCharacter()
        {
            foreach (var v in visualEffects)
                v.SendEvent("out");

            foreach (var m in materializes)
                m.Start_out();

            foreach (var renderer1 in manualControlRenderer)
                renderer1.enabled = false;
        }
    }

}
