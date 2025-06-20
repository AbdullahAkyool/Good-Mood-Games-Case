using System.Collections.Generic;
using UnityEngine;

public class EnemySkinSelector : MonoBehaviour
{
    [SerializeField] private Renderer meshRenderer;
    [SerializeField] List<Texture> skins;
    private Texture selectedSkin;

    void Awake()
    {
        SetRandomSkin();
    }
    public void SetRandomSkin()
    {
        int randomIndex = Random.Range(0, skins.Count);
        selectedSkin = skins[randomIndex];
        meshRenderer.materials[0].SetTexture("_BaseMap", selectedSkin);
    }
}
