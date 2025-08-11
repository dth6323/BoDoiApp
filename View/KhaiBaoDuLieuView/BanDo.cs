using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using BoDoiApp.DataLayer;

namespace BoDoiApp.View.KhaiBaoDuLieuView
{
   
    public partial class BanDo : Form
    {
        private const int TILE_SIZE = 512;
        private const int MAX_CACHE_TILES = 100;

        private Image originalImage;
        private float zoomLevel = 1.0f;
        private PointF offset = new PointF(0, 0);
        private bool isDragging = false;
        private Point lastMousePos;
        private PictureBox pictureBox;

        // Tile management
        private Dictionary<string, Image> tileCache = new Dictionary<string, Image>();
        private Queue<string> tileCacheOrder = new Queue<string>();
        private Dictionary<int, Size> pyramidSizes = new Dictionary<int, Size>();
        private int maxPyramidLevel = 0;
        private Size originalImageSize;

        // Icon management
        private List<MapIcon> mapIcons = new List<MapIcon>();
        private Image locationIcon;
        public BanDo()
        {
            InitializeComponent();
            CreateDefaultLocationIcon();
            pictureBox = new PictureBox
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White
            };
            pictureBox.Paint += PictureBox_Paint;
            pictureBox.MouseWheel += PictureBox_MouseWheel;
            pictureBox.MouseDown += PictureBox_MouseDown;
            pictureBox.MouseMove += PictureBox_MouseMove;
            pictureBox.MouseUp += PictureBox_MouseUp;
            pictureBox.MouseClick += PictureBox_MouseClick;

            this.Controls.Add(pictureBox);

            Button loadButton = new Button
            {
                Text = "Load Image",
                Dock = DockStyle.Top
            };
            loadButton.Click += LoadButton_Click;
            this.Controls.Add(loadButton);

            this.DoubleBuffered = true;
        }

        private void CreateDefaultLocationIcon()
        {
            locationIcon = new Bitmap(32, 32);
            using (Graphics g = Graphics.FromImage(locationIcon))
            {
                g.SmoothingMode = SmoothingMode.AntiAlias;
                g.Clear(Color.Transparent);
                using (SolidBrush brush = new SolidBrush(Color.Red))
                {
                    g.FillEllipse(brush, 8, 4, 16, 16);
                    Point[] triagle =
                    {
                        new Point(16, 20),
                        new Point(12, 28),
                        new Point(20, 28)
                    };
                    g.FillPolygon(brush, triagle);

                }
                using (SolidBrush whiteBrush = new SolidBrush(Color.White))
                {
                    g.FillEllipse(whiteBrush, 12, 8, 8, 8);
                }
            }
        }
        private void LoadButton_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Filter = "Image Files (*.jpg;*.jpeg;*.png;*.bmp;*.tiff)|*.jpg;*.jpeg;*.png;*.bmp;*.tiff|All Files (*.*)|*.*";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    this.Cursor = Cursors.WaitCursor;

