﻿using System.Collections.Generic;
using Odyssey.UserInterface.Style;
using SharpDX.Mathematics;

namespace Odyssey.Graphics.Drawing
{
    public class FigureParser
    {
        private readonly GeometrySink sink;
        private Vector2 startPoint;
        private Vector2 previousPoint;

        public bool IsFigureOpen { get; private set; }

        public FigureParser(GeometrySink sink)
        {
            this.sink = sink;
        }

        public void Execute(IEnumerable<VectorCommand> commands)
        {
            foreach (var instruction in commands)
            {
                bool isRelative = instruction.IsRelative;

                switch (instruction.Type)
                {
                    case CommandType.Move:
                        Move(instruction, isRelative);
                        break;

                    case CommandType.Line:
                        Line(instruction, isRelative);
                        break;

                    case CommandType.HorizontalLine:
                        HorizontalLine(instruction, isRelative);
                        break;

                    case CommandType.VerticalLine:
                        VerticalLine(instruction, isRelative);
                        break;

                    case CommandType.EllipticalArc:
                        Arc(instruction, isRelative);
                        break;

                    case CommandType.CubicBezierCurve:
                        CubicBezierCurve(instruction, isRelative);
                        break;

                    case CommandType.Close:
                        Close(FigureEnd.Closed);
                        break;

                }
            }
        }

        private void HorizontalLine(VectorCommand instruction, bool isRelative)
        {
            var points = new List<Vector2>();
            for (var i = 0; i < instruction.Arguments.Length; i++)
            {
                var point = new Vector2(instruction.Arguments[i], previousPoint.Y);
                if (isRelative)
                    point+= new Vector2(previousPoint.X, 0);
                points.Add(point);
                previousPoint = points[i];
            }
            sink.AddLines(points);
        }

        private void VerticalLine(VectorCommand instruction, bool isRelative)
        {
            var points = new List<Vector2>();
            for (var i = 0; i < instruction.Arguments.Length; i++)
            {
                var point = new Vector2(previousPoint.X, instruction.Arguments[i]);
                if (isRelative)
                    point+= new Vector2(0, previousPoint.Y);
                points.Add(point);
                previousPoint = points[i];
            }
            sink.AddLines(points);
        }

        void Move(VectorCommand instruction, bool isRelative)
        {
            if (IsFigureOpen)
                Close(FigureEnd.Open);

            var point = new Vector2(instruction.Arguments[0], instruction.Arguments[1]);
            if (isRelative)
                point += startPoint;
            startPoint = isRelative ? point + startPoint : point;
            previousPoint = startPoint;
            sink.BeginFigure(startPoint, FigureBegin.Filled);
            IsFigureOpen = true;
        }

        void Line(VectorCommand instruction, bool isRelative)
        {
            var points = new List<Vector2>();
            for (var i = 0; i < instruction.Arguments.Length; i = i + 2)
            {
                var point = new Vector2(instruction.Arguments[i], instruction.Arguments[i + 1]);
                if (isRelative)
                    point += previousPoint;
                points.Add(point);
                previousPoint = points[i];
            }
            sink.AddLines(points);
        }

        void Arc(VectorCommand instruction, bool isRelative)
        {
            for (int i = 0; i < instruction.Arguments.Length; i = i + 6)
            {
                float w = instruction.Arguments[0];
                float h = instruction.Arguments[1];
                float a = instruction.Arguments[2];
                bool isLargeArc = (int) instruction.Arguments[3] == 1;
                bool sweepDirection = (int) instruction.Arguments[4] == 1;

                var p = new Vector2(instruction.Arguments[5], instruction.Arguments[6]);
                if (isRelative)
                    p += previousPoint;
                sink.AddArc(w, h, a, isLargeArc, sweepDirection, p);
            }
        }

        void CubicBezierCurve(VectorCommand instruction, bool isRelative)
        {
            for (int i = 0; i < instruction.Arguments.Length; i = i + 6)
            {
                var p1 = new Vector2(instruction.Arguments[0], instruction.Arguments[1]);
                var p2 = new Vector2(instruction.Arguments[2], instruction.Arguments[3]);
                var p3 = new Vector2(instruction.Arguments[4], instruction.Arguments[5]);
                if (isRelative)
                {
                    p1 += previousPoint;
                    p2 += previousPoint;
                    p3 += previousPoint;
                }
                sink.AddCubicBezierCurve(p1,p2,p3);
            }
        }

        void Close(FigureEnd endType)
        {
            IsFigureOpen = false;
            sink.EndFigure(endType);
        }

    }
}
