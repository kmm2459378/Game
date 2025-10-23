using UnityEngine;
using UnityEngine.Rendering;

public class GPUInstancedRenderer : MonoBehaviour
{
    public Mesh mesh;
    public Material material;
    public ComputeShader computeShader;
    public int instanceCount = 1000;

    private ComputeBuffer matrixBuffer;
    private ComputeBuffer argsBuffer;
    private uint[] args = new uint[5];
    private int kernelID;

    private void Start()
    {
        matrixBuffer = new ComputeBuffer(instanceCount, 64);
        kernelID = computeShader.FindKernel("CSMain");
        computeShader.SetBuffer(kernelID, "_Matrices", matrixBuffer);
        computeShader.Dispatch(kernelID, instanceCount / 64 + 1, 1, 1);

        args[0] = (uint)mesh.GetIndexCount(0);
        args[1] = (uint)instanceCount;
        args[2] = (uint)mesh.GetIndexStart(0);
        args[3] = (uint)mesh.GetBaseVertex(0);
        args[4] = 0;

        argsBuffer = new ComputeBuffer(1, args.Length * sizeof(uint), ComputeBufferType.IndirectArguments);
        argsBuffer.SetData(args);


        material.SetBuffer("_Materices", matrixBuffer);
    }

    private void Update()
    {
        
        Graphics.DrawMeshInstanced
        
    }

}