                    Task.Run(() =>
                    {
                        try
                        {
                            using (var stream = new FileStream(openFileDialog.FileName,
                                FileMode.Open, FileAccess.Read, FileShare.Read, bufferSize: 65536))
                            {
                                var newImage = Image.FromStream(stream);

                                this.Invoke(new Action(() =>
                                {
                                    ClearCache();
                                    originalImage?.Dispose();
                                    originalImage = newImage;
                                    originalImageSize = originalImage.Size;

                                    // Calculate pyramid levels
                                    CalculatePyramidLevels();

                                    zoomLevel = 1.0f;
                                    offset = new PointF(0, 0);
                                    pictureBox.Invalidate();
                                    this.Cursor = Cursors.Default;
                                }));
                            }
                        }
                        catch (Exception ex)
                        {
                            this.Invoke(new Action(() =>
                            {
                                MessageBox.Show($"Error loading image: {ex.Message}");
                                this.Cursor = Cursors.Default;
                            }));
                        }
                    });
                }
            }
        }

        private void CalculatePyramidLevels()
        {
            pyramidSizes.Clear();
            maxPyramidLevel = 0;

            int width = originalImageSize.Width;
            int height = originalImageSize.Height;
            int level = 0;

            while (width > TILE_SIZE || height > TILE_SIZE)
            {
                pyramidSizes[level] = new Size(width, height);
                width /= 2;
                height /= 2;
                level++;
            }

            pyramidSizes[level] = new Size(width, height);
            maxPyramidLevel = level;
        }

        private int GetBestPyramidLevel(float zoom)
        {
            // Choose pyramid level based on zoom
            if (zoom >= 1.0f) return 0; // Use original resolution

            float targetScale = 1.0f / zoom;
            int level = 0;

            while (level < maxPyramidLevel && targetScale > 2.0f)
            {
                targetScale /= 2.0f;
                level++;
            }

            return Math.Min(level, maxPyramidLevel);
        }

        private void PictureBox_Paint(object sender, PaintEventArgs e)
        {
            if (originalImage == null) return;

            Graphics g = e.Graphics;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;

            int pyramidLevel = GetBestPyramidLevel(zoomLevel);
            Size pyramidSize = pyramidSizes[pyramidLevel];
            float pyramidScale = (float)pyramidSize.Width / originalImageSize.Width;
            float effectiveZoom = zoomLevel / pyramidScale;

            // Calculate visible area
            Rectangle clipRect = e.ClipRectangle;

            // Convert screen coordinates to image coordinates
            float imageLeft = (-offset.X) / effectiveZoom;
            float imageTop = (-offset.Y) / effectiveZoom;
            float imageRight = imageLeft + clipRect.Width / effectiveZoom;
            float imageBottom = imageTop + clipRect.Height / effectiveZoom;

            // Calculate tile range
            int startTileX = Math.Max(0, (int)Math.Floor(imageLeft / TILE_SIZE));
            int startTileY = Math.Max(0, (int)Math.Floor(imageTop / TILE_SIZE));
            int endTileX = Math.Min((pyramidSize.Width - 1) / TILE_SIZE, (int)Math.Ceiling(imageRight / TILE_SIZE));
            int endTileY = Math.Min((pyramidSize.Height - 1) / TILE_SIZE, (int)Math.Ceiling(imageBottom / TILE_SIZE));

            // Render visible tiles
            for (int tileY = startTileY; tileY <= endTileY; tileY++)
            {
                for (int tileX = startTileX; tileX <= endTileX; tileX++)
                {
                    Image tile = GetTile(pyramidLevel, tileX, tileY);
                    if (tile != null)
                    {
                        float screenX = offset.X + tileX * TILE_SIZE * effectiveZoom;
                        float screenY = offset.Y + tileY * TILE_SIZE * effectiveZoom;
                        float screenWidth = tile.Width * effectiveZoom;
                        float screenHeight = tile.Height * effectiveZoom;

                        RectangleF destRect = new RectangleF(screenX, screenY, screenWidth, screenHeight);
                        g.DrawImage(tile, destRect);
                    }
                }
            }
            DrawMapIcons(g, effectiveZoom);
        }
        private void DrawMapIcons(Graphics g, float effectiveZoom)
        {
            Console.WriteLine($"Drawing {mapIcons.Count} map icons at zoom level {effectiveZoom}");
            foreach (var icon in mapIcons)
            {
                // Chuyển đổi vị trí từ tọa độ ảnh sang tọa độ màn hình
                float screenX = offset.X + icon.ImagePosition.X * effectiveZoom;
                float screenY = offset.Y + icon.ImagePosition.Y * effectiveZoom;

                // Tính toán kích thước icon theo zoom level
                float iconSize = icon.Size * Math.Min(effectiveZoom, 1.0f); // Giới hạn kích thước tối đa
                iconSize = Math.Max(iconSize, 16); // Kích thước tối thiểu

                // Căn giữa icon
                float iconX = screenX - iconSize / 2;
                float iconY = screenY - iconSize;

                // Chỉ vẽ icon nếu nó nằm trong vùng hiển thị
                Rectangle iconRect = new Rectangle((int)iconX, (int)iconY, (int)iconSize, (int)iconSize);
                if (iconRect.IntersectsWith(new Rectangle(0, 0, pictureBox.Width, pictureBox.Height)))
                {
                    // Vẽ icon
                    if (locationIcon != null)
                    {
                        g.DrawImage(locationIcon, iconRect);
                    }

                    // Vẽ label nếu có
                    if (!string.IsNullOrEmpty(icon.Label))
                    {
                        using (Font font = new Font("Arial", 8))
                        using (SolidBrush textBrush = new SolidBrush(Color.Black))
                        using (SolidBrush backgroundBrush = new SolidBrush(Color.White))
                        {
                            SizeF textSize = g.MeasureString(icon.Label, font);
                            float textX = screenX - textSize.Width / 2;
                            float textY = screenY + 5;

                            // Vẽ nền cho text
                            RectangleF textBackground = new RectangleF(textX - 2, textY, textSize.Width + 4, textSize.Height);
                            g.FillRectangle(backgroundBrush, textBackground);
                            g.DrawRectangle(Pens.Black, Rectangle.Round(textBackground));

                            // Vẽ text
                            g.DrawString(icon.Label, font, textBrush, textX, textY);
                        }
                    }
                }
            }
        }
        private Image GetTile(int level, int tileX, int tileY)
        {
            string tileKey = $"{level}_{tileX}_{tileY}";

            if (tileCache.ContainsKey(tileKey))
            {
                return tileCache[tileKey];
            }

            // Generate tile on demand
            Image tile = GenerateTile(level, tileX, tileY);
            if (tile != null)
            {
                CacheTile(tileKey, tile);
            }

            return tile;
        }


        private Image GenerateTile(int level, int tileX, int tileY)
        {
            if (!pyramidSizes.ContainsKey(level)) return null;

            Size pyramidSize = pyramidSizes[level];
            float scale = (float)pyramidSize.Width / originalImageSize.Width;

            int sourceX = (int)(tileX * TILE_SIZE / scale);
            int sourceY = (int)(tileY * TILE_SIZE / scale);
            int sourceWidth = (int)(TILE_SIZE / scale);
            int sourceHeight = (int)(TILE_SIZE / scale);

            // Clamp to image bounds
            sourceX = Math.Max(0, Math.Min(sourceX, originalImageSize.Width - 1));
            sourceY = Math.Max(0, Math.Min(sourceY, originalImageSize.Height - 1));
            sourceWidth = Math.Min(sourceWidth, originalImageSize.Width - sourceX);
            sourceHeight = Math.Min(sourceHeight, originalImageSize.Height - sourceY);

            if (sourceWidth <= 0 || sourceHeight <= 0) return null;

            int tileWidth = Math.Min(TILE_SIZE, (int)(sourceWidth * scale));
            int tileHeight = Math.Min(TILE_SIZE, (int)(sourceHeight * scale));

            Bitmap tile = new Bitmap(tileWidth, tileHeight, PixelFormat.Format24bppRgb);

            using (Graphics g = Graphics.FromImage(tile))
            {
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.SmoothingMode = SmoothingMode.HighQuality;
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;

                Rectangle destRect = new Rectangle(0, 0, tileWidth, tileHeight);
                Rectangle srcRect = new Rectangle(sourceX, sourceY, sourceWidth, sourceHeight);

                g.DrawImage(originalImage, destRect, srcRect, GraphicsUnit.Pixel);
            }

            return tile;
        }

        private void CacheTile(string tileKey, Image tile)
        {
            // Remove oldest tiles if cache is full
            while (tileCache.Count >= MAX_CACHE_TILES && tileCacheOrder.Count > 0)
            {
                string oldestKey = tileCacheOrder.Dequeue();
                if (tileCache.ContainsKey(oldestKey))
                {
                    tileCache[oldestKey]?.Dispose();
                    tileCache.Remove(oldestKey);
                }
            }

            tileCache[tileKey] = tile;
            tileCacheOrder.Enqueue(tileKey);
        }

        private void ClearCache()
        {
            foreach (var tile in tileCache.Values)
            {
                tile?.Dispose();
            }
            tileCache.Clear();
            tileCacheOrder.Clear();
        }

        private void PictureBox_MouseWheel(object sender, MouseEventArgs e)
        {
            if (originalImage == null) return;

            float zoomSpeed = 0.1f;
            float oldZoomLevel = zoomLevel;
            if (e.Delta > 0)
                zoomLevel = Math.Min(zoomLevel + zoomSpeed, 10.0f);
            else
                zoomLevel = Math.Max(zoomLevel - zoomSpeed, 0.1f);

            float mouseX = e.X - offset.X;
            float mouseY = e.Y - offset.Y;
            float scaleChange = zoomLevel / oldZoomLevel;
            offset.X = e.X - mouseX * scaleChange;
            offset.Y = e.Y - mouseY * scaleChange;

            ClampOffset();
            pictureBox.Invalidate();
        }

        private void PictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = true;
                lastMousePos = e.Location;
            }
        }

        private void PictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragging)
            {
                offset.X += e.X - lastMousePos.X;
                offset.Y += e.Y - lastMousePos.Y;
                lastMousePos = e.Location;

                ClampOffset();
                pictureBox.Invalidate();
            }
        }

        private void PictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                isDragging = false;
            }
        }
        private void PictureBox_MouseClick(object sender, MouseEventArgs e)
        {
            Console.WriteLine($"Mouse clicked at: {e.Location}");
            if (originalImage == null || e.Button != MouseButtons.Right) return;

            // Chuyển đổi tọa độ click từ màn hình sang tọa độ ảnh
            int pyramidLevel = GetBestPyramidLevel(zoomLevel);
            Size pyramidSize = pyramidSizes[pyramidLevel];
            float pyramidScale = (float)pyramidSize.Width / originalImageSize.Width;
            float effectiveZoom = zoomLevel / pyramidScale;

            float imageX = (e.X - offset.X) / effectiveZoom;
            float imageY = (e.Y - offset.Y) / effectiveZoom;

            // Thêm icon mới
            AddMapIcon(new PointF(imageX, imageY), $"Location {mapIcons.Count + 1}");
        }
        public void AddMapIcon(PointF imagePosition, string label = "")
        {
            mapIcons.Add(new MapIcon
            {
                ImagePosition = imagePosition,
                Label = label,
                IconColor = Color.Red,
                Size = 32
            });

            pictureBox.Invalidate();
        }

        public void ClearMapIcons()
        {
            mapIcons.Clear();
            pictureBox.Invalidate();
        }
        private void ClampOffset()
        {
            if (originalImage == null) return;

            float scaledWidth = originalImageSize.Width * zoomLevel;
            float scaledHeight = originalImageSize.Height * zoomLevel;

            offset.X = Math.Min(0, Math.Max(offset.X, pictureBox.Width - scaledWidth));
            offset.Y = Math.Min(0, Math.Max(offset.Y, pictureBox.Height - scaledHeight));
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            locationIcon?.Dispose();
            ClearCache();
            originalImage?.Dispose();
            base.OnFormClosing(e);
        }

        
    }
}
