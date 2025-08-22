using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Linq;
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

        // Icon management - Cập nhật
        private List<MapIcon> mapIcons = new List<MapIcon>();
        private Image locationIcon;
        private MapIcon selectedIcon = null;
        private bool isDraggingIcon = false;
        private PointF iconDragOffset;

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

            // Panel chứa các buttons
            Panel buttonPanel = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 40
            };

            Button loadButton = new Button
            {
                Text = "Load Image",
                Width = 100,
                Height = 30,
                Left = 10,
                Top = 5
            };
            loadButton.Click += LoadButton_Click;
            buttonPanel.Controls.Add(loadButton);

            Button saveButton = new Button
            {
                Text = "Save Image",
                Width = 100,
                Height = 30,
                Left = 120,
                Top = 5
            };
            saveButton.Click += SaveButton_Click;
            buttonPanel.Controls.Add(saveButton);

            Button clearIconsButton = new Button
            {
                Text = "Clear Icons",
                Width = 100,
                Height = 30,
                Left = 230,
                Top = 5
            };
            clearIconsButton.Click += (s, e) => ClearMapIcons();
            buttonPanel.Controls.Add(clearIconsButton);

            // Thêm button test
            Button testIconButton = new Button
            {
                Text = "Test Icon",
                Width = 100,
                Height = 30,
                Left = 340,
                Top = 5
            };
            testIconButton.Click += (s, e) => AddTestIcon();
            buttonPanel.Controls.Add(testIconButton);

            this.Controls.Add(buttonPanel);

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
                    Point[] triangle =
                    {
                        new Point(16, 20),
                        new Point(12, 28),
                        new Point(20, 28)
                    };
                    g.FillPolygon(brush, triangle);
                }
                using (SolidBrush whiteBrush = new SolidBrush(Color.White))
                {
                    g.FillEllipse(whiteBrush, 12, 8, 8, 8);
                }
            }
        }

        // 1. Load ảnh và hiển thị lên pictureBox
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

        // 4. Save ảnh đã load + các icon đè lên ảnh
        private void SaveButton_Click(object sender, EventArgs e)
        {
            if (originalImage == null)
            {
                MessageBox.Show("No image loaded to save.");
                return;
            }

            using (SaveFileDialog saveFileDialog = new SaveFileDialog())
            {
                saveFileDialog.Filter = "PNG Files (*.png)|*.png|JPEG Files (*.jpg)|*.jpg|BMP Files (*.bmp)|*.bmp";
                saveFileDialog.DefaultExt = "png";
                
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        // Tạo ảnh mới với các icons được vẽ lên
                        Bitmap resultImage = new Bitmap(originalImage.Width, originalImage.Height);
                        using (Graphics g = Graphics.FromImage(resultImage))
                        {
                            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                            g.SmoothingMode = SmoothingMode.HighQuality;
                            
                            // Vẽ ảnh gốc
                            g.DrawImage(originalImage, 0, 0);
                            
                            // Vẽ tất cả các icons lên ảnh
                            foreach (var icon in mapIcons)
                            {
                                Image iconToDraw = icon.CustomIcon ?? locationIcon;
                                if (iconToDraw != null)
                                {
                                    float iconSize = icon.Size;
                                    float iconX = icon.ImagePosition.X - iconSize / 2;
                                    float iconY = icon.ImagePosition.Y - iconSize;
                                    
                                    RectangleF iconRect = new RectangleF(iconX, iconY, iconSize, iconSize);
                                    g.DrawImage(iconToDraw, iconRect);
                                }
                            }
                        }
                        
                        // Lưu ảnh
                        ImageFormat format = ImageFormat.Png;
                        string extension = Path.GetExtension(saveFileDialog.FileName).ToLower();
                        switch (extension)
                        {
                            case ".jpg":
                            case ".jpeg":
                                format = ImageFormat.Jpeg;
                                break;
                            case ".bmp":
                                format = ImageFormat.Bmp;
                                break;
                        }
                        
                        resultImage.Save(saveFileDialog.FileName, format);
                        resultImage.Dispose();
                        
                        MessageBox.Show("Image saved successfully!");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error saving image: {ex.Message}");
                    }
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
            if (zoom >= 1.0f) return 0;

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

            Rectangle clipRect = e.ClipRectangle;

            float imageLeft = (-offset.X) / effectiveZoom;
            float imageTop = (-offset.Y) / effectiveZoom;
            float imageRight = imageLeft + clipRect.Width / effectiveZoom;
            float imageBottom = imageTop + clipRect.Height / effectiveZoom;

            int startTileX = Math.Max(0, (int)Math.Floor(imageLeft / TILE_SIZE));
            int startTileY = Math.Max(0, (int)Math.Floor(imageTop / TILE_SIZE));
            int endTileX = Math.Min((pyramidSize.Width - 1) / TILE_SIZE, (int)Math.Ceiling(imageRight / TILE_SIZE));
            int endTileY = Math.Min((pyramidSize.Height - 1) / TILE_SIZE, (int)Math.Ceiling(imageBottom / TILE_SIZE));

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
            DrawMapIcons(g, zoomLevel);
        }

        private void DrawMapIcons(Graphics g, float currentZoomLevel)
        {
            Console.WriteLine($"DrawMapIcons called with {mapIcons.Count} icons, zoom: {currentZoomLevel}");
            
            foreach (var icon in mapIcons)
            {
                try
                {
                    // Chuyển đổi vị trí từ tọa độ ảnh sang tọa độ màn hình
                    float screenX = offset.X + icon.ImagePosition.X * currentZoomLevel;
                    float screenY = offset.Y + icon.ImagePosition.Y * currentZoomLevel;

                    float iconSize = 50; // Kích thước cố định

                    float iconX = screenX - iconSize / 2;
                    float iconY = screenY - iconSize;

                    Rectangle iconRect = new Rectangle((int)iconX, (int)iconY, (int)iconSize, (int)iconSize);
                    
                    Console.WriteLine($"Drawing icon '{icon.Label}' at screen position ({screenX}, {screenY}), rect: {iconRect}");
                    
                    // Luôn vẽ icon để test, không cần kiểm tra intersect
                    // if (iconRect.IntersectsWith(new Rectangle(0, 0, pictureBox.Width, pictureBox.Height)))
                    {
                        // Vẽ icon (custom hoặc default)
                        Image iconToDraw = icon.CustomIcon ?? locationIcon;
                        if (iconToDraw != null)
                        {
                            g.DrawImage(iconToDraw, iconRect);
                            Console.WriteLine($"Icon '{icon.Label}' drawn successfully");
                        }
                        else
                        {
                            // Vẽ hình chữ nhật màu đỏ nếu không có icon
                            g.FillRectangle(Brushes.Red, iconRect);
                            Console.WriteLine($"Drew red rectangle for icon '{icon.Label}'");
                        }

                        // Highlight icon được chọn
                        if (icon == selectedIcon)
                        {
                            using (Pen pen = new Pen(Color.Blue, 2))
                            {
                                g.DrawRectangle(pen, iconRect);
                            }
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

                                RectangleF textBackground = new RectangleF(textX - 2, textY, textSize.Width + 4, textSize.Height);
                                g.FillRectangle(backgroundBrush, textBackground);
                                g.DrawRectangle(Pens.Black, Rectangle.Round(textBackground));

                                g.DrawString(icon.Label, font, textBrush, textX, textY);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error drawing icon '{icon.Label}': {ex.Message}");
                }
            }
        }

        // Helper method để kiểm tra click trên icon
        private MapIcon GetIconAtPosition(PointF screenPosition)
        {
            foreach (var icon in mapIcons)
            {
                float screenX = offset.X + icon.ImagePosition.X * zoomLevel;
                float screenY = offset.Y + icon.ImagePosition.Y * zoomLevel;
                
                float iconSize = 50;
                float iconX = screenX - iconSize / 2;
                float iconY = screenY - iconSize;
                
                RectangleF iconRect = new RectangleF(iconX, iconY, iconSize, iconSize);
                if (iconRect.Contains(screenPosition))
                {
                    return icon;
                }
            }
            return null;
        }

        // Helper method để chuyển đổi tọa độ màn hình sang tọa độ ảnh
        private PointF ScreenToImageCoordinates(PointF screenPoint)
        {
            return new PointF(
                (screenPoint.X - offset.X) / zoomLevel,
                (screenPoint.Y - offset.Y) / zoomLevel
            );
        }

        private void PictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                // Kiểm tra xem có click trên icon nào không
                MapIcon clickedIcon = GetIconAtPosition(new PointF(e.X, e.Y));
                
                if (clickedIcon != null)
                {
                    selectedIcon = clickedIcon;
                    isDraggingIcon = true;
                    
                    // Tính offset từ vị trí click đến center của icon
                    float screenX = offset.X + clickedIcon.ImagePosition.X * zoomLevel;
                    float screenY = offset.Y + clickedIcon.ImagePosition.Y * zoomLevel;
                    iconDragOffset = new PointF(e.X - screenX, e.Y - screenY);
                    
                    pictureBox.Invalidate();
                }
                else
                {
                    selectedIcon = null;
                    isDragging = true;
                    lastMousePos = e.Location;
                    pictureBox.Invalidate();
                }
            }
        }

        private void PictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDraggingIcon && selectedIcon != null)
            {
                // Di chuyển icon
                PointF newScreenPos = new PointF(e.X - iconDragOffset.X, e.Y - iconDragOffset.Y);
                selectedIcon.ImagePosition = ScreenToImageCoordinates(newScreenPos);
                pictureBox.Invalidate();
            }
            else if (isDragging)
            {
                // Di chuyển map
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
                isDraggingIcon = false;
            }
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

        private void PictureBox_MouseClick(object sender, MouseEventArgs e)
        {
            if (originalImage == null || e.Button != MouseButtons.Right) return;

            PointF imagePosition = ScreenToImageCoordinates(new PointF(e.X, e.Y));

            if (imagePosition.X >= 0 && imagePosition.X < originalImageSize.Width && 
                imagePosition.Y >= 0 && imagePosition.Y < originalImageSize.Height)
            {
                AddMapIcon(imagePosition, $"Location {mapIcons.Count + 1}");
            }
        }

        public void AddMapIcon(PointF imagePosition, string label = "", Image customIcon = null, string fileName = "")
        {
            Console.WriteLine($"Adding map icon at position: {imagePosition}, label: {label}");
            
            mapIcons.Add(new MapIcon
            {
                ImagePosition = imagePosition,
                Label = label,
                IconColor = Color.Red,
                Size = 50,
                CustomIcon = customIcon,
                FileName = fileName
            });

            Console.WriteLine($"Total icons: {mapIcons.Count}");
            pictureBox.Invalidate();
        }

        // Thêm phương thức test để tạo icon mẫu
        public void AddTestIcon()
        {
            if (originalImage != null)
            {
                // Thêm icon test ở giữa ảnh
                PointF centerPosition = new PointF(originalImageSize.Width / 2, originalImageSize.Height / 2);
                AddMapIcon(centerPosition, "Test Icon", null, "test");
                Console.WriteLine("Test icon added");
            }
            else
            {
                Console.WriteLine("No image loaded - cannot add test icon");
            }
        }

        public void ClearMapIcons()
        {
            // Dispose custom icons trước khi clear
            foreach (var icon in mapIcons)
            {
                icon.CustomIcon?.Dispose();
            }
            mapIcons.Clear();
            selectedIcon = null;
            pictureBox.Invalidate();
        }

        // 2. Chọn các giai đoạn ở combobox và load các icon vào flowpanel
        private void comboBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            flowLayoutPanel1.Controls.Clear();
            
            if (comboBox1.SelectedItem == null) return;
            
            string selectItem = comboBox1.SelectedItem.ToString();
            
            // Đường dẫn folder chứa icons theo giai đoạn
            string folderPath = @"D:\Desktop\ThuatToan\MH3D";
            
            // Có thể thay đổi folder theo giai đoạn được chọn
            switch (selectItem)
            {
                case "Giai đoạn 1":
                    folderPath = @"D:\Desktop\ThuatToan\MH3D\GiaiDoan1";
                    break;
                case "Giai đoạn 2":
                    folderPath = @"D:\Desktop\ThuatToan\MH3D\GiaiDoan2";
                    break;
                case "Giai đoạn 3":
                    folderPath = @"D:\Desktop\ThuatToan\MH3D\GiaiDoan3";
                    break;
                case "Giai đoạn 4":
                    folderPath = @"D:\Desktop\ThuatToan\MH3D\GiaiDoan4";
                    break;
                default:
                    folderPath = @"D:\Desktop\ThuatToan\MH3D";
                    break;
            }
            
            // Fallback về folder mặc định nếu folder giai đoạn không tồn tại
            if (!Directory.Exists(folderPath))
            {
                folderPath = @"D:\Desktop\ThuatToan\MH3D";
            }
            
            if (Directory.Exists(folderPath))
            {
                var imageFiles = Directory.GetFiles(folderPath, "*.png")
                    .Concat(Directory.GetFiles(folderPath, "*.jpg"))
                    .Concat(Directory.GetFiles(folderPath, "*.jpeg"))
                    .Concat(Directory.GetFiles(folderPath, "*.bmp"))
                    .ToArray();
                    
                foreach (var file in imageFiles)
                {
                    try
                    {
                        PictureBox pb = new PictureBox();
                        pb.Image = Image.FromFile(file);
                        pb.SizeMode = PictureBoxSizeMode.Zoom;
                        pb.Width = 50;
                        pb.Height = 50;
                        pb.Tag = file; // Lưu đường dẫn file
                        pb.BorderStyle = BorderStyle.FixedSingle;

                        // Enable drag and drop
                        pb.MouseDown += (s, ev) =>
                        {
                            if (ev.Button == MouseButtons.Left)
                            {
                                PictureBox sourcePb = s as PictureBox;
                                if (sourcePb != null && sourcePb.Image != null)
                                {
                                    // Tạo data object với cả image và file path
                                    DataObject data = new DataObject();
                                    data.SetData("FilePath", sourcePb.Tag.ToString());
                                    data.SetData(DataFormats.Bitmap, sourcePb.Image);
                                    
                                    Console.WriteLine($"Starting drag operation for: {sourcePb.Tag}");
                                    sourcePb.DoDragDrop(data, DragDropEffects.Copy);
                                }
                            }
                        };

                        flowLayoutPanel1.Controls.Add(pb);
                        Console.WriteLine($"Added icon: {Path.GetFileName(file)}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error loading image {file}: {ex.Message}");
                    }
                }
            }
            else
            {
                // Tạo một vài icon mẫu nếu không tìm thấy folder
                CreateSampleIcons();
            }
        }

        // Tạo icons mẫu nếu không tìm thấy folder
        private void CreateSampleIcons()
        {
            for (int i = 1; i <= 5; i++)
            {
                PictureBox pb = new PictureBox();
                
                // Tạo icon mẫu
                Bitmap sampleIcon = new Bitmap(32, 32);
                using (Graphics g = Graphics.FromImage(sampleIcon))
                {
                    g.Clear(Color.LightBlue);
                    g.FillEllipse(new SolidBrush(Color.Blue), 4, 4, 24, 24);
                    g.DrawString(i.ToString(), new Font("Arial", 12), Brushes.White, 12, 8);
                }
                
                pb.Image = sampleIcon;
                pb.SizeMode = PictureBoxSizeMode.Zoom;
                pb.Width = 50;
                pb.Height = 50;
                pb.Tag = $"Sample_Icon_{i}";
                pb.BorderStyle = BorderStyle.FixedSingle;

                pb.MouseDown += (s, ev) =>
                {
                    if (ev.Button == MouseButtons.Left)
                    {
                        PictureBox sourcePb = s as PictureBox;
                        if (sourcePb != null && sourcePb.Image != null)
                        {
                            DataObject data = new DataObject();
                            data.SetData("FilePath", sourcePb.Tag.ToString());
                            data.SetData(DataFormats.Bitmap, sourcePb.Image);
                            
                            sourcePb.DoDragDrop(data, DragDropEffects.Copy);
                        }
                    }
                };

                flowLayoutPanel1.Controls.Add(pb);
            }
        }
        // 3. Drag and drop icons từ flowlayout lên ảnh
        private void BanDo_Load(object sender, EventArgs e)
        {
            // Thiết lập drag and drop cho pictureBox
            pictureBox.AllowDrop = true;

            pictureBox.DragEnter += (s, ev) =>
            {
                Console.WriteLine("DragEnter event triggered");
                // Kiểm tra xem data có chứa bitmap không
                if (ev.Data.GetDataPresent(DataFormats.Bitmap) || ev.Data.GetDataPresent("FilePath"))
                {
                    ev.Effect = DragDropEffects.Copy;
                    Console.WriteLine("Drag effect set to Copy");
                }
                else
                {
                    ev.Effect = DragDropEffects.None;
                    Console.WriteLine("Drag effect set to None");
                }
            };

            pictureBox.DragDrop += (s, ev) =>
            {
                Console.WriteLine("DragDrop event triggered");
                
                try
                {
                    if (originalImage == null)
                    {
                        MessageBox.Show("Please load an image first!");
                        return;
                    }

                    Image droppedImg = null;
                    string fileName = "Dropped Icon";

                    // Lấy dữ liệu từ drag operation
                    if (ev.Data.GetDataPresent(DataFormats.Bitmap))
                    {
                        droppedImg = (Image)ev.Data.GetData(DataFormats.Bitmap);
                        Console.WriteLine("Got bitmap data");
                    }

                    if (ev.Data.GetDataPresent("FilePath"))
                    {
                        string filePath = (string)ev.Data.GetData("FilePath");
                        fileName = Path.GetFileNameWithoutExtension(filePath);
                        Console.WriteLine($"Got file path: {filePath}");
                    }

                    if (droppedImg != null)
                    {
                        // Chuyển đổi vị trí drop sang tọa độ ảnh
                        Point dropPoint = pictureBox.PointToClient(new Point(ev.X, ev.Y));
                        PointF imagePosition = ScreenToImageCoordinates(new PointF(dropPoint.X, dropPoint.Y));
                        
                        Console.WriteLine($"Drop point: {dropPoint}, Image position: {imagePosition}");
                        
                        // Kiểm tra xem vị trí có hợp lệ không
                        if (imagePosition.X >= 0 && imagePosition.X < originalImageSize.Width && 
                            imagePosition.Y >= 0 && imagePosition.Y < originalImageSize.Height)
                        {
                            // Tạo copy của image để tránh conflict
                            Bitmap iconCopy = new Bitmap(droppedImg);
                            
                            // Thêm icon tại vị trí drop
                            AddMapIcon(imagePosition, fileName, iconCopy, fileName);
                            Console.WriteLine($"Icon added successfully at {imagePosition}");
                        }
                        else
                        {
                            Console.WriteLine("Drop position is outside image bounds");
                            MessageBox.Show("Please drop the icon on the image area.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("No image data found in drag operation");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error in DragDrop: {ex.Message}");
                    MessageBox.Show($"Error dropping icon: {ex.Message}");
                }
            };

            // Thiết lập các thuộc tính form
            this.AllowDrop = true;
            
            Console.WriteLine("BanDo_Load completed - Drag and drop initialized");
        }

        private Image GetTile(int level, int tileX, int tileY)
        {
            string tileKey = $"{level}_{tileX}_{tileY}";

            if (tileCache.ContainsKey(tileKey))
            {
                return tileCache[tileKey];
            }

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
            ClearMapIcons(); // Sẽ dispose các custom icons
            ClearCache();
            originalImage?.Dispose();
            base.OnFormClosing(e);
        }
    }
}
