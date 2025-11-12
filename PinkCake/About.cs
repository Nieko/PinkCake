using PinkCake.Settings;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PinkCake
{
    public partial class About: Form
    {
        public IAboutData Data { get; set; }

        public About(IAboutData data)
        {
            Data = data;

            InitializeComponent();
            RefreshData();
        }

        private void RefreshData()
        {
            var settings = Data.SettingsStore.Load();
            this.ListBoxSources.Items.Clear();

            foreach (var source in settings.Sources)
            {
                this.ListBoxSources.Items.Add(source.AsDisplay());
            }

            StatusStripBottom.Items.Clear();

            if (!Data.GetServiceStatus())
            {
                StatusStripBottom.Items.Add("Service not Running");
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            var settings = Data.SettingsStore.Load();

            settings.Sources = ListBoxSources.Items
                .Cast<object>()
                .Select(i => i.ToString().ToSourceFolder())
                .ToArray();
            Data.SettingsStore.Save(settings);
            Data.StartService();

            StatusStripBottom.Text = Data.GetServiceStatus() ? "Saved" : "Service not running";
        }

        private void ListBoxSources_SelectedIndexChanged(object sender, EventArgs e)
        {
            BtnRemove.Enabled = (ListBoxSources.SelectedItems.Count > 0);
        }

        private void TbPicturesFolder_TextChanged(object sender, EventArgs e)
        {
            BtnAdd.Enabled = !string.IsNullOrEmpty(TbPicturesFolder.Text);
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (Directory.Exists(TbPicturesFolder.Text))
                {
                    this.ListBoxSources.Items.Add(new SourceFolder 
                    { 
                        RootFolder = TbPicturesFolder.Text, 
                        Filter = TbFilter.Text
                    }.AsDisplay());
                }
            }
            catch
            {
                this.StatusStripBottom.Text = "Folder invalid";
            }
        }

        private void BtnSelectFolder_Click(object sender, EventArgs e)
        {
            using (var fbd = new FolderBrowserDialog())
            {
                try
                {
                    if (Directory.Exists(TbPicturesFolder.Text))
                    {
                        fbd.SelectedPath = TbPicturesFolder.Text;
                    }
                }
                catch { }

                DialogResult result = fbd.ShowDialog();

                if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath) && Directory.Exists(fbd.SelectedPath))
                {
                    TbPicturesFolder.Text = fbd.SelectedPath;
                    TbPicturesFolder_TextChanged(this, EventArgs.Empty);
                }
            }
        }

        private void BtnRemove_Click(object sender, EventArgs e)
        {
            ListBoxSources.Items.RemoveAt(ListBoxSources.SelectedIndex);
        }
    }
}
