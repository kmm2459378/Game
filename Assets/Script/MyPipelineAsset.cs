using UnityEngine;
using UnityEngine.Rendering;

//�V�F�[�_�[�̃x�[�X
[CreateAssetMenu(menuName = "Rendering/MyPipelineAsset")]
public class MyPipelineAsset : RenderPipelineAsset
{
    protected override RenderPipeline CreatePipeline()
    {
        return new MyRenderPipeline();
    }
}