using System.Formats.Asn1;
using System.Text.Json;
using Microsoft.VisualBasic;
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
            SKTypeface BaronBold = SKTypeface.FromFile("fonts/Baron Neue Bold.otf");
            SKTypeface Kollektif = SKTypeface.FromFile("fonts/Kollektif.ttf");
            SKTypeface KollektifBold = SKTypeface.FromFile("fonts/Kollektif-Bold.ttf");
            SKTypeface KollektifBI = SKTypeface.FromFile("fonts/Kollektif-BoldItalic.ttf");            
            SKTypeface NanumGothicBold = SKTypeface.FromFile("fonts/NanumGothic-Bold.woff");            
            SKTypeface Sceageus = SKTypeface.FromFile("fonts/Sceageus.otf");

            string json = File.ReadAllText("menu.json");
            var options = new JsonSerializerOptions {
                PropertyNameCaseInsensitive = true
            };
            Menu menu = JsonSerializer.Deserialize<Menu>(json, options)!;
            
            // Define image dimensions
            int width = 5280;
            int height = 3000;
            string text = "";

            // Create a new image surface
            using SKBitmap bitmap = new SKBitmap(width, height);
            using SKCanvas canvas = new SKCanvas(bitmap);

            // Set background color
            canvas.DrawColor(SKColors.White);

            using SKPaint sceageusPaint = new SKPaint
            {
                Color = new SKColor(247, 148, 29, 255),
                TextSize = 400,
                Typeface = Sceageus,
                IsAntialias = true
            };

            using SKPaint kollektifPaint = new SKPaint
            {
                Color = SKColors.Black,
                TextSize = 50,
                Typeface = Kollektif,
                IsAntialias = true
            };

            using SKPaint kollektifBoldPaint = new SKPaint
            {
                Color = SKColors.Black,
                TextSize = 50,
                Typeface = KollektifBold,
                IsAntialias = true
            };
            
            using SKPaint kollektifBIPaint = new SKPaint
            {
                Color = SKColors.Black,
                TextSize = 50,
                Typeface = KollektifBI,
                IsAntialias = true
            };

            using SKPaint baronPaint = new SKPaint
            {
                Color = SKColors.Black,
                TextSize = 50,
                Typeface = Baron,
                IsAntialias = true
            };

            using SKPaint baronBoldPaint = new SKPaint
            {
                Color = SKColors.Black,
                TextSize = 50,
                Typeface = BaronBold,
                IsAntialias = true
            };

            using SKPaint nanumGothicBoldPaint = new SKPaint
            {
                Color = SKColors.Black,
                TextSize = 50,
                Typeface = NanumGothicBold,
                IsAntialias = true
            };

            canvas.DrawText("menu", 525, 400, sceageusPaint);

            using SKPaint black = new SKPaint {
                Color = SKColors.Black
            };

            using SKPaint orange = new SKPaint {
                Color = new SKColor(214, 190, 166)
            };

            using SKPaint brown = new SKPaint {
                Color = new SKColor(214, 190, 166)
            };
            
            canvas.DrawRect(150, 300, 250, 15, black);
            canvas.DrawRect(1800, 300, 5000, 15, black);
            SKRect boxRect = new SKRect(150, 465, 2100, 1800);
            canvas.DrawRect(boxRect, brown);

            float x = 0;
            float y = 0;
            float padding = 0;

            foreach(Category category in menu.Categories) {
                if (category.Name == "Dosiiroc Boxes") {
                    x = 200;
                    y = 600;
                    padding = 95;

                    kollektifBoldPaint.TextSize = 150;
                    canvas.DrawText("DOSIIROC BOXES", x, y, kollektifBoldPaint);

                    kollektifBoldPaint.TextSize = 75;
                    canvas.DrawText("(doe-she-roc)", x + 1295, y - 15, kollektifBoldPaint);

                    text = "A Korean meal served conveniently in a DosiiRoc Box! Includes white rice, your choice of protein, japchae noodles, two pieces of yachae jeon with chef's choice of banchan and house made cucumber kimchi.";

                    kollektifBoldPaint.TextSize = 60;
                    y = drawTextWrap(canvas, x, y + 100, 2100, kollektifBoldPaint, text, 25);
                    
                    kollektifBoldPaint.TextSize = 65;
                    kollektifBIPaint.TextSize = 65;
                    kollektifPaint.TextSize = 50;
                    nanumGothicBoldPaint.TextSize = 65;
                    x += 30;
                    y += padding;
                    canvas.DrawText("PROTEIN CHOICES", x, y, kollektifBIPaint);

                    y += padding;
                    drawWithKorean(canvas, x, y, kollektifBoldPaint, nanumGothicBoldPaint, "- BEEF BULGOGI", "불고기");
                    canvas.DrawText("$" + category.Items[0].Cost, x + 1650, y, kollektifBoldPaint); 

                    y += padding;
                    drawWithKorean(canvas, x, y, kollektifBoldPaint, nanumGothicBoldPaint, "- SPICY PORK BULGOGI", "돼지 불고기");
                    canvas.DrawText("$" + category.Items[1].Cost, x + 1650, y, kollektifBoldPaint); 

                    y += padding;
                    drawWithKorean(canvas, x, y, kollektifBoldPaint, nanumGothicBoldPaint, "- CHICKEN BULGOGI", "치킨 불고기");
                    canvas.DrawText("$" + category.Items[2].Cost, x + 1650, y, kollektifBoldPaint); 

                    y += padding;
                    drawWithKorean(canvas, x, y, kollektifBoldPaint, nanumGothicBoldPaint, "- FRIED TOFU", "두부 튀김");
                    canvas.DrawText("$" + category.Items[3].Cost, x + 1650, y, kollektifBoldPaint); 

                    y += padding - 15;
                    canvas.DrawText("-> sauce: bulgogi / dosiiroc secret sauce", x + 48, y, kollektifPaint);

                    y += padding;
                    drawWithKorean(canvas, x, y, kollektifBoldPaint, nanumGothicBoldPaint, "- KOREAN BONELESS FRIED CHICKEN", "순살 치킨");
                    canvas.DrawText("$" + category.Items[4].Cost, x + 1650, y, kollektifBoldPaint); 

                    y += padding - 15;
                    canvas.DrawText("-> sauce: honey garlic / dosiiroc secret sauce", x + 48, y, kollektifPaint);

                    y += padding;
                    canvas.DrawText("add a fried egg for ", boxRect.MidX - kollektifBoldPaint.MeasureText("add a fried egg for ")/2, y, kollektifBoldPaint);
                }
                else if (category.Name == "Appetizers") {
                    x = 159;
                    y = 2000;
                    
                    kollektifBoldPaint.TextSize = 150;

                    canvas.DrawText("APPETIZERS", x, y, kollektifBoldPaint);
                }
            }

            // int yPosition = 200;
            // foreach (var category in menu.Categories)
            // {
            //     if (category.Type != "Food") {
            //         continue;
            //     }
            //     // Draw category name
            //     paint.Color = SKColors.Black;
            //     paint.TextSize = 40;
            //     canvas.DrawText(category.Name, 50, yPosition, paint);
            //     yPosition += 50;

            //     foreach (var item in category.Items)
            //     {
            //         // Draw item name
            //         paint.TextSize = 30;
            //         canvas.DrawText(item.Name, 70, yPosition, paint);

            //         // Draw price
            //         paint.TextSize = 30;
            //         canvas.DrawText($"${item.Cost:F2}", 600, yPosition, paint);
            //         yPosition += 40;

            //         // Draw description
            //         paint.TextSize = 20;
            //         paint.Color = SKColors.Gray;
            //         canvas.DrawText(item.Description, 70, yPosition, paint);
            //         yPosition += 40;
            //     }
            // }

            // Save as PNG
            using SKImage image = SKImage.FromBitmap(bitmap);
            using SKData data = image.Encode(SKEncodedImageFormat.Png, 100);
            File.WriteAllBytes("menu.png", data.ToArray());
        }

        // returns the y value that the final word is on
        public static float drawTextWrap(SKCanvas canvas, float startingx, float y, float boundaryx, SKPaint paint, string text, float padding = 0) {
            string[] words = text.Split(" ");
            float x = startingx;
            float word_length = 0; 

            foreach (string word in words) {
                word_length = paint.MeasureText(word + " ");

                x += word_length;
                if (x >= boundaryx) {
                    x = startingx + word_length;
                    y += paint.TextSize + padding;
                }
                canvas.DrawText(word + " ", x - word_length, y, paint);
            }

            return y;
        }

        public static void drawWithKorean(SKCanvas canvas, float x, float y, SKPaint paint, SKPaint koreanPaint, string text, string korean) {
            canvas.DrawText(text, x, y, paint);
            canvas.DrawText(korean, x + paint.MeasureText(text + " "), y, koreanPaint);
        }
    }
}