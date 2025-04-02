using System.Text.Json;
using SkiaSharp;

namespace MenuGenerator {
    class MenuGenerator {
        public class Menu {
            public List<Category>? Categories { get; set; }
        }

        public class Category {
            public string? Name { get; set; }
            public List<MenuItem>? Items { get; set; }
        }

        public class MenuItem {
            public string? Name { get; set; }
            public string? Cost { get; set; }
            public string? Description { get; set; }
        }

        public static void Main(string[] args) {
            string json = File.ReadAllText("menu.json");
            var options = new JsonSerializerOptions {
                PropertyNameCaseInsensitive = true
            };
            Menu menu = JsonSerializer.Deserialize<Menu>(json, options)!;
            
            // Define image dimensions
            int width = 1920;
            int height = 1080;

            // Create a new image surface
            using SKBitmap bitmap = new SKBitmap(width, height);
            using SKCanvas canvas = new SKCanvas(bitmap);

            // Set background color
            canvas.DrawColor(SKColors.White);

            using SKPaint paint = new SKPaint
            {
                Color = SKColors.Orange,
                TextSize = 60,
                Typeface = SKTypeface.FromFamilyName("Arial", SKFontStyle.Bold)
            };
            canvas.DrawText("menu", 50, 100, paint);

            int yPosition = 200;
            foreach (var category in menu.Categories)
            {
                // Draw category name
                paint.Color = SKColors.Black;
                paint.TextSize = 40;
                canvas.DrawText(category.Name, 50, yPosition, paint);
                yPosition += 50;

                foreach (var item in category.Items)
                {
                    // Draw item name
                    paint.TextSize = 30;
                    canvas.DrawText(item.Name, 70, yPosition, paint);

                    // Draw price
                    paint.TextSize = 30;
                    canvas.DrawText($"${item.Cost:F2}", 950, yPosition, paint);
                    yPosition += 40;

                    // Draw description
                    paint.TextSize = 20;
                    paint.Color = SKColors.Gray;
                    canvas.DrawText(item.Description, 70, yPosition, paint);
                    yPosition += 40;
                }
            }

            // Save as PNG
            using SKImage image = SKImage.FromBitmap(bitmap);
            using SKData data = image.Encode(SKEncodedImageFormat.Png, 100);
            File.WriteAllBytes("menu.png", data.ToArray());
        }
    }
}