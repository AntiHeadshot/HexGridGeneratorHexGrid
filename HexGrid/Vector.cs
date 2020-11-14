using System;
using System.IO;
using System.Text;
using TriangleNet.Geometry;

namespace HexGrid
{
    public class Vector
    {
        private static float PI = 3.141593f;
        private float _x;
        private float _y;
        private float lastAngle;

        /// <summary>Creates a new empty vector.</summary>
        public Vector()
        {
        }

        /// <summary>Creates a copy of a vector.</summary>
        /// <param name="vectorToCopy">The vector which will be copied.</param>
        public Vector(Vector vectorToCopy)
        {
            this._x = vectorToCopy._x;
            this._y = vectorToCopy._y;
        }

        public Vector(Vertex vectorToCopy)
        {
            this._x = (float)vectorToCopy.X;
            this._y = (float)vectorToCopy.Y;
        }

        /// <summary>Creates a new vector from xy coordinates.</summary>
        /// <param name="x">The x component of the vector.</param>
        /// <param name="y">The y component of the vector.</param>
        public Vector(float x, float y)
        {
            this._x = x;
            this._y = y;
        }

        /// <summary>Returns a new vector from xy coordinates.</summary>
        /// <param name="x">The x component of the vector.</param>
        /// <param name="y">The y component of the vector.</param>
        /// <returns>The new vector.</returns>
        public static Vector FromXY(float x, float y)
        {
            return new Vector(x, y);
        }

        /// <summary>Returns a new vector from specified angle and length.</summary>
        /// <param name="angle">The angle of the vector.</param>
        /// <param name="length">The length of the vector.</param>
        /// <returns>The new vector.</returns>
        public static Vector FromAngleLength(float angle, float length)
        {
            return new Vector((float)Math.Cos((double)angle * (double)Vector.PI / 180.0) * length, (float)Math.Sin((double)angle * (double)Vector.PI / 180.0) * length);
        }

        /// <summary>Returns an empty vector.</summary>
        /// <returns>An empty vector.</returns>
        public static Vector FromNull()
        {
            return new Vector();
        }

        internal void Write(BinaryWriter writer)
        {
            writer.Write(this._x);
            writer.Write(this._y);
        }

        /// <summary>The x component of the vector.</summary>
        public float X
        {
            get
            {
                return this._x;
            }
            set
            {
                this._x = value;
            }
        }

        /// <summary>The y component of the vector.</summary>
        public float Y
        {
            get
            {
                return this._y;
            }
            set
            {
                this._y = value;
            }
        }

        /// <summary>The angle of the vector.</summary>
        public float Angle
        {
            get
            {
                if ((double)this._x == 0.0 && (double)this._y == 0.0)
                    return this.lastAngle;
                return (float)((Math.Atan2((double)this._y, (double)this._x) * 180.0 / (double)Vector.PI + 360.0) % 360.0);
            }
            set
            {
                double num1 = (double)value * (double)Vector.PI / 180.0;
                float num2 = (float)Math.Sqrt((double)this._x * (double)this._x + (double)this._y * (double)this._y);
                this._x = num2 * (float)Math.Cos(num1);
                this._y = num2 * (float)Math.Sin(num1);
            }
        }

        /// <summary>The length of the vector.</summary>
        public float Length
        {
            get
            {
                return (float)Math.Sqrt((double)this._x * (double)this._x + (double)this._y * (double)this._y);
            }
            set
            {
                if ((double)value == 0.0)
                    this.lastAngle = this.Angle;
                if ((double)this._x == 0.0 && (double)this._y == 0.0)
                {
                    double num = (double)this.lastAngle * (double)Vector.PI / 180.0;
                    this._x = value * (float)Math.Cos(num);
                    this._y = value * (float)Math.Sin(num);
                }
                else
                {
                    float num = value / this.Length;
                    this._x *= num;
                    this._y *= num;
                }
            }
        }


        public float LengthSqr => this._x * this._x + this._y * this._y;

        /// <summary>Returns a new and rotated vector.</summary>
        /// <param name="angle">The angle the vector is rotated by.</param>
        /// <returns>The rotated vector.</returns>
        public Vector RotatedBy(float angle)
        {
            double num = (double)angle * (double)Vector.PI / 180.0;
            return new Vector((float)(Math.Cos(num) * (double)this._x - Math.Sin(num) * (double)this._y), (float)(Math.Sin(num) * (double)this._x + Math.Cos(num) * (double)this._y));
        }

        /// <summary>Returns the angle of a vector.</summary>
        /// <param name="v">The vector.</param>
        /// <returns></returns>
        public float AngleFrom(Vector v)
        {
            float num = v.Angle - this.Angle;
            if ((double)num < 0.0)
                num += 360f;
            return num;
        }

        public float Dot(Vector v)
        {
            return _x * v._x + _y * v._y;
        }

        /// <summary>Returns the sum of two vectors.</summary>
        /// <param name="l">The first vector.</param>
        /// <param name="r">The second vector, which will be added to the first.</param>
        /// <returns>The summation of the two vectors.</returns>
        public static Vector operator +(Vector l, Vector r)
        {
            return new Vector(l._x + r._x, l._y + r._y);
        }

        /// <summary>Returns the difference of two vectors.</summary>
        /// <param name="l">The first vector.</param>
        /// <param name="r">The second vector, which will be subtracted from the first.</param>
        /// <returns>The difference of the two vectors.</returns>
        public static Vector operator -(Vector l, Vector r)
        {
            return new Vector(l._x - r._x, l._y - r._y);
        }

