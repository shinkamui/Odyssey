﻿using System;

namespace Odyssey.Graphics
{
    /// <summary>
    /// Describes a mipmap.
    /// </summary>
    public class MipMapDescription : IEquatable<MipMapDescription>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MipMapDescription" /> class.
        /// </summary>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="depth">The depth.</param>
        /// <param name="rowStride">The row stride.</param>
        /// <param name="depthStride">The depth stride.</param>
        /// <param name="widthPacked">The packed width.</param>
        /// <param name="heightPacked">The packed height.</param>
        public MipMapDescription(int width, int height, int depth, int rowStride, int depthStride, int widthPacked, int heightPacked)
        {
            Width = width;
            Height = height;
            Depth = depth;
            RowStride = rowStride;
            DepthStride = depthStride;
            MipmapSize = depthStride * depth;
            WidthPacked = widthPacked;
            HeightPacked = heightPacked;
        }

        /// <summary>
        /// Width of this mipmap.
        /// </summary>
        public readonly int Width;

        /// <summary>
        /// Height of this mipmap.
        /// </summary>
        public readonly int Height;

        /// <summary>
        /// Width of this mipmap.
        /// </summary>
        public readonly int WidthPacked;

        /// <summary>
        /// Height of this mipmap.
        /// </summary>
        public readonly int HeightPacked;

        /// <summary>
        /// Depth of this mipmap.
        /// </summary>
        public readonly int Depth;

        /// <summary>
        /// RowStride of this mipmap (number of bytes per row).
        /// </summary>
        public readonly int RowStride;

        /// <summary>
        /// DepthStride of this mipmap (number of bytes per depth slice).
        /// </summary>
        public readonly int DepthStride;

        /// <summary>
        /// Size in bytes of this whole mipmap.
        /// </summary>
        public readonly int MipmapSize;

        public bool Equals(MipMapDescription other)
        {
            if (ReferenceEquals(null, other))
                return false;
            if (ReferenceEquals(this, other))
                return true;
            return Width == other.Width && Height == other.Height && WidthPacked == other.WidthPacked && HeightPacked == other.HeightPacked && Depth == other.Depth && RowStride == other.RowStride && MipmapSize == other.MipmapSize && DepthStride == other.DepthStride;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
                return false;
            if (ReferenceEquals(this, obj))
                return true;
            if (obj.GetType() != GetType())
                return false;
            return Equals((MipMapDescription)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = Width;
                hashCode = (hashCode * 397) ^ Height;
                hashCode = (hashCode * 397) ^ WidthPacked;
                hashCode = (hashCode * 397) ^ HeightPacked;
                hashCode = (hashCode * 397) ^ Depth;
                hashCode = (hashCode * 397) ^ RowStride;
                hashCode = (hashCode * 397) ^ MipmapSize;
                hashCode = (hashCode * 397) ^ DepthStride;
                return hashCode;
            }
        }

        /// <summary>
        /// Implements the ==.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator ==(MipMapDescription left, MipMapDescription right)
        {
            return Equals(left, right);
        }

        /// <summary>
        /// Implements the !=.
        /// </summary>
        /// <param name="left">The left.</param>
        /// <param name="right">The right.</param>
        /// <returns>The result of the operator.</returns>
        public static bool operator !=(MipMapDescription left, MipMapDescription right)
        {
            return !Equals(left, right);
        }
    }
}
