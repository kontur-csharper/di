using System;
using System.ComponentModel;
using System.Drawing;

namespace TagsCloud.Infrastructure
{
    public class ImageSettings
    {
        public ImageSettings(int width, int height)
        {
            this.width = width;
            this.height = height;
        }

        [Category("Цвета")]
        [DisplayName("Палитра")]
        [TypeConverter(typeof(ExpandableObjectConverter))]
        public Palette Palette { get; set; } = new Palette();

        private Font font = new Font(FontFamily.GenericSerif, 10);
        [DisplayName("Шрифт")]
        public Font Font
        {
            get => font;
            set
            {
                font = value;
                OnSettingsChange(EventArgs.Empty);
            }
        } 

        private int width;
        [DisplayName("Ширина")]
        public int Width 
        {
            get => width;
            set
            {
                width = value;
                OnSettingsChange(EventArgs.Empty);
            }
        }

        private int height;
        [DisplayName("Высота")]
        public int Height
        {
            get => height;
            set
            {
                height = value;
                OnSettingsChange(EventArgs.Empty);
            }
        }

        public static event EventHandler SettingsIsChanged;

        public static void OnSettingsChange(EventArgs e)
        {
            var handler = SettingsIsChanged;
            handler?.Invoke(null, e);
        }
    }
}