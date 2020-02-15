namespace OriginV2.Core.Utilities
{
    partial class report
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(report));
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.xrPictureBox1 = new DevExpress.XtraReports.UI.XRPictureBox();
            this.ptbImage = new DevExpress.XtraReports.UI.XRPictureBox();
            this.ptbQR = new DevExpress.XtraReports.UI.XRPictureBox();
            this.name = new DevExpress.XtraReports.UI.XRLabel();
            this.date = new DevExpress.XtraReports.UI.XRLabel();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // TopMargin
            // 
            this.TopMargin.Name = "TopMargin";
            // 
            // BottomMargin
            // 
            this.BottomMargin.Name = "BottomMargin";
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.date,
            this.name,
            this.ptbQR,
            this.ptbImage,
            this.xrPictureBox1});
            this.Detail.HeightF = 464.1667F;
            this.Detail.Name = "Detail";
            // 
            // xrPictureBox1
            // 
            this.xrPictureBox1.ImageSource = new DevExpress.XtraPrinting.Drawing.ImageSource("img", resources.GetString("xrPictureBox1.ImageSource"));
            this.xrPictureBox1.LocationFloat = new DevExpress.Utils.PointFloat(10.00002F, 102.0416F);
            this.xrPictureBox1.Name = "xrPictureBox1";
            this.xrPictureBox1.SizeF = new System.Drawing.SizeF(640F, 267.7083F);
            this.xrPictureBox1.Sizing = DevExpress.XtraPrinting.ImageSizeMode.ZoomImage;
            // 
            // ptbImage
            // 
            this.ptbImage.LocationFloat = new DevExpress.Utils.PointFloat(100.9548F, 164.8889F);
            this.ptbImage.Name = "ptbImage";
            this.ptbImage.SizeF = new System.Drawing.SizeF(189.4097F, 109.2848F);
            this.ptbImage.Sizing = DevExpress.XtraPrinting.ImageSizeMode.ZoomImage;
            // 
            // ptbQR
            // 
            this.ptbQR.LocationFloat = new DevExpress.Utils.PointFloat(406.2531F, 122.3947F);
            this.ptbQR.Name = "ptbQR";
            this.ptbQR.SizeF = new System.Drawing.SizeF(226.8252F, 225.735F);
            this.ptbQR.Sizing = DevExpress.XtraPrinting.ImageSizeMode.ZoomImage;
            // 
            // name
            // 
            this.name.LocationFloat = new DevExpress.Utils.PointFloat(122.4363F, 291F);
            this.name.Multiline = true;
            this.name.Name = "name";
            this.name.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 96F);
            this.name.SizeF = new System.Drawing.SizeF(283.8168F, 26.20514F);
            this.name.StylePriority.UseTextAlignment = false;
            this.name.Text = "name";
            this.name.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // date
            // 
            this.date.LocationFloat = new DevExpress.Utils.PointFloat(133.5187F, 320F);
            this.date.Multiline = true;
            this.date.Name = "date";
            this.date.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.date.SizeF = new System.Drawing.SizeF(272.7343F, 26.20514F);
            this.date.StylePriority.UseTextAlignment = false;
            this.date.Text = "name";
            this.date.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // report
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.TopMargin,
            this.BottomMargin,
            this.Detail});
            this.Font = new System.Drawing.Font("Arial", 9.75F);
            this.Version = "19.1";
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
        private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraReports.UI.XRLabel date;
        private DevExpress.XtraReports.UI.XRLabel name;
        private DevExpress.XtraReports.UI.XRPictureBox ptbQR;
        private DevExpress.XtraReports.UI.XRPictureBox ptbImage;
        private DevExpress.XtraReports.UI.XRPictureBox xrPictureBox1;
    }
}
