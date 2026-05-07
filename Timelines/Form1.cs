using System;
using System.Windows.Forms;

namespace Timelines
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnStartEditor_Click(object sender, EventArgs e)
        {
            Editor editor = new Editor();
            editor.Show();
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Filter = "Timeline project (*.json)|*.json";
                dialog.Title = "Otevřít projekt";

                if (dialog.ShowDialog() != DialogResult.OK)
                {
                    return;
                }

                TimelineProject project = TimelineStorage.Load(dialog.FileName);

                Editor editor = new Editor(project, dialog.FileName);
                editor.Show();
            }
        }
    }
}