using System.Formats.Asn1;
using System.Reflection.Metadata;
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
            public string? Korean { get; set; }
        }

        public static void Main(string[] args) {

            const int CATEGORY_FONT_SIZE = 115;
            const int ITEM_NAME_FONT_SIZE = 60;
            const int DESCRIPTION_FONT_SIZE = 50;
            const int PADDING_SIZE = 95;

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
            string text;

            // Create a new image surface
            SKBitmap bitmap = new SKBitmap(width, height);
            SKCanvas canvas = new SKCanvas(bitmap);

            // Set background color
            canvas.DrawColor(new SKColor(255,253,249));

            SKPaint sceageusPaint = new SKPaint
            {
                Color = new SKColor(247, 148, 29, 255),
                TextSize = 400,
                Typeface = Sceageus,
                IsAntialias = true
            };

            SKPaint kollektifPaint = new SKPaint
            {
                Color = SKColors.Black,
                TextSize = 50,
                Typeface = Kollektif,
                IsAntialias = true
            };

            SKPaint kollektifBoldPaint = new SKPaint
            {
                Color = SKColors.Black,
                TextSize = 50,
                Typeface = KollektifBold,
                IsAntialias = true
            };
            
            SKPaint kollektifBIPaint = new SKPaint
            {
                Color = SKColors.Black,
                TextSize = 50,
                Typeface = KollektifBI,
                IsAntialias = true
            };

            SKPaint baronPaint = new SKPaint
            {
                Color = SKColors.Black,
                TextSize = 50,
                Typeface = Baron,
                IsAntialias = true
            };

            SKPaint baronBoldPaint = new SKPaint
            {
                Color = SKColors.Black,
                TextSize = 50,
                Typeface = BaronBold,
                IsAntialias = true
            };

            SKPaint nanumGothicBoldPaint = new SKPaint
            {
                Color = SKColors.Black,
                TextSize = 50,
                Typeface = NanumGothicBold,
                IsAntialias = true
            };

            SKPaint black = new SKPaint {
                Color = SKColors.Black
            };

            SKPaint orange = new SKPaint {
                Color = new SKColor(247, 148, 29)
            };

            SKPaint brown = new SKPaint {
                Color = new SKColor(214, 190, 166)
            };

            SKPaint offwhite = new SKPaint {
                Color = new SKColor(255,253,249)
            };

            kollektifBoldPaint.TextSize = DESCRIPTION_FONT_SIZE;
            
            canvas.DrawRect(150, 300, 250, 15, black);
            canvas.DrawRect(1800, 300, width-150-1800, 15, black);
            canvas.DrawRect(width-150-450, height-150, 450, 15, black);
            canvas.DrawRect(150, height-150, width-150-1600, 15, black);
            SKRect boxRect = new SKRect(150, 465, 2250, 1800);
            canvas.DrawRect(boxRect, brown);
            canvas.DrawText("menu", 525, 400, sceageusPaint);
            canvas.DrawText("dosiiroccafe.com | @dosiiroccafe", (width-1600)+((width-150-450) - (width-1600))/2 - kollektifBoldPaint.MeasureText("dosiiroccafe.com | @dosiiroccafe")/2, height-150+kollektifBoldPaint.TextSize/2, kollektifBoldPaint);
            canvas.DrawText("If you have a food allergy or any dietary restriction/preference, please notfiy us. We will do our best to accomodate you.", 150, 2950, kollektifBoldPaint);

           SKBitmap logo = SKBitmap.Decode("images/face logo.png");
            
            SKImageInfo info = new SKImageInfo(370, 414, SKColorType.Argb4444);
            logo = logo.Resize(info, SKFilterQuality.High);

            SKColor logoColor = new SKColor(255, 227, 194);
            
            canvas.DrawCircle(width-387, 300, 475/2, black);

            using (SKPaint paint = new SKPaint() { Color = logoColor }) {
                canvas.DrawCircle(width-387, 300, 450/2, paint);
            };

            canvas.DrawBitmap(logo, width-387-info.Width/2, 300-info.Height/2);

            float x = 0;
            float y = 0;
            float padding = 0;
            int index = 0;
            int startingx = 0;
            int startingy = 0;

            foreach(Category category in menu.Categories) {
                if (category.Name == "Dosiiroc Boxes") {
                    x = 200;
                    y = 600;
                    padding = PADDING_SIZE;

                    kollektifBoldPaint.TextSize = CATEGORY_FONT_SIZE;
                    canvas.DrawText("DOSIIROC BOXES", x, y, kollektifBoldPaint);
                    float temp_len = kollektifBoldPaint.MeasureText("DOSIIROC BOXES");
                    kollektifBoldPaint.TextSize = 75;
                    canvas.DrawText("(doe-she-roc)", x + temp_len + 10, y - 15, kollektifBoldPaint);

                    text = "A Korean meal served conveniently in a DosiiRoc Box! Includes white rice, your choice of protein, japchae noodles, two pieces of yachae jeon with chef's choice of banchan and house made cucumber kimchi.";

                    kollektifBoldPaint.TextSize = DESCRIPTION_FONT_SIZE + 15;
                    y = drawTextWrap(canvas, x, y + 100, 2250, kollektifBoldPaint, text);
                    
                    kollektifBoldPaint.TextSize = ITEM_NAME_FONT_SIZE;
                    kollektifBIPaint.TextSize = ITEM_NAME_FONT_SIZE;
                    kollektifPaint.TextSize = DESCRIPTION_FONT_SIZE;
                    nanumGothicBoldPaint.TextSize = ITEM_NAME_FONT_SIZE;
                    x += 30;
                    y += padding;
                    canvas.DrawText("PROTEIN CHOICES:", x, y, kollektifBIPaint);
                    index = 0;
                    foreach(MenuItem item in category.Items) {
                        MenuItem temp = item;
                        temp.Name = temp.Name.Remove(temp.Name.LastIndexOf(" Dosiiroc"));
                        y += padding;
                        drawWithKorean(canvas, x, y, kollektifBoldPaint, nanumGothicBoldPaint, "- " + temp.Name.ToUpper(), item.Korean);
                        canvas.DrawText("$" + item.Cost, x + 1750, y, kollektifBoldPaint); 
                        index++;

                        if(temp.Name == "Fried Tofu") {
                            y += padding - 30;
                            canvas.DrawText("-> sauce: bulgogi / dosiiroc secret sauces", x + 30, y, kollektifPaint);
                        }
                        else if (temp.Name == "Korean Boneless Fried Chicken") {
                            y += padding - 30;
                            canvas.DrawText("-> sauce: honey garlic / dosiiroc secret sauces", x + 30, y, kollektifPaint);
                        }
                    }

                    y += padding;
                    canvas.DrawText("add a fried egg for $2.49", boxRect.MidX - kollektifBoldPaint.MeasureText("add a fried egg for $2.49")/2, y, kollektifBoldPaint);
                }
                else if (category.Name == "Appetizers") {
                    x = 159;
                    y = 1950;
                    
                    kollektifBoldPaint.TextSize = CATEGORY_FONT_SIZE;
                    kollektifPaint.TextSize = DESCRIPTION_FONT_SIZE;

                    canvas.DrawText("APPETIZERS", x, y, kollektifBoldPaint);

                    kollektifBoldPaint.TextSize = ITEM_NAME_FONT_SIZE;
                    nanumGothicBoldPaint.TextSize = ITEM_NAME_FONT_SIZE;
                    y += kollektifBoldPaint.TextSize + 30;
                    index = 0;
                    foreach(MenuItem item in category.Items) {
                        drawWithKorean(canvas, x, y, kollektifBoldPaint, nanumGothicBoldPaint, item.Name.ToUpper(), item.Korean);
                        canvas.DrawText('$' + item.Cost, x + 850, y, kollektifBoldPaint);
                        
                        y += kollektifPaint.TextSize + 20;
                        y = drawTextWrap(canvas, x, y, x + 800, kollektifPaint, item.Description.ToLower());
                        y += kollektifBoldPaint.TextSize + 30;
                        index++;
                        if (index == 3) {
                            x += 1050;
                            y = 1950 + kollektifBoldPaint.TextSize + 30;
                        }
                    }

                    

                    canvas.DrawRect(x, y, 1050, 130, orange);
                    canvas.DrawRect(x+15, y+15, 1050-30, 130-30, offwhite);
                    canvas.DrawText("extra rice: +$1.99", x + (1020/2 - kollektifBoldPaint.MeasureText("Extra rice: $1.99")/2), y + (100 + kollektifBoldPaint.TextSize)/2, kollektifBoldPaint);
                }
                else if (category.Name == "Noodles") {
                    startingx = 2350;
                    x = 2350;
                    y = 600;
                    startingy = 600;
                    padding = PADDING_SIZE;

                    kollektifBoldPaint.TextSize = CATEGORY_FONT_SIZE;
                    kollektifPaint.TextSize = DESCRIPTION_FONT_SIZE;

                    canvas.DrawText(category.Name.ToUpper(), x, y, kollektifBoldPaint);

                    kollektifBoldPaint.TextSize = ITEM_NAME_FONT_SIZE;

                    y += kollektifBoldPaint.TextSize + 30;
                    index = 0;

                    foreach(MenuItem item in category.Items) {
                        drawWithKorean(canvas, x, y, kollektifBoldPaint, nanumGothicBoldPaint, item.Name.ToUpper(), item.Korean);
                        canvas.DrawText('$' + item.Cost, x + 1300, y, kollektifBoldPaint);
                        
                        y += kollektifPaint.TextSize + 20;
                        y = drawTextWrap(canvas, x, y, x + 1300, kollektifPaint, item.Description.ToLower());
                        y += kollektifBoldPaint.TextSize + 30;
                        index++;
                    }
                }
                else if (category.Name == "Korean Pancakes") {
                    startingx = 2350;
                    x = 2350;
                    y = 1200;
                    startingy = 1200;
                    padding = PADDING_SIZE;

                    kollektifBoldPaint.TextSize = CATEGORY_FONT_SIZE;
                    kollektifPaint.TextSize = DESCRIPTION_FONT_SIZE;

                    canvas.DrawText(category.Name.ToUpper() + " (JEON)", x, y, kollektifBoldPaint);

                    kollektifBoldPaint.TextSize = ITEM_NAME_FONT_SIZE;

                    y += kollektifBoldPaint.TextSize + 30;
                    index = 0;

                    foreach(MenuItem item in category.Items) {
                        drawWithKorean(canvas, x, y, kollektifBoldPaint, nanumGothicBoldPaint, item.Name.ToUpper(), item.Korean);
                        canvas.DrawText('$' + item.Cost, x + 1300, y, kollektifBoldPaint);
                        
                        y += kollektifPaint.TextSize + 20;
                        y = drawTextWrap(canvas, x, y, x + 1300, kollektifPaint, item.Description.ToLower());
                        y += kollektifBoldPaint.TextSize + 30;
                        index++;
                    }
                }
                else if (category.Name == "A La Carte") {
                    startingx = 2350;
                    x = 2350;
                    y = 1950;
                    startingy = 1950;
                    padding = PADDING_SIZE;

                    kollektifBoldPaint.TextSize = CATEGORY_FONT_SIZE;
                    kollektifPaint.TextSize = DESCRIPTION_FONT_SIZE;

                    canvas.DrawText(category.Name.ToUpper(), x, y, kollektifBoldPaint);

                    kollektifBoldPaint.TextSize = ITEM_NAME_FONT_SIZE;

                    y += kollektifBoldPaint.TextSize + 30;
                    index = 0;

                    foreach(MenuItem item in category.Items) {
                        drawWithKorean(canvas, x, y, kollektifBoldPaint, nanumGothicBoldPaint, item.Name.ToUpper(), item.Korean);
                        canvas.DrawText("S $" + item.Cost, x + 1300, y, kollektifBoldPaint);
                        double largeCost = double.Parse(item.Cost) + 9; 
                        canvas.DrawText("L $" + largeCost.ToString(), x + 1300, y+kollektifBoldPaint.TextSize+10, kollektifBoldPaint);
                        
                        y += kollektifPaint.TextSize + 20;
                        y = drawTextWrap(canvas, x, y, x + 1250, kollektifPaint, item.Description.ToLower().Remove(item.Description.IndexOf("Upgrade")));
                        y += kollektifBoldPaint.TextSize + 30;
                        index++;

                        if (item.Name.Contains("Korean")) {
                            canvas.DrawText("-> sauce: honey garlic / dosiiroc secret sauces", x + 30, y - 10, kollektifPaint);
                            y += kollektifBoldPaint.TextSize + 30;
                        }
                    }
                }
                else if (category.Name == "Rice Dishes") {
                    startingx = 3900;
                    x = 3900;
                    y = 600;
                    startingy = 600;
                    padding = PADDING_SIZE;

                    kollektifBoldPaint.TextSize = CATEGORY_FONT_SIZE;
                    kollektifPaint.TextSize = DESCRIPTION_FONT_SIZE;

                    canvas.DrawText(category.Name.ToUpper(), x, y, kollektifBoldPaint);

                    kollektifBoldPaint.TextSize = ITEM_NAME_FONT_SIZE;

                    y += kollektifBoldPaint.TextSize + 30;
                    index = 0;

                    foreach(MenuItem item in category.Items) {
                        drawWithKorean(canvas, x, y, kollektifBoldPaint, nanumGothicBoldPaint, item.Name.ToUpper(), item.Korean);
                        canvas.DrawText('$' + item.Cost, x + 1100, y, kollektifBoldPaint);
                        
                        y += kollektifPaint.TextSize + 20;
                        y = drawTextWrap(canvas, x, y, x + 1100, kollektifPaint, item.Description.ToLower());
                        y += kollektifBoldPaint.TextSize + 30;
                        index++;
                    }
                }
                else if (category.Name == "BBQ Meats") {
                    startingx = 3900;
                    x = 3900;
                    y = 1750;
                    startingy = 1750;
                    padding = PADDING_SIZE;

                    kollektifBoldPaint.TextSize = CATEGORY_FONT_SIZE;
                    kollektifPaint.TextSize = DESCRIPTION_FONT_SIZE;

                    canvas.DrawText("KOREAN " + category.Name.ToUpper(), x, y, kollektifBoldPaint);

                    kollektifBoldPaint.TextSize = ITEM_NAME_FONT_SIZE;

                    y += kollektifBoldPaint.TextSize + 30;
                    index = 0;

                    foreach(MenuItem item in category.Items) {
                        drawWithKorean(canvas, x, y, kollektifBoldPaint, nanumGothicBoldPaint, item.Name.ToUpper(), item.Korean);
                        canvas.DrawText('$' + item.Cost, x + 1100, y, kollektifBoldPaint);
                        
                        y += kollektifPaint.TextSize + 20;
                        y = drawTextWrap(canvas, x, y, x + 1100, kollektifPaint, item.Description.ToLower());
                        y += kollektifBoldPaint.TextSize + 30;
                        index++;
                    }

                    drawWithKorean(canvas, x, y, kollektifBoldPaint, nanumGothicBoldPaint, "ADD ON: LEAFY LETTUCE", "치마 상추");
                    canvas.DrawText("+$5.95", x - kollektifBoldPaint.MeasureText("+") + 1100, y, kollektifBoldPaint);

                    y += kollektifPaint.TextSize + 20;
                        y = drawTextWrap(canvas, x, y, x + 1100, kollektifPaint, "make your own bbq ssam (korean lettuce wraps) includes sliced chili peppers, garlic, and ssamjang sauce");

                }
            }

            // Save as PNG
            using SKImage image = SKImage.FromBitmap(bitmap);
            using SKData data = image.Encode(SKEncodedImageFormat.Png, 100);
            File.WriteAllBytes("menu.png", data.ToArray());
        }

        // returns the y value that the final word is on
        public static float drawTextWrap(SKCanvas canvas, float startingx, float y, float boundaryx, SKPaint paint, string text, float padding = 20) {
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