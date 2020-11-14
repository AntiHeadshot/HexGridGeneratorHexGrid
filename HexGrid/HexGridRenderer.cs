using Newtonsoft.Json;
using PixelFarm.CpuBlit;
using PixelFarm.Drawing;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using TriangleNet.Geometry;
using TriangleNet.Meshing;
using TriangleNet.Topology;
using YourImplementation;
using Color = System.Drawing.Color;
using File = TagLib.File;
using Image = System.Drawing.Image;

namespace HexGrid
{
    public class ColorPos
    {
        public Color Color;
        public Vector Pos;

        public ColorPos(Color color, Vector pos)
        {
            Color = color;
            Pos = pos;
        }
    }

    public class HexGridRenderer : IDisposable
    {
        // ReSharper disable MemberCanBePrivate.Global
        // ReSharper disable FieldCanBeMadeReadOnly.Global
        public int LayerCnt = 2048;
        public int Multiplier = 1;
        public float HexS = 150f;
        public float MinHexS = 0;
        public float Rotation = -10f;
        public float Twist = -90f;
        public float StrokeW = 0.01f;
        public Color ColorCenter = Color.Black;
        public Color ColorBorder = Color.White;
        public float ColorCenterStrength = 0.4f;
        public float ColorBorderStrength = 0.2f;
        public Vector Offset = Vector.FromNull();

        public ObservableCollection<ColorPos> Colors = new ObservableCollection<ColorPos>();

        [JsonIgnore]
        public int Width
        {
            get => (int)Size.X;
            set => Size.X = value;
        }

        [JsonIgnore]
        public int Height
        {
            get => (int)Size.Y;
            set => Size.Y = value;
        }

        [JsonIgnore]
        public float OffsetX
        {
            get => -Offset.X;
            set => Offset.X = -value;
        }

        [JsonIgnore]
        public float OffsetY
        {
            get => -Offset.Y;
            set => Offset.Y = -value;
        }

        public Vector Size = new Vector(100, 100);


        [JsonIgnore]
        public Bitmap Bitmap { get; private set; }

        [JsonIgnore]
        public int RenderWidth
        {
            get => (int)RenderSize.X;
            set => RenderSize.X = value;
        }

        [JsonIgnore]
        public int RenderHeight
        {
            get => (int)RenderSize.Y;
            set => RenderSize.Y = value;
        }


        [JsonIgnore]
        public Vector RenderSize = new Vector(100, 100);

        public delegate void HexGridEvent(HexGridRenderer hgr);

        [JsonIgnore]
        public HexGridEvent OnRender;

        public delegate void ColorPosEvent(ColorPos hgr);

        [JsonIgnore]
        public ColorPosEvent ColorAdded;

        [JsonIgnore]
        public ColorPosEvent ColorRemoved;

        // ReSharper restore MemberCanBePrivate.Global
        // ReSharper restore FieldCanBeMadeReadOnly.Global

        public HexGridRenderer()
        {
            Colors.CollectionChanged += Colors_CollectionChanged;
        }

        private void Colors_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (object x in e.NewItems)
                    ColorAdded?.Invoke(x as ColorPos);
                UpdateTriangles();
            }