        /// <summary>Returns a new vector with a modified length.</summary>
        /// <param name="vector">The old vector.</param>
        /// <param name="factor">The factor by which length of the old vector is stretched.</param>
        /// <returns>The stretched vector.</returns>
        public static Vector operator *(Vector vector, float factor)
        {
            Vector vector1 = new Vector(factor * vector._x, factor * vector._y);
            if ((double)factor == 0.0)
                vector1.lastAngle = vector.Angle;
            return vector1;
        }

        /// <summary>Returns a new vector with a modified length.</summary>
        /// <param name="vector">The old vector.</param>
        /// <param name="divisor">The factor by which the length of the old vector will be divided.</param>
        /// <returns>The compressed vector.</returns>
        public static Vector operator /(Vector vector, float divisor)
        {
            return new Vector(vector._x / divisor, vector._y / divisor);
        }

        /// <summary>
        /// Checks if the two vectors match with a tolerance of 0.25
        /// </summary>
        /// <param name="l">The first vector.</param>
        /// <param name="r">The second vector.</param>
        /// <returns>True or false.</returns>
        public static bool operator ==(Vector l, Vector r)
        {
            if ((object)l == null && (object)r == null)
                return true;
            if ((object)l == null || (object)r == null)
                return false;
            return l - r < 0.25f;
        }

        /// <summary>
        /// Checks if the length of a vector matches a value with a tolerance of 0.25
        /// </summary>
        /// <param name="l">The vector.</param>
        /// <param name="r">The desired length.</param>
        /// <returns>True or false.</returns>
        public static bool operator ==(Vector l, float r)
        {
            if ((object)l == null)
                return false;
            float length = l.Length;
            if ((double)length - 0.25 < (double)r)
                return (double)length + 0.25 > (double)r;
            return false;
        }

        /// <summary>Checks if the first vector is larger than the second.</summary>
        /// <param name="l">The first vector.</param>
        /// <param name="r">The second vector.</param>
        /// <returns>True or false.</returns>
        public static bool operator >(Vector l, Vector r)
        {
            return (double)l._x * (double)l._x + (double)l._y * (double)l._y > (double)r._x * (double)r._x + (double)r._y * (double)r._y;
        }

        /// <summary>Checks if two vectors are the same.</summary>
        /// <param name="l">The first vector.</param>
        /// <param name="r">The second vector.</param>
        /// <returns>True or false.</returns>
        public static bool operator !=(Vector l, Vector r)
        {
            return !(l == r);
        }

        /// <summary>
        /// Checks if the length of the vector equals the given length.
        /// </summary>
        /// <param name="l">The vector.</param>
        /// <param name="r">The length to compare.</param>
        /// <returns>True or false.</returns>
        public static bool operator !=(Vector l, float r)
        {
            return !(l == r);
        }

        /// <summary>
        /// Checks if the first vector is smaller than the second.
        /// </summary>
        /// <param name="l">The first vector.</param>
        /// <param name="r">The second vector.</param>
        /// <returns>True or false.</returns>
        public static bool operator <(Vector l, Vector r)
        {
            return (double)l._x * (double)l._x + (double)l._y * (double)l._y < (double)r._x * (double)r._x + (double)r._y * (double)r._y;
        }

        /// <summary>Checks if the vector is shorter.</summary>
        /// <param name="l"></param>
        /// <param name="r"></param>
        /// <returns></returns>
        public static bool operator >(Vector l, float r)
        {
            return (double)l._x * (double)l._x + (double)l._y * (double)l._y > (double)r * (double)r;
        }

        /// <summary>Checks if the vector is longer.</summary>
        /// <param name="l">The vector.</param>
        /// <param name="r">The length to compare.</param>
        /// <returns>True or false.</returns>
        public static bool operator <(Vector l, float r)
        {
            return (double)l._x * (double)l._x + (double)l._y * (double)l._y < (double)r * (double)r;
        }

        /// <summary>Unary minus operator</summary>
        /// <param name="v">Input vector</param>
        /// <returns>A Vector pointing in the opposite direction</returns>
        public static Vector operator -(Vector v)
        {
            return new Vector(-v.X, -v.Y);
        }

        /// <summary>
        /// Checks if any component of the vector is infinite or not a number.
        /// </summary>
        public bool IsDamaged
        {
            get
            {
                if (!float.IsInfinity(this._x) && !float.IsNaN(this._x) && !float.IsInfinity(this._y))
                    return float.IsNaN(this._y);
                return true;
            }
        }

        /// <summary>Returns the vector.</summary>
        /// <returns>A string.</returns>
        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            if (float.IsInfinity(this._x) || float.IsNaN(this._x) || (float.IsInfinity(this._y) || float.IsNaN(this._y)))
                stringBuilder.Append("DAMAGED: ");
            if (float.IsPositiveInfinity(this._x))
                stringBuilder.Append("+INV");
            else if (float.IsNegativeInfinity(this._x))
                stringBuilder.Append("-INV");
            else if (float.IsNaN(this._x))
                stringBuilder.Append("NAN");
            else
                stringBuilder.Append(this._x.ToString("F"));
            stringBuilder.Append('/');
            if (float.IsPositiveInfinity(this._y))
                stringBuilder.Append("+INV");
            else if (float.IsNegativeInfinity(this._y))
                stringBuilder.Append("-INV");
            else if (float.IsNaN(this._y))
                stringBuilder.Append("NAN");
            else
                stringBuilder.Append(this._y.ToString("F"));
            return stringBuilder.ToString();
        }

        /// <summary>Checks if this equals an object.</summary>
        /// <param name="obj">The object to compare with this vector.</param>
        /// <returns>True or false.</returns>
        public override bool Equals(object obj)
        {
            return (object)this == obj;
        }

        /// <summary>Returns the hashcode of this vector.</summary>
        /// <returns>The hashcode as an int.</returns>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}