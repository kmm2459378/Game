using UnityEngine;
using UnityEngine.Rendering;

//シェーダーのベース
[CreateAssetMenu(menuName = "Rendering/MyPipelineAsset")]
public class MyPipelineAsset : RenderPipelineAsset
{
    protected override RenderPipeline CreatePipeline()
    {
        return new MyRenderPipeline();
    }
}