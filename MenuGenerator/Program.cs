using System.ComponentModel;
using System.Text.Json;
using Microsoft.VisualBasic;
using SkiaSharp;

namespace MenuGenerator {
    class MenuGenerator {
        private static SKTypeface EMOJI_FONT = SKTypeface.FromFile("fonts/NotoColorEmoji-Regular.ttf");
        private static SKPaint EMOJI_PAINT = new SKPaint
        {
            Color = SKColors.Black,
            TextSize = 50,
            Typeface = EMOJI_FONT,
            IsAntialias = true
        };

        public class Menu {
            public List<Category>? Categories { get; set; }
        }

        public class Category {
            public string? Name { get; set; }
            public string? Type { get; set; }
            public List<MenuItem>? Items { get; set; }
            public string? egg_price { get; set; }
            public string? ade_price { get; set; }
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

            SKRect boxRect = new SKRect(150, 465, 2250, 1800);

            if(!args.Contains("drinks")) {
                kollektifBoldPaint.TextSize = DESCRIPTION_FONT_SIZE;
                
                canvas.DrawRect(150, 300, 250, 15, black);
                canvas.DrawRect(1800, 300, width-150-1800, 15, black);
                canvas.DrawRect(width-150-450, height-150, 450, 15, black);
                canvas.DrawRect(150, height-150, width-150-1600, 15, black);
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
            }
            else {
                kollektifBIPaint.TextSize = 100;
                if (!args.Contains("0")) {
                    canvas.DrawRect(150, 300, 2050, 15, black);
                    canvas.DrawRect(width-150-320, 300, 320, 15, black);

                    canvas.DrawText("cafe menu", 2200+1305-sceageusPaint.MeasureText("cafe menu")/2, 300+sceageusPaint.TextSize/4, sceageusPaint);
                    using (SKPaint red = new SKPaint { Color = new SKColor(230, 60, 59), Typeface = KollektifBI, TextSize = 100 }) {
                        canvas.DrawText("hot drinks ", 2200+1305-sceageusPaint.MeasureText("caf")/2-165, 300+sceageusPaint.TextSize/4+95, red);
                    };

                    canvas.DrawText(" - 12oz / ", 2200+1305-sceageusPaint.MeasureText("caf")/2-165+kollektifBIPaint.MeasureText("hot drinks"), 300+sceageusPaint.TextSize/4+95, kollektifBIPaint);

                    using (SKPaint blue = new SKPaint { Color = new SKColor(86, 123, 170), Typeface = KollektifBI, TextSize = 100}) {
                        blue.Color = new SKColor(86, 123, 170);

                        canvas.DrawText("iced drinks", 2200+1305-sceageusPaint.MeasureText("caf")/2-165+kollektifBIPaint.MeasureText("hot drinks - 12oz / "), 300+sceageusPaint.TextSize/4+95, blue);
                    }
                    canvas.DrawText(" - 16oz", 2200+1305-sceageusPaint.MeasureText("caf")/2-165+kollektifBIPaint.MeasureText("hot drinks - 12oz / iced drinks"), 300+sceageusPaint.TextSize/4+95, kollektifBIPaint);
                }
                else {
                    canvas.DrawRect(150, 300, 300, 15, black);
                    canvas.DrawRect(width-150-2050, 300, 2050, 15, black);

                    canvas.DrawText("cafe menu", 450+1305-sceageusPaint.MeasureText("cafe menu")/2, 300+sceageusPaint.TextSize/4, sceageusPaint);
                    using (SKPaint red = new SKPaint { Color = new SKColor(230, 60, 59), Typeface = KollektifBI, TextSize = 100 }) {
                        canvas.DrawText("hot drinks ", 450+1305-sceageusPaint.MeasureText("caf")/2-165, 300+sceageusPaint.TextSize/4+95, red);
                    };

                    canvas.DrawText(" - 12oz / ", 450+1305-sceageusPaint.MeasureText("caf")/2-165+kollektifBIPaint.MeasureText("hot drinks"), 300+sceageusPaint.TextSize/4+95, kollektifBIPaint);

                    using (SKPaint blue = new SKPaint { Color = new SKColor(86, 123, 170), Typeface = KollektifBI, TextSize = 100}) {
                        blue.Color = new SKColor(86, 123, 170);

                        canvas.DrawText("iced drinks", 450+1305-sceageusPaint.MeasureText("caf")/2-165+kollektifBIPaint.MeasureText("hot drinks - 12oz / "), 300+sceageusPaint.TextSize/4+95, blue);
                    }
                    canvas.DrawText(" - 16oz", 450+1305-sceageusPaint.MeasureText("caf")/2-165+kollektifBIPaint.MeasureText("hot drinks - 12oz / iced drinks"), 300+sceageusPaint.TextSize/4+95, kollektifBIPaint);
                }

                canvas.DrawRect(width-150-450, height-150, 450, 15, black);
                canvas.DrawRect(150, height-150, width-150-1600, 15, black);

                canvas.DrawText("dosiiroccafe.com | @dosiiroccafe", (width-1600)+((width-150-450) - (width-1600))/2 - kollektifBoldPaint.MeasureText("dosiiroccafe.com | @dosiiroccafe")/2, height-150+kollektifBoldPaint.TextSize/2, kollektifBoldPaint);
                canvas.DrawText("If you have a food allergy or any dietary restriction/preference, please notfiy us. We will do our best to accomodate you.", 150, 2950, kollektifBoldPaint);
            }

