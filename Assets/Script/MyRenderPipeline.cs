using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.RendererUtils;
using System.Collections.Generic;


public class MyRenderPipeline : RenderPipeline
{
    //GPUに送る設計図の作成
    private CommandBuffer cmd = new CommandBuffer { name = "MyCustomRender" };
    //Shaderを番号で区別するためのデータ
    private ShaderTagId shaderTag = new ShaderTagId("SRPDefaultUnlit");

    protected override void Render(ScriptableRenderContext context,List<Camera> cameras)
    {
        foreach (var camera in cameras)
        {
            if (!camera.TryGetCullingParameters(out var cullParams))
            continue; 

            var cullResults = context.Cull(ref cullParams);
            context.SetupCameraProperties(camera);

            cmd.ClearRenderTarget(true, true, Color.black);
            context.ExecuteCommandBuffer(cmd);
            cmd.Clear();

            var sorting = new SortingSettings(camera) { criteria = SortingCriteria.CommonOpaque };
            var rendererListDesc = new RendererListDesc(shaderTag, cullResults, camera)
            {
                sortingCriteria = SortingCriteria.CommonOpaque,
                renderQueueRange = RenderQueueRange.opaque,

            };

            var rendererList = context.CreateRendererList(rendererListDesc);
            
            cmd.DrawRendererList(rendererList);

            context.ExecuteCommandBuffer(cmd);
            cmd.Clear();

            context.Submit();
        }
    }
}