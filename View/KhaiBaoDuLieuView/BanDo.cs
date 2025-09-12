using BoDoiApp.DataLayer;
using BoDoiApp.Helpers;
using ImageMagick;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BoDoiApp.View.KhaiBaoDuLieuView
{
    public partial class BanDo : Form
    {
        private const int TILE_SIZE = 512;
        private const int MAX_CACHE_TILES = 200;

        private Color giaiDoan;
        private bool isDich = false;

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

        // Large image handling fields - Added for >70MB support
        private string currentImageFilePath = null;
        private bool isLargeImage = false;
        private long imageFileSizeInMB = 0;

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

        // 1. Load ảnh và hiển thị lên pictureBox - Enhanced for large images (>70MB)
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
                            // Check file size first to determine loading strategy
                            var fileInfo = new FileInfo(openFileDialog.FileName);
                            imageFileSizeInMB = fileInfo.Length / (1024 * 1024);

                            this.Invoke(new Action(() =>
                            {
                                this.Text = $"BanDo - Loading {Path.GetFileName(openFileDialog.FileName)} ({imageFileSizeInMB} MB)...";
                            }));

                            if (imageFileSizeInMB > 70)
                            {
                                // Large image handling with Magick.NET
                                this.Invoke(new Action(() =>
                                {
                                    var result = MessageBox.Show(
                                        $"Large image detected: {imageFileSizeInMB} MB\n" +
                                        $"This will use Magick.NET optimized loading for better performance.\n" +
                                        $"Continue?",
                                        "Large Image Warning",
                                        MessageBoxButtons.YesNo,
                                        MessageBoxIcon.Information);

                                    if (result == DialogResult.No)
                                    {
                                        this.Cursor = Cursors.Default;
                                        this.Text = "BanDo";
                                        return;
                                    }
                                }));

                                LoadLargeImage(openFileDialog.FileName);

                            }
                            else
                            {
                                // Standard loading with Magick.NET for smaller images  
                                LoadStandardImage(openFileDialog.FileName);
                            }
                        }
                        catch (Exception ex)
                        {
                            this.Invoke(new Action(() =>
                            {
                                MessageBox.Show($"Error loading image: {ex.Message}");
                                this.Cursor = Cursors.Default;
                                this.Text = "BanDo";
                            }));
                        }
                    });
                }
            }
        }

        // Standard loading method for smaller images with enhanced Magick.NET
        private void LoadStandardImage(string filePath)
        {
            try
            {
                // Force garbage collection before loading
                GC.Collect();
                GC.WaitForPendingFinalizers();

                Image newImage = null;

                // For smaller images (<70MB), try Magick.NET first for enhanced quality
                try
                {
                    Console.WriteLine($"Loading standard image with Magick.NET enhancement: {Path.GetFileName(filePath)}");
                    //newImage = LoadImageWithMagickNet(filePath);
                    using (var stream = new FileStream(filePath,
                        FileMode.Open, FileAccess.Read, FileShare.Read, bufferSize: 1024 * 1024)) // 1MB buffer
                    {
                        newImage = Image.FromStream(stream);
                    }
                    Console.WriteLine("Magick.NET loading successful for standard image");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Magick.NET failed: {ex.Message}, falling back to standard GDI+ loading");

                    // Fallback to standard GDI+ loading with optimized buffer
                    using (var stream = new FileStream(filePath,
                        FileMode.Open, FileAccess.Read, FileShare.Read, bufferSize: 1024 * 1024)) // 1MB buffer
                    {
                        newImage = Image.FromStream(stream);
                    }
                    Console.WriteLine("Standard GDI+ loading successful");
                }

                this.Invoke(new Action(() =>
                {
                    CleanupPreviousImage();

                    originalImage = newImage;
                    originalImageSize = originalImage.Size;
                    currentImageFilePath = filePath;
                    isLargeImage = false;

                    CalculatePyramidLevels();

                    zoomLevel = 1.0f;
                    offset = new PointF(0, 0);
                    pictureBox.Invalidate();
                    this.Cursor = Cursors.Default;
                    this.Text = $"BanDo - {Path.GetFileName(filePath)} ({originalImageSize.Width}×{originalImageSize.Height}) [Standard + Enhanced]";
                }));
            }
            catch (OutOfMemoryException)
            {
                this.Invoke(new Action(() =>
                {
                    MessageBox.Show("Not enough memory for standard loading. Switching to large image mode...");
                }));
                LoadLargeImage(filePath);
            }
            catch (Exception ex)
            {
                this.Invoke(new Action(() =>
                {
                    MessageBox.Show($"Error loading standard image: {ex.Message}");
                    this.Cursor = Cursors.Default;
                    this.Text = "BanDo";
                }));
            }
        }

        // Enhanced loading for large images (>70MB) with advanced Magick.NET optimization
        private async void LoadLargeImage(string filePath)
        {
            try
            {
                await Task.Run(() =>
                {
                    this.Invoke(new Action(() =>
                    {
                        this.Text = $"BanDo - Processing large image with Magick.NET...";
                    }));

                    // Force aggressive garbage collection before loading large image
                    for (int i = 0; i < 3; i++)
                    {
                        GC.Collect();
                        GC.WaitForPendingFinalizers();
                    }

                    MagickImage magickImage = null;
                    Image downsampledImage = null;

                    try
                    {
                        this.Invoke(new Action(() =>
                        {
                            this.Text = $"BanDo - Reading large image metadata...";
                        }));

                        // First, read just the image info to get dimensions without loading full image
                        // Replace this line:
                        // using (var imageInfo = new MagickImageInfo(filePath))
                        // With the following (remove the using statement, just declare the variable):

                        MagickImageInfo imageInfo = new MagickImageInfo(filePath);
                        var originalWidth = (int)imageInfo.Width;
                        var originalHeight = (int)imageInfo.Height;
                        var maxDimension = Math.Max(originalWidth, originalHeight);

                        this.Invoke(new Action(() =>
                        {
                            this.Text = $"BanDo - Large image: {originalWidth}×{originalHeight} ({imageFileSizeInMB}MB)";
                        }));

                        // Determine optimal downsampling strategy based on image size
                        int targetMaxDimension;
                        if (imageFileSizeInMB > 500)
                        {
                            targetMaxDimension = 8192; // Ultra large images
                        }
                        else if (imageFileSizeInMB > 200)
                        {
                            targetMaxDimension = 12288; // Very large images
                        }
                        else if (imageFileSizeInMB > 100)
                        {
                            targetMaxDimension = 16384; // Large images
                        }
                        else
                        {
                            targetMaxDimension = 20480; // Medium large images
                        }

                        this.Invoke(new Action(() =>
                        {
                            this.Text = $"BanDo - Loading and downsampling to {targetMaxDimension}px...";
                        }));

                        // Load image with optimized settings for large files
                        magickImage = new MagickImage();

                        // Configure read settings for large images
                        var readSettings = new MagickReadSettings();

                        // For extremely large images, use define to optimize reading
                        if (imageFileSizeInMB > 200)
                        {
                            readSettings.SetDefine(MagickFormat.Jpeg, "size", $"{targetMaxDimension}x{targetMaxDimension}");
                            readSettings.SetDefine("jpeg:size", $"{targetMaxDimension}x{targetMaxDimension}");
                        }

                        // Read the image with settings
                        magickImage.Read(filePath, readSettings);

                        this.Invoke(new Action(() =>
                        {
                            this.Text = $"BanDo - Applying optimizations...";
                        }));

                        // Apply auto-orient to handle EXIF orientation
                        magickImage.AutoOrient();

                        // Strip metadata to save memory
                        magickImage.Strip();

                        // Apply quality enhancements for large images
                        magickImage.Enhance();
                        magickImage.Normalize();

                        // Downsample if necessary
                        if (maxDimension > targetMaxDimension)
                        {
                            this.Invoke(new Action(() =>
                            {
                                this.Text = $"BanDo - Downsampling from {maxDimension}px to {targetMaxDimension}px...";
                            }));

                            // Use optimal filter for large image downsampling
                            FilterType filter = FilterType.Lanczos;
                            double scaleRatio = (double)targetMaxDimension / maxDimension;

                            if (scaleRatio < 0.25)
                            {
                                filter = FilterType.Box; // Best for extreme downscaling
                            }
                            else if (scaleRatio < 0.5)
                            {
                                filter = FilterType.Mitchell; // Good for moderate downscaling
                            }

                            magickImage.FilterType = filter;
                            magickImage.Resize(new MagickGeometry($"{targetMaxDimension}x{targetMaxDimension}>"));

                            // Apply subtle sharpening after downsampling
                            if (scaleRatio < 0.7)
                            {
                                magickImage.Sharpen(0.5, 0.8);
                            }
                        }

                        this.Invoke(new Action(() =>
                        {
                            this.Text = $"BanDo - Converting to display format...";
                        }));

                        // Optimize format for memory usage
                        magickImage.Format = MagickFormat.Jpeg;
                        magickImage.Quality = 90; // High quality but compressed

                        // Convert to System.Drawing.Image
                        downsampledImage = ConvertMagickImageToImage(magickImage);

                        // Store original dimensions for proper coordinate mapping
                        var finalWidth = (int)magickImage.Width;
                        var finalHeight = (int)magickImage.Height;

                        this.Invoke(new Action(() =>
                        {
                            CleanupPreviousImage();

                            originalImage = downsampledImage;
                            originalImageSize = new Size(originalWidth, originalHeight); // Keep original size for coordinate mapping
                            currentImageFilePath = filePath;
                            isLargeImage = true;

                            // Calculate enhanced pyramid levels for large images
                            CalculateEnhancedPyramidLevels();

                            zoomLevel = 1.0f;
                            offset = new PointF(0, 0);
                            pictureBox.Invalidate();

                            this.Text = $"BanDo - {Path.GetFileName(filePath)} ({originalWidth}×{originalHeight} → {finalWidth}×{finalHeight}) [Large Image Mode]";
                            this.Cursor = Cursors.Default;

                            // Show completion message
                            var message = $"Large image loaded successfully!\n" +
                                        $"Original: {originalWidth}×{originalHeight} ({imageFileSizeInMB}MB)\n" +
                                        $"Display: {finalWidth}×{finalHeight}\n" +
                                        $"Memory optimization: {(double)imageFileSizeInMB / ((finalWidth * finalHeight * 3) / (1024.0 * 1024.0)):F1}x reduction";

                            MessageBox.Show(message, "Large Image Loaded", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }));
                    }
                    finally
                    {
                        // Clean up MagickImage resources
                        magickImage?.Dispose();
                    }
                });
            }
            catch (OutOfMemoryException)
            {
                this.Invoke(new Action(() =>
                {
                    MessageBox.Show(
                        $"Not enough memory to load this {imageFileSizeInMB}MB image.\n" +
                        $"Please try closing other applications or use a smaller image.",
                        "Memory Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    this.Cursor = Cursors.Default;
                    this.Text = "BanDo";
                }));
            }
            catch (Exception ex)
            {
                this.Invoke(new Action(() =>
                {
                    MessageBox.Show(
                        $"Error loading large image: {ex.Message}\n" +
                        $"File: {Path.GetFileName(filePath)} ({imageFileSizeInMB}MB)",
                        "Large Image Loading Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                    this.Cursor = Cursors.Default;
                    this.Text = "BanDo";
                }));
            }
        }

        // Enhanced pyramid calculation for large images
        private void CalculateEnhancedPyramidLevels()
        {
            pyramidSizes.Clear();
            maxPyramidLevel = 0;

            int width = originalImageSize.Width;
            int height = originalImageSize.Height;
            int level = 0;

            // For large images, create more pyramid levels for better performance
            int maxDimension = Math.Max(width, height);

            // Calculate optimal tile size based on image size
            int optimalTileSize = isLargeImage && maxDimension > 20000 ? 2048 : TILE_SIZE;

            while (width > optimalTileSize || height > optimalTileSize)
            {
                pyramidSizes[level] = new Size(width, height);

                // For very large images, use different scaling factors
                if (isLargeImage && maxDimension > 50000)
                {
                    // More aggressive downscaling for extremely large images
                    width = Math.Max(1, width * 2 / 3);
                    height = Math.Max(1, height * 2 / 3);
                }
                else
                {
                    // Standard halving
                    width /= 2;
                    height /= 2;
                }

                level++;

                // Prevent too many pyramid levels
                if (level > 15) break;
            }

            pyramidSizes[level] = new Size(width, height);
            maxPyramidLevel = level;

            Console.WriteLine($"Created {level + 1} pyramid levels for {(isLargeImage ? "large" : "standard")} image");
        }

        // Clean up previous image resources
        private void CleanupPreviousImage()
        {
            ClearCache();
            originalImage?.Dispose();
            originalImage = null;
            isLargeImage = false;
            currentImageFilePath = null;
            imageFileSizeInMB = 0;
        }

        // ===== MAGICK.NET ENHANCED METHODS =====

        // Load image using Magick.NET with optimized downscaling
        private static Image LoadImageWithMagickNet(string filePath, int maxDimension = 0)
        {
            try
            {
                Console.WriteLine($"LoadImageWithMagickNet called with maxDimension: {maxDimension}");

                using (var magickImage = new MagickImage(filePath))
                {
                    Console.WriteLine($"Original image dimensions: {magickImage.Width}x{magickImage.Height}");

                    // Apply auto-orient to handle EXIF orientation properly
                    //magickImage.AutoOrient();

                    // Enhanced quality improvements for all images
                    //magickImage.Enhance(); // Auto-enhance image quality
                    //magickImage.Normalize(); // Normalize contrast and brightness

                    // Advanced resizing for large images
                    if (maxDimension > 0)
                    {
                        var currentMax = Math.Max(magickImage.Width, magickImage.Height);
                        if (currentMax > maxDimension)
                        {
                            Console.WriteLine($"Resizing from {currentMax} to {maxDimension}");

                            // Calculate scale ratio for better quality control
                            double scaleRatio = (double)maxDimension / currentMax;

                            // Choose optimal filter based on scale ratio
                            FilterType filter;
                            if (scaleRatio < 0.3)
                            {
                                filter = FilterType.Box; // Best for extreme downscaling
                            }
                            else if (scaleRatio < 0.6)
                            {
                                filter = FilterType.Mitchell; // Good balance for moderate downscaling
                            }
                            else
                            {
                                filter = FilterType.Lanczos; // Highest quality for light downscaling
                            }

                            magickImage.FilterType = filter;

                            // Resize with maintain aspect ratio
                            magickImage.Resize(new MagickGeometry($"{maxDimension}x{maxDimension}>"));

                            // Apply adaptive sharpening based on scale ratio
                            if (scaleRatio < 0.7)
                            {
                                double sharpenRadius = scaleRatio < 0.5 ? 0.5 : 0.3;
                                double sharpenSigma = scaleRatio < 0.5 ? 0.8 : 0.5;
                                magickImage.Sharpen(sharpenRadius, sharpenSigma);
                            }

                            Console.WriteLine($"Resized to: {magickImage.Width}x{magickImage.Height}");
                        }
                    }

                    // Advanced optimization for memory efficiency
                    magickImage.Strip(); // Remove all metadata to save memory
                    magickImage.Quality = 95; // High quality but optimized

                    // For very large images, use JPEG format internally to save memory
                    if (maxDimension > 0 && Math.Max(magickImage.Width, magickImage.Height) > 10000)
                    {
                        magickImage.Format = MagickFormat.Jpeg;
                        Console.WriteLine("Using JPEG format for memory optimization");
                    }
                    else
                    {
                        magickImage.Format = MagickFormat.Png;
                    }

                    // Convert to System.Drawing.Image with optimized memory usage
                    return ConvertMagickImageToImage(magickImage);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Magick.NET loading failed: {ex.Message}");

                // Enhanced fallback with better error handling
                try
                {
                    Console.WriteLine("Attempting fallback to standard Image.FromFile");
                    return Image.FromFile(filePath);
                }
                catch (Exception fallbackEx)
                {
                    Console.WriteLine($"Fallback also failed: {fallbackEx.Message}");
                    throw new Exception($"Both Magick.NET and standard loading failed. Magick.NET: {ex.Message}, Standard: {fallbackEx.Message}");
                }
            }
        }

        // Enhanced convert MagickImage to System.Drawing.Image with memory optimization
        private static Image ConvertMagickImageToImage(MagickImage magickImage)
        {
            try
            {
                using (var memoryStream = new MemoryStream())
                {
                    // Use optimized format based on image characteristics
                    if (magickImage.HasAlpha)
                    {
                        magickImage.Format = MagickFormat.Png;
                    }
                    else
                    {
                        // For images without alpha, JPEG is more memory efficient
                        var imageSize = magickImage.Width * magickImage.Height;
                        if (imageSize > 50000000) // >50MP
                        {
                            magickImage.Format = MagickFormat.Jpeg;
                            magickImage.Quality = 95;
                        }
                        else
                        {
                            magickImage.Format = MagickFormat.Png;
                        }
                    }

                    magickImage.Write(memoryStream);
                    memoryStream.Position = 0;

                    return Image.FromStream(memoryStream);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error converting MagickImage to Image: {ex.Message}");
                throw;
            }
        }

        // Convert System.Drawing.Image to MagickImage
        private static MagickImage ConvertImageToMagickImage(Image image)
        {
            using (var memoryStream = new MemoryStream())
            {
                image.Save(memoryStream, ImageFormat.Png);
                memoryStream.Position = 0;
                return new MagickImage(memoryStream);
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

        // Enhanced pyramid level calculation for better sharpness
        private int GetBestPyramidLevel(float zoom)
        {
            // For large images, be more conservative with pyramid levels to maintain sharpness
            if (isLargeImage)
            {
                // Use original resolution more aggressively for large images
                if (zoom >= 0.8f) return 0;

                float targetScale = 1.0f / zoom;
                int level = 0;

                // Use smaller scaling thresholds for large images to maintain detail
                while (level < maxPyramidLevel && targetScale > 1.5f)
                {
                    targetScale /= (isLargeImage && originalImageSize.Width > 20000) ? 1.5f : 2.0f;
                    level++;
                }

                return Math.Min(level, maxPyramidLevel);
            }
            else
            {
                // Standard behavior for smaller images
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
            comboBox1.Items.Clear();
            comboBox1.Items.Add("Chon Loại Binh chủng");
            comboBox1.Items.AddRange(new FileHelper().ReadDirectoryString("C:\\Users\\NguyenBirain\\Documents\\Thai Ha\\Images\\Nhóm KHQS\\"));
            comboBox1.SelectedIndex = 0;
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
                        Bitmap originImage = (Bitmap)ev.Data.GetData(DataFormats.Bitmap);
                        Color background = Color.FromArgb(200, 230, 250);
                        // Thay đổi background thành màu đỏ
                        // Giả sử background là màu trắng với tolerance 50
                        Bitmap redBackgroundBitmap = new ImageProcess().ChangeBackGroundColor(
                            originImage,
                            background,    // Màu background cần thay đổi
                            50,            // Tolerance
                            giaiDoan      // Màu mới (đỏ)
                        );
                        droppedImg = redBackgroundBitmap;
                    }

                    if (ev.Data.GetDataPresent("FilePath"))
                    {
                        string filePath = (string)ev.Data.GetData("FilePath");
                        fileName = Path.GetFileNameWithoutExtension(filePath);
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

        // Enhanced tile generation with Magick.NET integration
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

            // Try Magick.NET for high-quality downscaling first
            if (scale < 0.8f && tileWidth < sourceWidth && tileHeight < sourceHeight)
            {
                try
                {
                    return GenerateTileWithMagickNet(sourceX, sourceY, sourceWidth, sourceHeight, tileWidth, tileHeight);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Magick.NET tile generation failed: {ex.Message}, falling back to GDI+");
                }
            }

            // Fallback to standard GDI+ approach
            return GenerateTileWithGDI(sourceX, sourceY, sourceWidth, sourceHeight, tileWidth, tileHeight);
        }

        // Generate tile using Magick.NET for superior quality downscaling
        private Image GenerateTileWithMagickNet(int sourceX, int sourceY, int sourceWidth, int sourceHeight, int tileWidth, int tileHeight)
        {
            try
            {
                using (var magickImage = ConvertImageToMagickImage(originalImage))
                {
                    // Crop to the source rectangle
                    var cropGeometry = new MagickGeometry(sourceX, sourceY, (uint)sourceWidth, (uint)sourceHeight);
                    magickImage.Crop(cropGeometry);

                    // Apply high-quality downscaling
                    if (tileWidth != sourceWidth || tileHeight != sourceHeight)
                    {
                        // Choose appropriate filter based on scaling ratio
                        FilterType filter = FilterType.Lanczos;
                        float scaleRatio = Math.Min((float)tileWidth / sourceWidth, (float)tileHeight / sourceHeight);

                        if (scaleRatio < 0.25f)
                        {
                            // For very small tiles, use Box filter to prevent aliasing
                            filter = FilterType.Box;
                        }
                        else if (scaleRatio < 0.5f)
                        {
                            // For medium downscaling, use Mitchell for good quality/performance balance
                            filter = FilterType.Mitchell;
                        }

                        magickImage.FilterType = filter;
                        magickImage.Resize((uint)tileWidth, (uint)tileHeight);

                        // Apply subtle sharpening for downscaled tiles
                        if (scaleRatio < 0.7f)
                        {
                            magickImage.Sharpen(0, 0.5);
                        }
                    }

                    // Convert back to System.Drawing.Image
                    return ConvertMagickImageToImage(magickImage);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Magick.NET tile processing failed: {ex.Message}");
                throw;
            }
        }

        // Generate tile using standard GDI+ (fallback method)
        private Image GenerateTileWithGDI(int sourceX, int sourceY, int sourceWidth, int sourceHeight, int tileWidth, int tileHeight)
        {
            Bitmap tile = null;
            Graphics g = null;

            try
            {
                // Use optimized pixel format for large images
                PixelFormat pixelFormat = isLargeImage ? PixelFormat.Format24bppRgb : PixelFormat.Format32bppArgb;
                tile = new Bitmap(tileWidth, tileHeight, pixelFormat);

                g = Graphics.FromImage(tile);

                // Enhanced graphics settings for better quality
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.SmoothingMode = SmoothingMode.HighQuality;
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                g.CompositingQuality = CompositingQuality.HighQuality;

                // For large images, use different rendering approach
                if (isLargeImage && sourceWidth > tileWidth * 2)
                {
                    // For significant downscaling, use highest quality
                    g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                }

                Rectangle destRect = new Rectangle(0, 0, tileWidth, tileHeight);
                Rectangle srcRect = new Rectangle(sourceX, sourceY, sourceWidth, sourceHeight);

                g.DrawImage(originalImage, destRect, srcRect, GraphicsUnit.Pixel);

                return tile;
            }
            catch (OutOfMemoryException)
            {
                Console.WriteLine($"Out of memory generating tile ({sourceX}, {sourceY})");

                // Clean up and try with reduced quality
                g?.Dispose();
                tile?.Dispose();

                // Force garbage collection
                GC.Collect();
                GC.WaitForPendingFinalizers();

                // Try again with smaller tile size for large images
                if (isLargeImage)
                {
                    try
                    {
                        int reducedTileWidth = Math.Min(512, tileWidth);
                        int reducedTileHeight = Math.Min(512, tileHeight);

                        tile = new Bitmap(reducedTileWidth, reducedTileHeight, PixelFormat.Format24bppRgb);
                        g = Graphics.FromImage(tile);
                        g.InterpolationMode = InterpolationMode.HighQualityBicubic;

                        Rectangle destRect = new Rectangle(0, 0, reducedTileWidth, reducedTileHeight);
                        Rectangle srcRect = new Rectangle(sourceX, sourceY, sourceWidth, sourceHeight);

                        g.DrawImage(originalImage, destRect, srcRect, GraphicsUnit.Pixel);

                        return tile;
                    }
                    catch
                    {
                        // Complete failure - return null
                        g?.Dispose();
                        tile?.Dispose();
                        return null;
                    }
                    finally
                    {
                        g?.Dispose();
                    }
                }

                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error generating tile: {ex.Message}");
                g?.Dispose();
                tile?.Dispose();
                return null;
            }
            finally
            {
                g?.Dispose();
            }
        }

        // Enhanced cache management for large images
        private void CacheTile(string tileKey, Image tile)
        {
            // Adjust cache size based on image size and available memory
            int maxCacheForCurrentImage;

            if (isLargeImage)
            {
                // Reduce cache size for large images to prevent memory issues
                if (imageFileSizeInMB > 200)
                {
                    maxCacheForCurrentImage = MAX_CACHE_TILES / 8; // Very conservative for huge images
                }
                else if (imageFileSizeInMB > 100)
                {
                    maxCacheForCurrentImage = MAX_CACHE_TILES / 4; // Conservative for large images
                }
                else
                {
                    maxCacheForCurrentImage = MAX_CACHE_TILES / 2; // Moderate for medium large images
                }
            }
            else
            {
                maxCacheForCurrentImage = MAX_CACHE_TILES;
            }

            // Clean up old tiles if cache is full
            while (tileCache.Count >= maxCacheForCurrentImage && tileCacheOrder.Count > 0)
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

            // For large images, periodically force garbage collection
            if (isLargeImage && tileCache.Count % 10 == 0)
            {
                GC.Collect();
            }
        }

        // Enhanced cache clearing with better memory management
        private void ClearCache()
        {
            foreach (var tile in tileCache.Values)
            {
                tile?.Dispose();
            }
            tileCache.Clear();
            tileCacheOrder.Clear();

            // Force garbage collection for large images
            if (isLargeImage)
            {
                for (int i = 0; i < 2; i++)
                {
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                }
            }
        }

        private void ClampOffset()
        {
            if (originalImage == null) return;

            float scaledWidth = originalImageSize.Width * zoomLevel;
            float scaledHeight = originalImageSize.Height * zoomLevel;

            offset.X = Math.Min(0, Math.Max(offset.X, pictureBox.Width - scaledWidth));
            offset.Y = Math.Min(0, Math.Max(offset.Y, pictureBox.Height - scaledHeight));
        }

        // Enhanced cleanup for large images
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            locationIcon?.Dispose();
            ClearMapIcons(); // Sẽ dispose các custom icons
            CleanupPreviousImage(); // Clean up both standard and large image resources
            base.OnFormClosing(e);
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            var value = comboBox2.SelectedItem.ToString();
            switch (value)
            {
                case "Giai đoạn 1":
                    giaiDoan = Color.Red;
                    break;
                case "Giai đoạn 2":
                    giaiDoan = Color.Orange;
                    break;
                case "Giai đoạn 3":
                    giaiDoan = Color.Yellow;
                    break;
                case "Giai đoạn 4":
                    giaiDoan = Color.Green;
                    break;
                case "Giai đoạn 5":
                    giaiDoan = Color.Blue;
                    break;
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
        
            flowLayoutPanel1.Controls.Clear();

            if (comboBox1.SelectedItem == null) return;

            string selectItem = comboBox1.SelectedItem.ToString();

            string folderPath = Path.Combine("C:\\Users\\NguyenBirain\\Documents\\Thai Ha\\Images\\Nhóm KHQS", selectItem);

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
                    Bitmap originalBitmap = new Bitmap(file);

                    try
                    {
                        PictureBox pb = new PictureBox();
                        pb.Image = originalBitmap;
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
    
    }

}
