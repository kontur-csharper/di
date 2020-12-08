using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using TagsCloud.Infrastructure;
using TagsCloud.Layouters;
using TagsCloud.WordsProcessing;

namespace TagsCloud.UiActions
{
    public class RenderFileAction : IUiAction
    {
        public RenderFileAction(IImageHolder holder, ICloudLayouter layouter)
        {
            this.holder = holder;
            this.layouter = layouter;
        }

        private IImageHolder holder;
        private ICloudLayouter layouter;
        public string Category => "Файл";
        public string Name => "Визуализировать файл...";
        public string Description => "Выбрать файл для визуализации облака тегов";
        public void Perform()
        {
            var dialog = new OpenFileDialog()
            {
                CheckFileExists = true,
                CheckPathExists = true,
                InitialDirectory = Path.GetFullPath(Assembly.GetExecutingAssembly().Location),
                DefaultExt = "txt",
                Filter = "Текстовые файлы (*.txt)|*.txt|"
                         +"Документы (*.doc;*.docx)|*.doc;*.docx"
            };

            var res = dialog.ShowDialog();

            if (res != DialogResult.OK) 
                return;
            try
            {
                holder.RenderWordsFromFile(dialog.FileName);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Неправильный формат файла");
            }
        }
    }
}