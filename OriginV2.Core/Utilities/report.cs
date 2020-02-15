using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace OriginV2.Core.Utilities
{
    public partial class report : DevExpress.XtraReports.UI.XtraReport
    {
        public report()
        {
            InitializeComponent();
        }
        public void create(Image qr, Image image, string product, string harvest)
        {

            ptbImage.Image = image;
            ptbQR.Image = qr;
            name.Text = product;
            date.Text = harvest;

        }
    }
}