            float x = 0;
            float y = 0;
            float padding = 0;
            int index = 0;
            int startingx = 0;
            int startingy = 0;
            List<Category> foods = new();
            List<Category> drinks = new();
            List<Category> choice = new();
            Category korean_ade = new();

            if (args.Contains("drinks")) {
                choice = drinks;
            }
            else {
                choice = foods;
            }

            foreach (Category category in menu.Categories) {
                if (category.Type == "Food") {
                    foods.Add(category);
                }
                else {
                    drinks.Add(category);
                }

                if (category.Name.Equals("Korean Ade")) {
                    korean_ade = category;
                }

                foreach(MenuItem item in category.Items) {
                    if (item.Description.EndsWith('.')) {
                        item.Description = item.Description.TrimEnd('.');
                    }
                    item.Description = item.Description.Trim();
                }
            }

            foreach(Category category in choice) {
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
                            canvas.DrawText("-> sauce: bulgogi / dosiiroc secret sauce", x + 30, y, kollektifPaint);
                        }
                        else if (temp.Name == "Korean Boneless Fried Chicken") {
                            y += padding - 30;
                            text = "-> sauce: honey garlic / dosiiroc secret sauce";
                            canvas.DrawText(text, x + 30, y, kollektifPaint);
                            EMOJI_PAINT.TextSize = DESCRIPTION_FONT_SIZE;
                            canvas.DrawText("🌶", x + 30 + kollektifPaint.MeasureText(text), y, EMOJI_PAINT);
                        }
                    }

