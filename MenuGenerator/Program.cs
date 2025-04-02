using System.Text.Json;
using SkiaSharp;

namespace MenuGenerator {
    class MenuGenerator {
        public class Menu {
            public List<Category>? Categories { get; set; }
        }

        public class Category {
            public string? Name { get; set; }
            public string? Type { get; set; }
            public List<MenuItem>? Items { get; set; }
        }

        public class MenuItem {
            public string? Name { get; set; }
            public string? Cost { get; set; }
            public string? Description { get; set; }
        }

        public static void Main(string[] args) {
            SKTypeface Baron = SKTypeface.FromFile("fonts/Baron Neue.otf");
            SKTypeface Kollektif = SKTypeface.FromFile("fonts/Kollektif.ttf");
            SKTypeface Sceageus = SKTypeface.FromFile("fonts/Sceageus.otf");

            string json = File.ReadAllText("menu.json");
            var options = new JsonSerializerOptions {
                PropertyNameCaseInsensitive = true
            };
            Menu menu = JsonSerializer.Deserialize<Menu>(json, options)!;
            
            // Define image dimensions
            int width = 5280;
            int height = 3000;

            // Create a new image surface
            using SKBitmap bitmap = new SKBitmap(width, height);
            using SKCanvas canvas = new SKCanvas(bitmap);

            // Set background color
            canvas.DrawColor(SKColors.White);

            using SKPaint paint = new SKPaint
            {
                Color = SKColors.Orange,
                TextSize = 400,
                Typeface = Sceageus
            };
            canvas.DrawText("menu", 525, 400, paint);

            using SKPaint black = new SKPaint {
                Color = SKColors.Black
            };
            
            canvas.DrawRect(150, 300, 250, 15, black);
            canvas.DrawRect(1800, 300, 5000, 15, black);
            canvas.DrawRect(150, 465, 1920, 1330, black);

            int yPosition = 200;
            foreach (var category in menu.Categories)
            {
                if (category.Type != "Food") {
                    continue;
                }
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
                    canvas.DrawText($"${item.Cost:F2}", 600, yPosition, paint);
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