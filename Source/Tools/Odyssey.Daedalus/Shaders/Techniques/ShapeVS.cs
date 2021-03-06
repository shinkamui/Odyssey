﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Odyssey.Daedalus.Shaders.Nodes;
using Odyssey.Daedalus.Shaders.Nodes.Functions;
using Odyssey.Daedalus.Shaders.Nodes.Math;
using Odyssey.Daedalus.Shaders.Nodes.Operators;
using Odyssey.Daedalus.Shaders.Structs;
using Odyssey.Engine;
using Odyssey.Graphics.Effects;
using Odyssey.Graphics.Shaders;
using ConstantBuffer = Odyssey.Daedalus.Shaders.Structs.ConstantBuffer;

namespace Odyssey.Daedalus.Shaders.Techniques
{
    public class ShapeVS : Shader
    {
        public ShapeVS()
        {
            Name = "ShapeVS";
            Type = ShaderType.Vertex;
            FeatureLevel = FeatureLevel.VS_4_0_Level_9_1;
            KeyPart = new TechniqueKey(vs: VertexShaderFlags.Position | VertexShaderFlags.Color);
            EnableSeparators = true;
            InputStruct = Struct.VertexPositionColor;
            OutputStruct = VSOut;

            ConstantBuffer cbScene = CBPerFrame;
            ConstantBuffer cbInstance = CBPerInstance;
            Add(cbScene);
            Add(cbInstance);

            var nPosV3toV4 = CastNode.PositionV3toV4(InputStruct[Param.SemanticVariables.Position]);
            var mulPosWVP = new MatrixMultiplyNode
            {
                Input1 = nPosV3toV4,
                Input2 = MatrixMultiplyNode.WorldViewProjection,
            };

            CustomOutputNode outputNode = new CustomOutputNode() { Output = OutputStruct };
            outputNode.RegisterField(Vector.ClipPosition, mulPosWVP, Shaders.Type.Float4);
            outputNode.RegisterField(Vector.Color, new ReferenceNode { Value = InputStruct[Param.SemanticVariables.Color] }, Shaders.Type.Float4);

            Result = outputNode;
        }

        public static Struct VSOut
        {
            get
            {
                Struct vpt = new Struct
                {
                    CustomType = CustomType.VSOut,
                    Name = "output",
                };
                vpt.Add(Vector.ClipPosition);
                vpt.Add(Vector.Color);
                return vpt;
            }
        }

        public static ConstantBuffer CBPerFrame
        {
            get
            {
                ConstantBuffer cbFrame = new ConstantBuffer
                {
                    Name = Param.ConstantBuffer.PerFrame,
                    UpdateType = UpdateType.SceneFrame
                };
                cbFrame.Add(Matrix.CameraView);
                cbFrame.Add(Matrix.CameraProjection);

                return cbFrame;
            }
        }

        public static ConstantBuffer CBPerInstance
        {
            get
            {
                ConstantBuffer cbFrame = new ConstantBuffer
                {
                    Name = Param.ConstantBuffer.PerInstance,
                    UpdateType = UpdateType.InstanceFrame
                };
                cbFrame.Add(Matrix.EntityWorld);

                return cbFrame;
            }
        }
    }
}