                    y += padding;
                    canvas.DrawText("add a fried egg for $" + category.egg_price, boxRect.MidX - kollektifBoldPaint.MeasureText("add a fried egg for $2.49")/2, y, kollektifBoldPaint);
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
                        y = drawTextWrap(canvas, x, y, x + 950, kollektifPaint, item.Description.ToLower());
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
                            canvas.DrawText("-> sauce: honey garlic / dosiiroc secret sauce", x + 30, y - 10, kollektifPaint);
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
                    y = drawTextWrap(canvas, x, y, x + 1100, kollektifPaint, "make your own bbq ssam (korean lettuce wraps) includes sliced chili peppers, garlic, and ssamjang sauce.");

                }
                else if (category.Name == "Stews") {
                    startingx = args.Contains("0") ? 3600 : 150;
                    x = args.Contains("0") ? 3600 : 150;
                    y = 500;
                    startingy = 500;
                    padding = PADDING_SIZE;

                    for (float circlex = x + (args.Contains("0") ? -200 : 1300), circley = 400; circley < 2900; circley += 100) {
                        canvas.DrawCircle(circlex, circley, 10, black);
                    }

                    kollektifBoldPaint.TextSize = CATEGORY_FONT_SIZE;
                    kollektifPaint.TextSize = DESCRIPTION_FONT_SIZE;

                    canvas.DrawText(category.Name.ToUpper(), x, y, kollektifBoldPaint);

                    kollektifBoldPaint.TextSize = ITEM_NAME_FONT_SIZE;

                    y += kollektifBoldPaint.TextSize + 30;

                    foreach(MenuItem item in category.Items) {
                        drawWithKorean(canvas, x, y, kollektifBoldPaint, nanumGothicBoldPaint, item.Name.ToUpper(), item.Korean);
                        canvas.DrawText('$' + item.Cost, x + 1100, y, kollektifBoldPaint);

                        if(item.Name.Equals("Kimchi Jjigae")) {
                            y += padding - 30;
                            canvas.DrawText("-> PORK / TUNA", x + 30, y, kollektifPaint);
                            
                            y += kollektifPaint.TextSize + 20;
                            y = drawTextWrap(canvas, x, y, x + 1100, kollektifPaint, item.Description.ToLower().Remove(item.Description.LastIndexOf(" with your")));
                        }
                        else if(item.Name.Equals("Dwenjang Jjigae")) {
                            y += padding - 30;
                            canvas.DrawText("-> VEGGIE / BEEF +$1.50", x + 30, y, kollektifPaint);

                            y += kollektifPaint.TextSize + 20;
                            y = drawTextWrap(canvas, x, y, x + 1100, kollektifPaint, item.Description.ToLower().Remove(item.Description.LastIndexOf(" with your")));
                        }
                        else if(item.Name.Equals("Soondubu Jjigae")) {
                            y += padding - 30;
                            canvas.DrawText("-> VEGGIE / PORK +$1.50 / SEAFOOD +$2", x + 30, y, kollektifPaint);

                            y += kollektifPaint.TextSize + 20;
                            y = drawTextWrap(canvas, x, y, x + 1100, kollektifPaint, item.Description.ToLower().Remove(item.Description.LastIndexOf(" with your")));
                        }
                        else {
                            y += kollektifPaint.TextSize + 20;
                            y = drawTextWrap(canvas, x, y, x + 1100, kollektifPaint, item.Description.ToLower());
                        }
                        
                        
                        y += kollektifBoldPaint.TextSize + 30;
                    }
                }
                else if (category.Name == "Katsu Cutlets") {
                    startingx = args.Contains("0") ? 3600 : 150;
                    x = args.Contains("0") ? 3600 : 150;
                    y = 2500;
                    startingy = 2500;
                    padding = PADDING_SIZE;

                    kollektifBoldPaint.TextSize = CATEGORY_FONT_SIZE;
                    kollektifPaint.TextSize = DESCRIPTION_FONT_SIZE;

                    canvas.DrawText(category.Name.ToUpper(), x, y, kollektifBoldPaint);

                    kollektifBoldPaint.TextSize = ITEM_NAME_FONT_SIZE;

                    y += kollektifBoldPaint.TextSize;

                    y = drawTextWrap(canvas, x, y, x + 1100, kollektifPaint, "deep fried cutlet topped with home made roux sauce and served with a side of cabbage slaw");

                    y += kollektifBoldPaint.TextSize + 30;
                    foreach(MenuItem item in category.Items) {
                        drawWithKorean(canvas, x, y, kollektifBoldPaint, nanumGothicBoldPaint, item.Name.ToUpper(), item.Korean);
                        canvas.DrawText('$' + item.Cost, x + 1100, y, kollektifBoldPaint);

                        y += kollektifBoldPaint.TextSize + 30;
                    }
                }
                else if (category.Name == "Coffee") {
                    startingx = args.Contains("0") ? 150: 1900;
                    x = args.Contains("0") ? 150: 1900;
                    y = 550;
                    startingy = 550;
                    padding = PADDING_SIZE;

                    kollektifBoldPaint.TextSize = CATEGORY_FONT_SIZE;
                    kollektifPaint.TextSize = DESCRIPTION_FONT_SIZE;

                    canvas.DrawText(category.Name.ToUpper() + " (", x, y, kollektifBoldPaint);

                    text = category.Name.ToUpper();

                    using (SKPaint red = new SKPaint { Color = new SKColor(230, 60, 59), Typeface = KollektifBold, TextSize = CATEGORY_FONT_SIZE}) {
                        canvas.DrawText("hot", startingx+kollektifBoldPaint.MeasureText(text + " ("), y, red);
                    };

                    canvas.DrawText("/", startingx+kollektifBoldPaint.MeasureText(text + " (hot"), y, kollektifBIPaint);

                    using (SKPaint blue = new SKPaint { Color = new SKColor(86, 123, 170), Typeface = KollektifBold, TextSize = CATEGORY_FONT_SIZE}) {
                        blue.Color = new SKColor(86, 123, 170);

                        canvas.DrawText("iced", startingx+kollektifBoldPaint.MeasureText(text + " (hot/"), y, blue);
                    }

                    canvas.DrawText(")", startingx+kollektifBoldPaint.MeasureText(text + " (hot/iced"), y, kollektifBIPaint);



                    kollektifBoldPaint.TextSize = ITEM_NAME_FONT_SIZE;

                    y += kollektifBoldPaint.TextSize + 30;

                    foreach(MenuItem item in category.Items) {
                        drawWithKorean(canvas, x, y, kollektifBoldPaint, nanumGothicBoldPaint, item.Name.ToUpper(), item.Korean);
                        canvas.DrawText('$' + item.Cost, x + 1100, y, kollektifBoldPaint);
                        
                        y += kollektifPaint.TextSize + 20;
                        if (item.Description.Contains("(hot/iced)")) {
                            y = drawTextWrap(canvas, x, y, x + 1100, kollektifPaint, item.Description.ToLower().Remove(item.Description.LastIndexOf("(hot/iced)")));
                        }
                        else {
                            y = drawTextWrap(canvas, x, y, x + 1100, kollektifPaint, item.Description.ToLower());
                        }
                        y += kollektifBoldPaint.TextSize + 30;
                    }
                }
                else if (category.Name == "Tea") {
                    startingx = args.Contains("0") ? 150: 1900;
                    x = args.Contains("0") ? 150: 1900;
                    y = 1550;
                    startingy = 1550;
                    padding = PADDING_SIZE;

                    kollektifBoldPaint.TextSize = CATEGORY_FONT_SIZE;
                    kollektifPaint.TextSize = DESCRIPTION_FONT_SIZE;

                    canvas.DrawText(category.Name.ToUpper() + " (", x, y, kollektifBoldPaint);

                    text = category.Name.ToUpper();

                    using (SKPaint red = new SKPaint { Color = new SKColor(230, 60, 59), Typeface = KollektifBold, TextSize = CATEGORY_FONT_SIZE}) {
                        canvas.DrawText("hot", startingx+kollektifBoldPaint.MeasureText(text + " ("), y, red);
                    };

                    canvas.DrawText("/", startingx+kollektifBoldPaint.MeasureText(text + " (hot"), y, kollektifBIPaint);

                    using (SKPaint blue = new SKPaint { Color = new SKColor(86, 123, 170), Typeface = KollektifBold, TextSize = CATEGORY_FONT_SIZE}) {
                        blue.Color = new SKColor(86, 123, 170);

                        canvas.DrawText("iced", startingx+kollektifBoldPaint.MeasureText(text + " (hot/"), y, blue);
                    }

                    canvas.DrawText(")", startingx+kollektifBoldPaint.MeasureText(text + " (hot/iced"), y, kollektifBIPaint);



                    kollektifBoldPaint.TextSize = ITEM_NAME_FONT_SIZE;

                    y += kollektifBoldPaint.TextSize + 30;

                    foreach(MenuItem item in category.Items) {
                        drawWithKorean(canvas, x, y, kollektifBoldPaint, nanumGothicBoldPaint, item.Name.ToUpper(), item.Korean);
                        canvas.DrawText('$' + item.Cost, x + 1100, y, kollektifBoldPaint);
                        
                        y += kollektifPaint.TextSize + 20;
                        if (item.Description.Contains("(hot/iced)")) {
                            y = drawTextWrap(canvas, x, y, x + 1100, kollektifPaint, item.Description.ToLower().Remove(item.Description.LastIndexOf("(hot/iced)")));
                        }
                        else {
                            y = drawTextWrap(canvas, x, y, x + 1100, kollektifPaint, item.Description.ToLower());
                        }
                        y += kollektifBoldPaint.TextSize + 30;
                    }

                    x = startingx + 1250/2;
                    y += 45;
                    kollektifBIPaint.TextSize = 95;

                    text = "add a shot for $1";
                    canvas.DrawText(text, x - kollektifBIPaint.MeasureText(text)/2, y, kollektifBIPaint);

                    y += 75;

                    kollektifBIPaint.TextSize = 65;

                    canvas.DrawRect(x-450, y, 900, 300, black);
                    canvas.DrawRect(x-440, y+10, 880, 280, brown);

                    text = "we use lactose-free milk.";
                    y += kollektifBIPaint.TextSize+20;
                    canvas.DrawText(text, x-kollektifBIPaint.MeasureText(text)/2, y, kollektifBIPaint);

                    text = "swap to oat milk for an";
                    y += kollektifBIPaint.TextSize+20;
                    canvas.DrawText(text, x-kollektifBIPaint.MeasureText(text)/2, y, kollektifBIPaint);

                    text = "additional 75¢";
                    y += kollektifBIPaint.TextSize+20;
                    canvas.DrawText(text, x-kollektifBIPaint.MeasureText(text)/2, y, kollektifBIPaint);
                }
                else if (category.Name == "Specialties") {
                    startingx = args.Contains("0") ? 1400: 3200;
                    x = args.Contains("0") ? 1400: 3200;
                    y = 550;
                    startingy = 550;
                    padding = PADDING_SIZE;

                    kollektifBoldPaint.TextSize = CATEGORY_FONT_SIZE;
                    kollektifPaint.TextSize = DESCRIPTION_FONT_SIZE;

                    canvas.DrawRect(x+300, y, 1300, 600, orange);
                    canvas.DrawRect(x+310, y+10, 1280, 580, offwhite);

                    text = "SOFT DRINKS";

                    canvas.DrawText(text, x+300+1280/2 - kollektifBoldPaint.MeasureText(text)/2, y + 30+kollektifBoldPaint.TextSize, kollektifBoldPaint);

                    kollektifBoldPaint.TextSize = DESCRIPTION_FONT_SIZE;

                    List<string> col1 = ["coca cola", "diet coke", "sprite", "ginger ale", "orange soda"];
                    List<string> col2 = ["sikhye", "milkis", "2% (peach)", "grape bongbong"];
                    List<string> col3 = ["watermelon soda", "calamansi soda", "peach bongbong", "jeju hallabong soda"];

                    text = "$1.99 / can";
                    canvas.DrawText(text, x+310+175-kollektifBoldPaint.MeasureText(text)/2, y+220, kollektifBoldPaint);
                    canvas.DrawLine(x+310+175-kollektifBoldPaint.MeasureText(text)/2, y+225, x+310+175+kollektifBoldPaint.MeasureText(text)/2, y+225, black);
                    y += kollektifBoldPaint.TextSize + 5;

                    foreach (string drink in col1) {
                        canvas.DrawText(drink, x+310+175-kollektifBoldPaint.MeasureText(drink)/2, y+220, kollektifBoldPaint);
                        y += kollektifBoldPaint.TextSize + 5;
                    }

                    y = startingy;

                    text = "$2.50 / can";
                    canvas.DrawText(text, x+310+580-kollektifBoldPaint.MeasureText(text)/2, y+220, kollektifBoldPaint);
                    canvas.DrawLine(x+310+580-kollektifBoldPaint.MeasureText(text)/2, y+225, x+310+580+kollektifBoldPaint.MeasureText(text)/2, y+225, black);
                    y += kollektifBoldPaint.TextSize + 5;

                    foreach (string drink in col2) {
                        canvas.DrawText(drink, x+310+580-kollektifBoldPaint.MeasureText(drink)/2, y+220, kollektifBoldPaint);
                        y += kollektifBoldPaint.TextSize + 5;
                    }

                    y = startingy;

                    text = "$2.99 / can";
                    canvas.DrawText(text, x+310+1050-kollektifBoldPaint.MeasureText(text)/2, y+220, kollektifBoldPaint);
                    canvas.DrawLine(x+310+1050-kollektifBoldPaint.MeasureText(text)/2, y+225, x+310+1050+kollektifBoldPaint.MeasureText(text)/2, y+225, black);
                    y += kollektifBoldPaint.TextSize + 5;

                    foreach (string drink in col3) {
                        canvas.DrawText(drink, x+310+1050-kollektifBoldPaint.MeasureText(drink)/2, y+220, kollektifBoldPaint);
                        y += kollektifBoldPaint.TextSize + 5;
                    }

                    kollektifBoldPaint.TextSize = CATEGORY_FONT_SIZE;

                    y += 500;
                    x += 375 + 1300/2 - 1600/2;

                    bool is_there_ade = korean_ade.Items.Count > 0;
                    
                    canvas.DrawRect(x-75, y-150, 1600, 200 + (category.Items.Count+ (is_there_ade ? 1 : 0)) * (is_there_ade ? 190 : 175), brown);

                    text = "DOSIIROC " + category.Name.ToUpper();

                    canvas.DrawText(text , x-75 + 1600/2 - kollektifBoldPaint.MeasureText(text)/2, y, kollektifBoldPaint);

                    kollektifBoldPaint.TextSize = ITEM_NAME_FONT_SIZE;

                    y += kollektifBoldPaint.TextSize + 30;
                    
                    foreach(MenuItem item in category.Items) {
                        drawWithKorean(canvas, x, y, kollektifBoldPaint, nanumGothicBoldPaint, item.Name.ToUpper(), item.Korean);
                        canvas.DrawText('$' + item.Cost, x + 1300, y, kollektifBoldPaint);

                        y += kollektifPaint.TextSize + 20;
                        if (item.Description.Contains("(hot/iced)")) {
                            y = drawTextWrap(canvas, x, y, x + 1300, kollektifPaint, item.Description.ToLower().Remove(item.Description.LastIndexOf("(hot/iced)")));
                        }
                        else {
                            y = drawTextWrap(canvas, x, y, x + 1300, kollektifPaint, item.Description.ToLower());
                        }
                        y += kollektifBoldPaint.TextSize + 30;
                    }
                    if (is_there_ade) {
                        canvas.DrawText("Korean Ade".ToUpper(), x, y, kollektifBoldPaint);
                        canvas.DrawText('$' + korean_ade.ade_price, x + 1300, y, kollektifBoldPaint);

                        y += kollektifPaint.TextSize + 20;

                        text = "->";

                        foreach(MenuItem ade in korean_ade.Items) {
                            text += " " + ade.Name + " |";
                        }

                        y = drawTextWrap(canvas, x, y, x + 1300, kollektifPaint, text.TrimEnd('|'));

                        y += kollektifPaint.TextSize + 20;
                        y = drawTextWrap(canvas, x, y, x + 1300, kollektifPaint, "Homemade fruit cheong (korean style fruit preserves) with sparkling water.");
                    }
                }
            }

            // Save as PNG
            using SKImage image = SKImage.FromBitmap(bitmap);
            using SKData data = image.Encode(SKEncodedImageFormat.Png, 100);
            File.WriteAllBytes("menu.png", data.ToArray());
        }

        // returns the y value that the final word is on
        public static float drawTextWrap(SKCanvas canvas, float startingx, float y, float boundaryx, SKPaint paint, string text, float padding = 20) {
            bool hasPeriod = text.EndsWith('.');
            string[] words = text.Split(" ");
            float x = startingx;
            float word_length = 0; 

            foreach (string word in words) {
                if (word.Contains("🌶")) {
                    word_length = EMOJI_PAINT.MeasureText(word + " ");

                    x += word_length;
                    if (x >= boundaryx) {
                        x = startingx + word_length;
                        y += paint.TextSize + padding;
                    }
                    canvas.DrawText("🌶", x - word_length, y, EMOJI_PAINT);
                    canvas.DrawText(".", x - word_length + EMOJI_PAINT.MeasureText("🌶"), y, paint);
                    continue;
                }
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