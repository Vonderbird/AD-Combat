using System.Collections;
using ADC.API;
using UnityEngine;

public class Simple_UNI_Materialize : Materialize
{
    public Renderer[] meshes;
    public float materializeDuration = 4.0f;

    public override void Start_in()
    {
        StartCoroutine(MaterializeIn());
    }

    public override void Start_out()
    {
        StartCoroutine(MaterializeOut());
    }

    IEnumerator MaterializeIn()
    {
        float m_step = 1 / (materializeDuration * 50);
        float m_in = 0;
        while (m_in < 1)
        {
            m_in += m_step;
            foreach (var mesh in meshes)
            {
                var materials = mesh.materials;
                foreach (var material in materials)
                {
                    material.SetFloat("_Materialize", m_in);
                }
            }
            yield return new WaitForSeconds(0.02f);
        }
    }
    IEnumerator MaterializeOut()
    {
        float m_step = 1 / (materializeDuration * 50);
        float m_out = 1;
        while (m_out > 0)
        {
            m_out -= m_step;
            foreach (var mesh in meshes)
            {
                var materials = mesh.materials;
                foreach (var material in materials)
                {
                    material.SetFloat("_Materialize", m_out);
                }
            }
            yield return new WaitForSeconds(0.02f);
        }
    }
}