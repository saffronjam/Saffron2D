// Decompiled with JetBrains decompiler
// Type: SFML.System.Vector4f
// Assembly: SFML.System, Version=2.5.0.0, Culture=neutral, PublicKeyToken=null
// MVID: CF4B14B0-2FF9-4A85-8962-6FA5288DDC78
// Assembly location: C:\Program Files\NuGet\packages\sfml.system\2.5.0\lib\netstandard2.0\SFML.System.dll

using System;
using SFML.System;

namespace Saffron2D.Core.SfmlExtension
{
  /// <summary>
  /// Vector4f is an utility class for manipulating 4 dimensional
  /// vectors with float components
  /// </summary>
  public struct Vector4f : IEquatable<Vector4f>
  {
    private const float tolerance = 0.001f;
    
    /// <summary>X (horizontal) component of the vector</summary>
    public float X;
    /// <summary>Y (vertical) component of the vector</summary>
    public float Y;
    /// <summary>Z (depth) component of the vector</summary>
    public float Z;
    /// <summary>W (imaginary) component of the vector</summary>
    public float W;

    /// <summary>Construct the vector from its coordinates</summary>
    /// <param name="x">X coordinate</param>
    /// <param name="y">Y coordinate</param>
    /// <param name="z">Z coordinate</param>
    /// <param name="w">Z coordinate</param>
    public Vector4f(float x, float y, float z, float w)
    {
      this.X = x;
      this.Y = y;
      this.Z = z;
      this.W = w;
    }
    
    public Vector4f(Vector3f v3, float w)
    {
      this.X = v3.X;
      this.Y = v3.Y;
      this.Z = v3.Z;
      this.W = w;
    }

    /// <summary>
    /// Operator - overload ; returns the opposite of a vector
    /// </summary>
    /// <param name="v">Vector to negate</param>
    /// <returns>-v</returns>
    public static Vector4f operator -(Vector4f v) => new Vector4f(-v.X, -v.Y, -v.Z, -v.W);

    /// <summary>Operator - overload ; subtracts two vectors</summary>
    /// <param name="v1">First vector</param>
    /// <param name="v2">Second vector</param>
    /// <returns>v1 - v2</returns>
    public static Vector4f operator -(Vector4f v1, Vector4f v2) => new Vector4f(v1.X - v2.X, v1.Y - v2.Y, v1.Z - v2.Z, v1.W - v2.W);

    /// <summary>Operator + overload ; add two vectors</summary>
    /// <param name="v1">First vector</param>
    /// <param name="v2">Second vector</param>
    /// <returns>v1 + v2</returns>
    public static Vector4f operator +(Vector4f v1, Vector4f v2) => new Vector4f(v1.X + v2.X, v1.Y + v2.Y, v1.Z + v2.Z, v1.W + v2.W);

    /// <summary>
    /// Operator * overload ; multiply a vector by a scalar value
    /// </summary>
    /// <param name="v">Vector</param>
    /// <param name="x">Scalar value</param>
    /// <returns>v * x</returns>
    public static Vector4f operator *(Vector4f v, float x) => new Vector4f(v.X * x, v.Y * x, v.Z * x, v.W * x);

    /// <summary>
    /// Operator * overload ; multiply a scalar value by a vector
    /// </summary>
    /// <param name="x">Scalar value</param>
    /// <param name="v">Vector</param>
    /// <returns>x * v</returns>
    public static Vector4f operator *(float x, Vector4f v) => new Vector4f(v.X * x, v.Y * x, v.Z * x, v.W * x);

    /// <summary>
    /// Operator / overload ; divide a vector by a scalar value
    /// </summary>
    /// <param name="v">Vector</param>
    /// <param name="x">Scalar value</param>
    /// <returns>v / x</returns>
    public static Vector4f operator /(Vector4f v, float x) => new Vector4f(v.X / x, v.Y / x, v.Z / x, v.W / x);

    /// <summary>Operator == overload ; check vector equality</summary>
    /// <param name="v1">First vector</param>
    /// <param name="v2">Second vector</param>
    /// <returns>v1 == v2</returns>
    public static bool operator ==(Vector4f v1, Vector4f v2) => v1.Equals(v2);

    /// <summary>Operator != overload ; check vector inequality</summary>
    /// <param name="v1">First vector</param>
    /// <param name="v2">Second vector</param>
    /// <returns>v1 != v2</returns>
    public static bool operator !=(Vector4f v1, Vector4f v2) => !v1.Equals(v2);

    /// <summary>Provide a string describing the object</summary>
    /// <returns>String description of the object</returns>
    public override string ToString() => string.Format("[Vector4f] X({0}) Y({1}) Z({2}) W({3})", (object) this.X, (object) this.Y, (object) this.Z, (object) this.W);

    /// <summary>
    /// Compare vector and object and checks if they are equal
    /// </summary>
    /// <param name="obj">Object to check</param>
    /// <returns>Object and vector are equal</returns>
    public override bool Equals(object obj) => obj is Vector4f other && this.Equals(other);

    /// <summary>Compare two vectors and checks if they are equal</summary>
    /// <param name="other">Vector to check</param>
    /// <returns>Vectors are equal</returns>
    public bool Equals(Vector4f other) => Math.Abs((double) this.X - (double) other.X) < tolerance && 
                                          Math.Abs((double) this.Y - (double) other.Y) < tolerance && 
                                          Math.Abs((double) this.Z - (double) other.Z) < tolerance && 
                                          Math.Abs((double) this.W - (double) other.W) < tolerance;

    /// <summary>Provide a integer describing the object</summary>
    /// <returns>Integer description of the object</returns>
    public override int GetHashCode() => this.X.GetHashCode() ^ this.Y.GetHashCode() ^ this.Z.GetHashCode() ^ this.W.GetHashCode();
  }
}