            if (e.Action == NotifyCollectionChangedAction.Remove || e.Action == NotifyCollectionChangedAction.Reset)
            {
                foreach (object x in e.OldItems)
                    ColorRemoved?.Invoke(x as ColorPos);
                UpdateTriangles();
            }
        }

        private List<(Triangle tri, List<ColorPos> colorPos)> _triangles = new List<(Triangle tri, List<ColorPos> colorPos)>();
        private readonly object _trianglesLock = new object();

        public void UpdateTriangles()
        {
            if (Colors.Count <= 3)
                return;

            lock (_trianglesLock)
            {
                Polygon poly = new Polygon(Colors.Count);

                int i = 0;
                foreach (ColorPos colorPos in Colors)
                    poly.Add(new Vertex(colorPos.Pos.X, colorPos.Pos.Y, ++i));

                IMesh mesh = poly.Triangulate();

                List<ColorPos> adds = new List<ColorPos>();
                foreach (int p in mesh.Segments.Select(x => x.P1).Concat(poly.Segments.Select(x => x.P0)).Distinct())
                {
                    List<SubSegment> pts = mesh.Segments.Where(x => x.P1 == p || x.P0 == p).ToList();
                    int p0 = pts[0].P0 == p ? pts[0].P1 : pts[0].P0;
                    int p1 = pts[1].P0 == p ? pts[1].P1 : pts[1].P0;

                    Vertex vertex = mesh.Vertices.ElementAt(p);

                    Vector v = new Vector(vertex);
                    Vector v0 = new Vector(mesh.Vertices.ElementAt(p0)) - v;
                    Vector v1 = new Vector(mesh.Vertices.ElementAt(p1)) - v;

                    float rdx = (v1.Angle - v0.Angle + 360) % 360;

                    v += Vector.FromAngleLength(v0.Angle + rdx / 2 + (rdx < 180 ? 180 : 0), RenderWidth * 2);

                    adds.Add(new ColorPos(Colors[vertex.Label - 1].Color, v));
                }

                foreach (ColorPos colorPos in adds)
                    poly.Add(new Vertex(colorPos.Pos.X, colorPos.Pos.Y, ++i));

                i = 0;
                foreach (Vertex polyPoint in poly.Points)
                {
                    polyPoint.ID = i;
                    i++;
                }

                adds = Colors.Concat(adds).ToList();

                mesh = poly.Triangulate();
                _triangles = new List<(Triangle tri, List<ColorPos> colorPos)>();

                foreach (Triangle tri in mesh.Triangles)
                {
                    (Triangle tri, List<ColorPos> colorPos) newTri = (tri, new List<ColorPos>());

                    for (i = 0; i < 3; i++)
                    {
                        Vertex v = tri.GetVertex(i);
                        newTri.colorPos.Add(adds[v.Label - 1]);

                    }
                    if (newTri.colorPos.Distinct().Count() < 3)
                        ;
                    _triangles.Add(newTri);
                }
            }
        }

        private HexGridRenderer(HexGridRenderer hexGridRenderer)
        {
            LayerCnt = hexGridRenderer.LayerCnt;
            Multiplier = hexGridRenderer.Multiplier;
            HexS = hexGridRenderer.HexS;
            MinHexS = hexGridRenderer.MinHexS;
            Rotation = hexGridRenderer.Rotation;
            Twist = hexGridRenderer.Twist;
            StrokeW = hexGridRenderer.StrokeW;
            ColorCenter = hexGridRenderer.ColorCenter;
            ColorBorder = hexGridRenderer.ColorBorder;
            ColorCenterStrength = hexGridRenderer.ColorCenterStrength;
            ColorBorderStrength = hexGridRenderer.ColorBorderStrength;
            Colors = hexGridRenderer.Colors;
            _triangles = hexGridRenderer._triangles;
            Width = hexGridRenderer.Width;
            Height = hexGridRenderer.Height;
            OffsetX = hexGridRenderer.OffsetX;
            OffsetY = hexGridRenderer.OffsetY;
        }

        public void Render(CancellationToken ct)
        {
            if (RenderWidth < 0 | RenderHeight < 0)
                return;

            using MemBitmap img = new MemBitmap(RenderWidth, RenderHeight);

            AggPainter p = AggPainter.Create(img);

            p.RenderQuality = RenderQuality.HighQuality;
            p.StrokeWidth = StrokeW;
            p.Clear(PixelFarm.Drawing.Color.Black);

            float hexH = (float)Math.Sin(Math.PI / 3) * HexS;
            float hexW = HexS;

            Vector dV = new Vector(0, hexH).RotatedBy(Rotation);
            Vector dH = new Vector(hexW, 0).RotatedBy(Rotation);
            Vector dH3 = dH * 3;

            float cHeight = RenderHeight;
            float cWidth = RenderWidth;

            float maxY = cHeight + hexH * 2 + (RenderWidth - Offset.X) * (float)Math.Tan(-Rotation * Math.PI / 180);

            int lineNr = 1;

            Vector pVert = new Vector(Offset);

            try
            {
                for (; pVert.Y < maxY; pVert += dV, lineNr++)
                {
                    Vector dO = lineNr % 2 == 1 ? new Vector(0, 0) : new Vector(hexW * 1.5f, 0).RotatedBy(Rotation);

                    Vector pHor = dH + pVert + dO;

                    pHor -= dH3 * (float)Math.Floor((pHor.X + HexS) / dH3.X);

                    for (; pHor.X < cWidth + HexS && pHor.Y > -HexS; pHor += dH3)
                    {
                        if (pHor.Y > cHeight + HexS)
                            continue;
                        Vector pHorI = new Vector(pHor);

                        float r = Rotation;

                        float strength = GetStrength(pHorI);
                        int layerCnt = (int)(LayerCnt * strength);

                        for (int layerNr = 0; layerNr < layerCnt * Multiplier; layerNr++)
                        {
                            float sizeStep = (HexS - MinHexS) / layerCnt;

                            int nr = layerNr % layerCnt;
                            if (nr == 0)
                                r = Rotation;

                            if (layerNr % 500 == 0 && ct.IsCancellationRequested)
                                goto doneDraw;

                            float size = MinHexS + sizeStep * (layerCnt - nr);
                            float prevSize = MinHexS + sizeStep * (layerCnt - nr - 1);

                            if (nr == 0)
                                DrawHex(p, pHorI, size, r, 6, true);
                            else
                                DrawHex(p, pHorI, size, r);

                            r -= GetRotation(size, prevSize);
                        }
                    }
                }
            }
            catch (Exception)
            {
            }

            doneDraw:

            MemoryStream ms = new MemoryStream();

            MemBitmapExt.DefaultMemBitmapIO = new ImgCodecMemBitmapIO();

            img.SaveImage(ms, MemBitmapIO.OutputImageFormat.Png);

            Bitmap?.Dispose();

            using (ms = new MemoryStream(ms.ToArray()))
            {
                Bitmap = (Bitmap)Image.FromStream(ms);
            }

            using (Graphics g = Graphics.FromImage(Bitmap))
                g.FillRectangle(Brushes.Yellow, -1, -1, 1, 1);

            OnRender?.Invoke(this);
        }

        float GetRotation(float s, float ps)
        {
            return Math.Min(((s - ps) / s) * Twist, 30);
        }

        Color GetColor(Vector v, float d)
        {
            v -= Offset;
            Color c = Color.HotPink;

            switch (Colors.Count)
            {
                case 0:
                    break;
                case 1:
                    c = Colors[0].Color;
                    break;
                case 2:
                    {
                        Vector ab = Colors[1].Pos - Colors[0].Pos;
                        Vector av = v - Colors[0].Pos;

                        float f = av.Dot(ab) / ab.LengthSqr;
                        c = LerpColor(Colors[0].Color, Colors[1].Color, f);
                    }
                    break;
                default:
                    IReadOnlyList<ColorPos> cs = GetColors(v, out float w0, out float w1, out float w2);

                    c = LerpColor(cs[0].Color, cs[1].Color, w1 / (w0 + w1));
                    c = LerpColor(c, cs[2].Color, w2);

                    break;
            }

            c = LerpColor(ColorCenter, c, Lerp(1 - ColorCenterStrength, 1, d / HexS));
            c = LerpColor(c, ColorBorder, Lerp(0, ColorBorderStrength, d / HexS));

            return c;
        }

        float GetStrength(Vector v)
        {
            v -= Offset;
            float d = 1;

            switch (Colors.Count)
            {
                case 0:
                    break;
                case 1:
                    d = Colors[0].Color.A / 255f;
                    break;
                case 2:
                    {
                        Vector ab = Colors[1].Pos - Colors[0].Pos;
                        Vector av = v - Colors[0].Pos;

                        float f = av.Dot(ab) / ab.LengthSqr;
                        d = Lerp(Colors[0].Color.A, Colors[1].Color.A, f) / 255f;
                    }
                    break;
                default:
                    IReadOnlyList<ColorPos> cs = GetColors(v, out float w0, out float w1, out float w2);

                    d = Lerp(cs[0].Color.A, cs[1].Color.A, w1 / (w0 + w1));
                    d = Lerp(d, cs[2].Color.A, w2) / 255f;
                    break;
            }

            return d;
        }

        private IReadOnlyList<ColorPos> GetColors(Vector v, out float w0, out float w1, out float w2)
        {
            IReadOnlyList<ColorPos> cs;
            if (Colors.Count == 3)
                cs = Colors;
            else
            {
                (Triangle tri, List<ColorPos> colorPos) t = _triangles.FirstOrDefault(x => x.tri.Contains(v.X, v.Y));

                if (t != default)
                    cs = t.colorPos;
                else
                    cs = Colors;
            }

            Vector p = v;
            Vector v1 = cs[0].Pos;
            Vector v2 = cs[1].Pos;
            Vector v3 = cs[2].Pos;

            float div = (v2.Y - v3.Y) * (v1.X - v3.X) +
                        (v3.X - v2.X) * (v1.Y - v3.Y);

            w0 = ((v2.Y - v3.Y) * (p.X - v3.X) +
                  (v3.X - v2.X) * (p.Y - v3.Y)) / div;

            w1 = ((v3.Y - v1.Y) * (p.X - v3.X) +
                  (v1.X - v3.X) * (p.Y - v3.Y)) / div;

            if (div == 0)
            {
                w0 = 0.5f;
                w1 = 0.5f;
            }

            w2 = 1 - w0 - w1;
            return cs;
        }

        private Color LerpColor(Color s, Color t, float k)
        {
            k = Math.Min(Math.Max(k, 0), 1);

            float bk = (1 - k);
            float a = 255;//s.A * bk + t.A * k;
            float r = s.R * bk + t.R * k;
            float g = s.G * bk + t.G * k;
            float b = s.B * bk + t.B * k;
            return Color.FromArgb((int)a, (int)r, (int)g, (int)b);
        }

        private static float Lerp(float p0, float p1, float d)
        {
            d = Math.Min(Math.Max(d, 0), 1);

            return p0 + (p1 - p0) * d;
        }

        private void DrawHex(Painter paint, Vector p, float s, float r, int sides = 6, bool drawHalf = false)
        {

            Vector pO = new Vector(s, 0).RotatedBy(r);
            r = 360f / sides;

            if (drawHalf) sides /= 2;

            Vector lp = pO + p;
            for (int i = 0; i < sides; i++)
            {
                pO = pO.RotatedBy(r);
                Vector pp = pO + p;
                if (LineIntersectsRect(pp, lp, Size))
                {
                    // ======================== Half size lines
                    //Vector dir = (lp - pp);
                    //Vector colorP = pp + dir * 0.5f;
                    //Color c = GetColor(colorP, s);

                    //paint.StrokeColor = ConvertColor(c);

                    //lp = pp + dir * (1 - ((HexS-s) / HexS) / 2);
                    //Vector pp2 = pp + dir * (((HexS-s) / HexS) / 2);

                    //paint.DrawLine(lp.X, lp.Y, pp2.X, pp2.Y);

                    // ======================== Normal
                    Vector colorP = pp + (lp - pp) * 0.5f;
                    Color c = GetColor(colorP, s);

                    paint.StrokeColor = ConvertColor(c);
                    paint.DrawLine(lp.X, lp.Y, pp.X, pp.Y);

                    // ======================== Dot
                    //Vector colorP = pp + (lp - pp) * 0.5f;
                    //Color c = GetColor(colorP, s);

                    //paint.StrokeWidth = 0;
                    //paint.StrokeColor = ConvertColor(c);
                    //paint.FillColor = paint.StrokeColor;
                    //paint.FillRect(colorP.X, colorP.Y, StrokeW * 2, StrokeW * 2);
                }

                lp = pp;
            }
        }

        private static bool LineIntersectsRect(Vector p1, Vector p2, Vector w)
        {
            return LineIntersectsLine(p1, p2, new Vector(0, 0), new Vector(w.X, 0)) ||
                   LineIntersectsLine(p1, p2, new Vector(w.X, 0), new Vector(w.X, w.Y)) ||
                   LineIntersectsLine(p1, p2, new Vector(w.X, w.Y), new Vector(0, w.Y)) ||
                   LineIntersectsLine(p1, p2, new Vector(0, w.Y), new Vector(0, 0)) ||
                   (Contains(w, p1) && Contains(w, p2));
        }

        private static bool Contains(Vector w, Vector p)
        {
            return p.X >= 0 && p.Y >= 0 && p.X <= w.X && p.Y <= w.Y;
        }

        // ReSharper disable InconsistentNaming
        private static bool LineIntersectsLine(Vector l1p1, Vector l1p2, Vector l2p1, Vector l2p2)
        // ReSharper restore InconsistentNaming
        {
            float q = (l1p1.Y - l2p1.Y) * (l2p2.X - l2p1.X) - (l1p1.X - l2p1.X) * (l2p2.Y - l2p1.Y);
            float d = (l1p2.X - l1p1.X) * (l2p2.Y - l2p1.Y) - (l1p2.Y - l1p1.Y) * (l2p2.X - l2p1.X);

            if (d == 0)
            {
                return false;
            }

            float r = q / d;

            q = (l1p1.Y - l2p1.Y) * (l1p2.X - l1p1.X) - (l1p1.X - l2p1.X) * (l1p2.Y - l1p1.Y);
            float s = q / d;

            return !(r < 0) && !(r > 1) && !(s < 0) && !(s > 1);
        }

        private static PixelFarm.Drawing.Color ConvertColor(Color color)
        {
            return new PixelFarm.Drawing.Color(color.A, color.R, color.G, color.B);
        }

        public static implicit operator Bitmap(HexGridRenderer g) => g.Bitmap;

        private CancellationTokenSource _cancle;
        private Semaphore _semaphore = new Semaphore(1, 1);
        private Semaphore _runOnce = new Semaphore(1, 1);

        public Task RenderThreaded(int? anzahlThreads = null)
        {
            return Task.Run(() =>
            {
                if (!_runOnce.WaitOne(1))
                    return;
                _cancle?.Cancel();
                _semaphore.WaitOne();
                _runOnce.Release();
                _cancle = new CancellationTokenSource();
                CancellationToken ct = _cancle.Token;

                List<Task> tasks = new List<Task>();

                int split = Math.Max(anzahlThreads ?? Environment.ProcessorCount, 1);

                int splitW = (int)Math.Floor(Math.Sqrt(split));
                int splitH = split / splitW;

                while (splitH * splitW != split)
                    splitH = split / --splitW;

                int partWidth = (int)Math.Ceiling(RenderWidth / (float)splitW);
                int partHeight = (int)Math.Ceiling(RenderHeight / (float)splitH);

                Bitmap[,] images = new Bitmap[splitW, splitH];

                for (int x = 0; x < splitW; x++)
                    for (int y = 0; y < splitH; y++)
                    {
                        int xx = x;
                        int yy = y;
                        tasks.Add(Task.Run(() =>
                        {
                            HexGridRenderer r = new HexGridRenderer(this);
                            r.OffsetX += xx * partWidth;
                            r.OffsetY += yy * partHeight;
                            r.RenderWidth = Math.Min(partWidth, RenderWidth - xx * partWidth);
                            r.RenderHeight = Math.Min(partHeight, RenderHeight - yy * partHeight);

                            r.Render(ct);

                            images[xx, yy] = r;
                        }));
                    }

                Task.WaitAll(tasks.ToArray());

                Bitmap bmp = new Bitmap(RenderWidth, RenderHeight);
                using (Graphics g = Graphics.FromImage(bmp))
                    for (int x = 0, dx = 0; x < splitW; x++, dx += partWidth)
                        for (int y = 0, dy = 0; y < splitH; y++, dy += partHeight)
                            g.DrawImageUnscaled(images[x, y], dx, dy);

                Bitmap old = Bitmap;
                Bitmap = bmp;

                OnRender?.Invoke(this);
                old?.Dispose();
                _cancle = null;
                _semaphore.Release();

            });
        }

        public void Dispose()
        {
            Bitmap?.Dispose();
            Bitmap = null;
        }

        public void Save(string path)
        {
            if (Bitmap != null)
            {
                string imageDescription = JsonConvert.SerializeObject(this);

                Bitmap.Save(path);
                File file = File.Create(path);
                file.GetTag(TagLib.TagTypes.Png, true).Comment = imageDescription;

                file.Save();
            }
        }

        public void Load(string path)
        {
            string comment;
            try
            {
                comment = File.Create(path).Tag.Comment;
            }
            catch
            {
                return;
            }

            if (comment != null)
            {
                HexGridRenderer data = JsonConvert.DeserializeObject<HexGridRenderer>(comment);

                if (data == null)
                    return;
                LayerCnt = data.LayerCnt;
                Multiplier = data.Multiplier;
                HexS = data.HexS;
                MinHexS = data.MinHexS;
                Rotation = data.Rotation;
                Twist = data.Twist;
                StrokeW = data.StrokeW;
                ColorCenter = data.ColorCenter;
                ColorBorder = data.ColorBorder;
                ColorCenterStrength = data.ColorCenterStrength;
                ColorBorderStrength = data.ColorBorderStrength;
                OffsetX = data.OffsetX;
                OffsetY = data.OffsetY;
                Width = data.Width;
                Height = data.Height;

                while (Colors.Count > 0)
                    Colors.RemoveAt(0);

                foreach (ColorPos dataColor in data.Colors)
                    Colors.Add(dataColor);
            }
        }
    }
}