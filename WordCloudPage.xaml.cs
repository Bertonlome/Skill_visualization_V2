using KnowledgePicker.WordCloud;
using KnowledgePicker.WordCloud.Coloring;
using KnowledgePicker.WordCloud.Drawing;
using KnowledgePicker.WordCloud.Layouts;
using KnowledgePicker.WordCloud.Primitives;
using KnowledgePicker.WordCloud.Sizers;
using SkiaSharp;
using SkiaSharp.Views.UWP;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Skill_visualization
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class WordCloudPage : Page
    {
        public WordCloudPage()
        {
            this.InitializeComponent();

            WebViewA.Navigate(new Uri("https://quickchart.io/wordcloud?text=To be or not to be, that is the question"));
            /*
            var frequencies = new Dictionary<string, int>
            {
                ["KnowledgePicker"] = 3,
                ["WordCloud"] = 2,
                ["ErgoCog"] = 4,
                ["Coopération"] = 1,
                ["Ergo phys"] = 2,
                ["C# .NET"] = 5
            };

            IEnumerable<WordCloudEntry> wordEntries = frequencies.Select(p => new WordCloudEntry(p.Key, p.Value));

            var wordCloud = new WordCloudInput(wordEntries)
            {
                Width = 1024,
                Height = 256,
                MinFontSize = 8,
                MaxFontSize = 32
            };

            var sizer = new LogSizer(wordCloud);
            var engine = new SkGraphicEngine(sizer, wordCloud);
            var layout = new SpiralLayout(wordCloud);
            //var colorizer = new RandomColorizer();
            var colorizer = new SpecificColorizer(
                new Dictionary<string, Color>
                {
                    ["KnowledgePicker"] = Color.FromArgb(0x0f3057),
                    ["WordCloud"] = Color.FromArgb(0xe25a5a)
                },
                fallback: new RandomColorizer());
            var wcg = new WordCloudGenerator<SKBitmap>(wordCloud, engine, layout, colorizer);

            IEnumerable<(LayoutItem Item, double FontSize)> items = wcg.Arrange();

            var final = new SKBitmap(wordCloud.Width, wordCloud.Height);
            //var canvas = new SKXamlCanvas(final);

            //canvas.Clear(SKColor.Empty);
            var bitmap = wcg.Draw();
            //myCanvas.Draw Bitmap(bitmap, 0, 0);

            //var data = final.Encode(SKEncodedImageFormat.Png, 100);
            //var writer = File.Create("output.png");
            //data.SaveTo(writer);
            */
        }

    }
}
